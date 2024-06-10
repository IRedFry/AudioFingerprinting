
using System.Collections;

namespace AudioFingerprinting_ver_1
{
    public class EuclidCompactFingerprintComparer // Also LSHSimilaritySearch
    {

        public EuclidCompactFingerprintComparer() { }

        public CompareResult Compare(BitArray baseFingerprint, BitArray fingerprint)
        {
            byte[] baseFingerprintByte = new byte[baseFingerprint.Length / 8 + 1];
            byte[] fingerprintByte = new byte[fingerprint.Length / 8 + 1];

            BitArray baseFingerprintReverse = new BitArray(baseFingerprint.Cast<bool>().Reverse().ToArray());
            BitArray fingerprintReverse = new BitArray(fingerprint.Cast<bool>().Reverse().ToArray());

            baseFingerprintReverse.CopyTo(baseFingerprintByte, 0);
            fingerprintReverse.CopyTo(fingerprintByte, 0);


            int frameCount = fingerprintByte.Length;
            int trackFrameCount = baseFingerprintByte.Length;

            int startPositionCount = trackFrameCount - frameCount + 1;

            double minDistance = Double.MaxValue;
            int startIndexMinDistance = -1;



            double[] distances = new double[startPositionCount];
            Parallel.For(0, startPositionCount, i =>
            {
                distances[i] = FrameDistance(baseFingerprintByte, fingerprintByte, i);
            });

            minDistance = distances.Min(value => value);
            startIndexMinDistance = Array.IndexOf(distances, minDistance);


            return new CompareResult() {index = startIndexMinDistance, distance = minDistance, trackName= "" };
        }

        private double FrameDistance(byte[] frame1, byte[] frame2, int frame1StartIndex)
        {
            double totalDistance = 0.0;

            byte[] distances = new byte[frame2.Length];
            for (int i = 0; i < frame2.Length; ++i)
                distances[i] = (byte)(frame1[i + frame1StartIndex] ^ frame2[i]);

            BitArray distanceByte = new BitArray(distances);
            totalDistance = distanceByte.Cast<bool>().Sum(i => i ? 1.0 : 0.0);
            totalDistance /= frame2.Length;

            return totalDistance;
        }
    }
}
