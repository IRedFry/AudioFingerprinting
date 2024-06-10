using NAudio.Wave;
using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Options;
using NWaves.Windows;
using System.Collections;

namespace BLL
{
    public class CompactMfccExtractor : IMfccExtractor
    {
        public byte[] ComputeFrom(WaveFormat waveFormat, float[] samples)
        {
            var mfcc = ComputeSimpleMfcc(waveFormat, samples);

            BitArray compactMfcc = CreateCompactMfccBitArray(mfcc);

            return CreteByteArrayFromCompactMfccBitArray(compactMfcc);
        }

        private List<float[]> ComputeSimpleMfcc(WaveFormat waveFormat, float[] samples)
        {
            var mfccOptions = new MfccOptions
            {
                SamplingRate = waveFormat.SampleRate,
                FeatureCount = 13,
                FrameDuration = 0.025/*sec*/,
                HopDuration = 0.010/*sec*/,
                FilterBankSize = 26,
                PreEmphasis = 0.97,
                Window = WindowType.Hamming
            };
            var mfccExtractor = new MfccExtractor(mfccOptions);

            return mfccExtractor.ComputeFrom(samples);
        }
        private BitArray CreateCompactMfccBitArray(List<float[]> simpleMfcc)
        {
            int stringLength = (simpleMfcc.Count - 1) * (simpleMfcc[0].Length - 1);
            BitArray stringArray = new BitArray(stringLength);


            for (int i = 1; i < simpleMfcc.Count; i++)
            {
                for (int j = 0; j < simpleMfcc[i].Length - 1; j++)
                {
                    float result = (simpleMfcc[i][j] - simpleMfcc[i][j + 1]) - (simpleMfcc[i - 1][j] - simpleMfcc[i - 1][j + 1]);
                    if (result > 0)
                        stringArray[(simpleMfcc[i].Length - 1) * (i - 1) + j] = true;
                    else
                        stringArray[(simpleMfcc[i].Length - 1) * (i - 1) + j] = false;
                }
            }

            return stringArray;
        }
        private byte[] CreteByteArrayFromCompactMfccBitArray(BitArray compactMfcc)
        {
            byte[] resultArray = new byte[compactMfcc.Count / 8 + 1];
            BitArray rightOrderBits = new BitArray(compactMfcc.Cast<bool>().Reverse().ToArray());
            rightOrderBits.CopyTo(resultArray, 0);

            return resultArray;
        }
    }
}
