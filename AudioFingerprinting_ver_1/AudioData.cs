using NAudio.Dmo;
using NAudio.Wave;

namespace AudioFingerprinting_ver_1
{
    public class AudioData // Class that represesnts audio data in samples
    {
        const int byteBufferSize = 262144; // Size of byteBuffer for reading from file

        private byte[] bytesData; // Bytes array
        private float[] samples; // Samples
        private WaveFormat waveFormat; // Format of audio
        private int sampleByteLength; // Count of bytes in one sample

        public AudioData(float[] samples, WaveFormat waveFormat)
        {
            this.samples = samples;
            this.waveFormat = waveFormat;
        }

        public AudioData(string filename)
        {
            var b = File.ReadAllBytes(filename);
            Console.WriteLine("Original = " + b.Length);
            LoadFromFile(filename);
        }

        public AudioData() { }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Could not find file");
                return;
            }

            if (Path.GetExtension(filename) == ".mp3")
                ReadOtherFile(filename);
            else if (Path.GetExtension(filename) == ".wav")
                ReadWavFile(filename);
            else
                ReadOtherFile(filename);
        }

        public WaveFormat GetWaveFormat()
        {
            return waveFormat;
        }

        public float[] GetSamples()
        {
            return samples;
        }

        public void ChangeAudioFormat(WaveFormat newWaveFormat)
        {
            
            using (MemoryStream inputStream = new MemoryStream(bytesData))
            {
                using (WaveStream waveStream = new RawSourceWaveStream(inputStream, waveFormat))
                {
                    WaveFormatConversionStream conversionStream = new WaveFormatConversionStream(newWaveFormat, waveStream);

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, conversionStream.WaveFormat))
                        {
                            byte[] buffer = new byte[bytesData.Length];
                            int totalBytesRead = 0;
                            int bytesRead = 0;
                            while ((bytesRead = conversionStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                waveFileWriter.Write(buffer, 0, bytesRead);
                                totalBytesRead += bytesRead;
                            }
                            bytesData = new byte[totalBytesRead];
                            Buffer.BlockCopy(buffer, 0, bytesData, 0, totalBytesRead);
                        }

                        waveFormat = newWaveFormat;

                        CalculateSampleArrayFromBytes();
                    }   
                }
            }
        }

        private void ReadMP3File(string filename)
        {
            using (var reader = new Mp3FileReader(filename))
            {
                // Save wave format
                waveFormat = reader.WaveFormat;

                // Read bytes from file
                bytesData = new byte[reader.Length];
                byte[] byteBuffer = new byte[byteBufferSize];
                int bytesRead = 0;
                int totalBytesRead = 0;
                while ((bytesRead = reader.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; ++i)
                        bytesData[totalBytesRead + i] = byteBuffer[i];
                    totalBytesRead += bytesRead;
                }

                CalculateSampleArrayFromBytes();
            }
        }

        private void ReadWavFile(string filename)
        {
            using (var reader = new WaveFileReader(filename))
            {

                // Save wave format
                waveFormat = reader.WaveFormat;

                // Read bytes from file
                bytesData = new byte[reader.Length];
                byte[] byteBuffer = new byte[byteBufferSize];
                int bytesRead = 0;
                int totalBytesRead = 0;
                while ((bytesRead = reader.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; ++i)
                        bytesData[totalBytesRead + i] = byteBuffer[i];
                    totalBytesRead += bytesRead;
                }

                CalculateSampleArrayFromBytes();
            }
        }       
        
        private void ReadOtherFile(string filename)
        {
            
            using (var reader = new MediaFoundationReader(filename))
            {
                // Save wave format
                waveFormat = reader.WaveFormat;

                // Read bytes from file
                Console.WriteLine(filename + " === " + reader.Length);
                bytesData = new byte[reader.Length];
                byte[] byteBuffer = new byte[byteBufferSize];
                int bytesRead = 0;
                int totalBytesRead = 0;
                while ((bytesRead = reader.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; ++i)
                        bytesData[totalBytesRead + i] = byteBuffer[i];
                    totalBytesRead += bytesRead;
                }

                CalculateSampleArrayFromBytes();
            }
        }

        private void CalculateSampleArrayFromBytes()
        {
            // Calculate sample size and store samples
            sampleByteLength = (waveFormat.BitsPerSample / 8);
            int samplesCount = (bytesData.Length / sampleByteLength) ;
            samples = new float[samplesCount];

            byte[] sampleBytes = new byte[sampleByteLength];
            for (int i = 0; i < samplesCount; i++)
            {
                Array.Copy(bytesData, i * sampleByteLength, sampleBytes, 0, sampleByteLength);
                samples[i] = (BitConverter.ToInt16(sampleBytes, 0)) / 32767.0f; // from -1 to 1
            }
        }

        public void PlayAudioData()
        {
            var ms = new MemoryStream(bytesData);
            var rs = new RawSourceWaveStream(ms, new WaveFormat(waveFormat.SampleRate, waveFormat.BitsPerSample, waveFormat.Channels));

            var wo = new WaveOutEvent();
            wo.Init(rs);
            wo.Play();
            Console.WriteLine("Write \'stop\' to stop playing audio");
            while (wo.PlaybackState == PlaybackState.Playing)
            {
                var input = Console.ReadLine();
                if (input == "stop")
                    break;
                Thread.Sleep(1);
            }
            wo.Dispose();
        }
    }
}
