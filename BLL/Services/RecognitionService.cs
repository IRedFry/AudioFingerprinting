using NAudio.Wave;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;

namespace BLL
{
    public class RecognitionService : IRecognitionService
    {
        private IUnityOfWork context;
        private IMfccExtractor mfccExtractor;
        private IFingerprintComparer fingerprintComparer;
        public RecognitionService(IUnityOfWork context, IMfccExtractor mfccExtractor, IFingerprintComparer fingerprintComparer)
        {
            this.context = context;
            this.mfccExtractor = mfccExtractor;
            this.fingerprintComparer = fingerprintComparer;
        }
        public int GetBestRecognitionResult(string base64, int userId)
        {
            byte[] data = Convert.FromBase64String(base64);

            AudioData audioData = new AudioData(data);

            WaveFormatBuilder waveFormatBuilder = new WaveFormatBuilder();
            waveFormatBuilder.SetChannels(1);
            waveFormatBuilder.SetBitsPerSample(16);
            waveFormatBuilder.SetSampleRate(16000);
            WaveFormat waveFormat = waveFormatBuilder.GetWaveFormat();

            audioData.ChangeAudioFormat(waveFormat);

            SamplesExtractor samplesExtractor = new SamplesExtractor(audioData);

            byte[] fingerprint = mfccExtractor.ComputeFrom(waveFormat, samplesExtractor.samples);

            var tracks = context.Tracks.GetList();

            CompareResult finalCompareResult = new CompareResult
            {
                trackId = -1,
                distance = Double.MaxValue,
                index = -1
            };

            foreach (var track in tracks)
            {
                CompareResult compareResult = fingerprintComparer.CompareCompact(track.Fingerprint, fingerprint);


                //Console.WriteLine(track.Title + " = " + compareResult.distance + ", " + track.Fingerprint.Length);
                if (compareResult.distance < finalCompareResult.distance)
                {
                    finalCompareResult.distance = compareResult.distance;
                    finalCompareResult.index = compareResult.index;
                    finalCompareResult.trackId = track.Id;
                    if (finalCompareResult.distance < 3.5)
                        break;
                }
            }

            if (userId != -1)
            {
                context.RecognitionHistory.Create(new RecognitionHistory(default, DateTime.Now, userId, finalCompareResult.trackId));
                context.Save();
            }

            return finalCompareResult.trackId;
        }

        public void GetBestRecognitionResult(string base64, int userId, string necTrack)
        {
            byte[] data = Convert.FromBase64String(base64);

            AudioData audioData = new AudioData(data);

            WaveFormatBuilder waveFormatBuilder = new WaveFormatBuilder();
            waveFormatBuilder.SetChannels(1);
            waveFormatBuilder.SetBitsPerSample(16);
            waveFormatBuilder.SetSampleRate(16000);
            WaveFormat waveFormat = waveFormatBuilder.GetWaveFormat();

            audioData.ChangeAudioFormat(waveFormat);

            SamplesExtractor samplesExtractor = new SamplesExtractor(audioData);

            byte[] fingerprint = mfccExtractor.ComputeFrom(waveFormat, samplesExtractor.samples);

            var tracks = context.Tracks.GetList();

            foreach (var track in tracks)
            {
                Console.WriteLine(track.Title + "!!!!!");
                List<Tuple<double, string>> sims = new List<Tuple<double, string>>();
                BitArray setBit = new BitArray(track.Fingerprint.Skip((track.Fingerprint.Length - 14000) / 2).Take(14000).ToArray());
                foreach (var track2 in tracks)
                {
                    BitArray setOrBit = new BitArray(track2.Fingerprint.Skip((track2.Fingerprint.Length - 14000) / 2).Take(14000).ToArray());
                    bool[] set = setBit.Cast<bool>().Select(i => i).ToArray();
                    bool[] setOr = setOrBit.Cast<bool>().Select(i => i).ToArray();

                    int numHashFunctions = 74;
                    MinHash minHash = new MinHash(numHashFunctions);

                    int[] signature1 = minHash.ComputeMinHashSignature(set);
                    int[] signature2 = minHash.ComputeMinHashSignature(setOr);

                    double similarity = MinHash.ComputeJaccardSimilarity(signature1, signature2);
                    if (track.GenreId == track2.GenreId)
                        similarity += 0.2f;
                    if (track.ArtistId == track2.ArtistId)
                        similarity += 0.1f;

                    sims.Add(new Tuple<double, string>(similarity, track2.Title));
                }

                var best = sims.OrderByDescending(i => i.Item1).Skip(1).Take(3).ToList();

                foreach (var pair in best)
                {
                    Console.WriteLine(pair.Item1 + " - " + pair.Item2);
                }
                Console.WriteLine();
            }
        }

        public int[] GetRecommendations(int trackId)
        {
            var track = context.Tracks.GetItem(trackId);

            byte[] signatureBytes = track.LSH;

            int[] signature = new int[signatureBytes.Length / 4];
            for (int i = 0; i < signature.Length; i++)
                signature[i] = BitConverter.ToInt32(signatureBytes, i * 4);


            MinHash minHash = new MinHash();
            List<Tuple<double, int>> sims = new List<Tuple<double, int>>();
            var tracks = context.Tracks.GetList();

            foreach (var recTrack in tracks)
            {        
                BitArray setOrBit = new BitArray(recTrack.Fingerprint.Skip((recTrack.Fingerprint.Length - 14000) / 2).Take(14000).ToArray());
                bool[] setOr = setOrBit.Cast<bool>().Select(i => i).ToArray();

                int[] signature2 = minHash.ComputeMinHashSignature(setOr);

                double similarity = MinHash.ComputeJaccardSimilarity(signature, signature2);

                if (track.GenreId == recTrack.GenreId)
                    similarity += 0.15f;
                if (track.ArtistId == recTrack.ArtistId)
                    similarity += 0.1f;

                sims.Add(new Tuple<double, int>(similarity, recTrack.Id));
            }

            var best = sims.OrderByDescending(i => i.Item1).Skip(1).Take(3).Select(i => i.Item2).ToArray();

            return best;
        }
    }
}
