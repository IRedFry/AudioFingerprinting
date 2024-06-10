using Accord.Math.Distances;
using Accord.Math.Metrics;
using System.Collections;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace BLL
{
    public class EuclidFingerprintComparer : IFingerprintComparer
    {
        public CompareResult CompareCompact(byte[] baseFingerprint, byte[] fingerprint)
        {
            int frameCount = fingerprint.Length;
            int trackFrameCount = baseFingerprint.Length;

            int startPositionCount = trackFrameCount - frameCount + 1;

            double minDistance;
            int startIndexMinDistance;


            byte[] miniF = new byte[fingerprint.Length];
            Array.Copy(baseFingerprint, miniF, miniF.Length);

            double dist = 0;

            for (int i = 0; i < miniF.Length; ++i)
                dist += BitOperations.PopCount((uint)(miniF[i] ^ fingerprint[i]));
            
            minDistance = dist / fingerprint.Length;
            startIndexMinDistance = 0;

            for (int i = miniF.Length; i < startPositionCount; ++i)
            {
                dist = 0;
                Array.Copy(baseFingerprint, i, miniF, 0, miniF.Length);
                for (int j = 0; j < miniF.Length; ++j)
                    dist += BitOperations.PopCount((uint)(miniF[j] ^ fingerprint[j]));

                dist /= fingerprint.Length;

                if (dist < minDistance)
                {
                    minDistance = dist;
                    startIndexMinDistance = i;
                }
            }


            return new CompareResult() { index = startIndexMinDistance, distance = minDistance, trackId = -1 };
        }

        public CompareResult CompareSimple(List<float[]> baseFingerprint, List<float[]> fingerprint)
        {
            int frameCount = fingerprint.Count;
            int trackFrameCount = baseFingerprint.Count;

            int startPositionCount = trackFrameCount - frameCount + 1;

            double minDistance = Double.MaxValue;
            int startIndexMinDistance = -1;

            double[] distances = new double[startPositionCount];
            Parallel.For(0, startPositionCount, i =>
            {
                distances[i] = FrameDistance(baseFingerprint, fingerprint, i);
            });

            minDistance = distances.Min(value => value);
            startIndexMinDistance = Array.IndexOf(distances, minDistance);

            return new CompareResult() { index = startIndexMinDistance, distance = minDistance, trackId = -1 };
        }

        private double FrameDistance(List<float[]> frame1, List<float[]> frame2, int frame1StartIndex)
        {
            double totalDistance = 0.0;

            double[] distances = new double[frame2.Count];
            Parallel.For(0, frame2.Count, i =>
            {
                distances[i] = EuclideanDistance(frame1[i + frame1StartIndex], frame2[i]);
            });

            totalDistance = distances.Sum(i => i);
            totalDistance /= frame2.Count;

            return totalDistance;
        }

        private double EuclideanDistance(float[] vector1, float[] vector2)
        {
            double sum = 0.0f;

            for (int i = 0; i < vector1.Length; ++i)
            {
                sum += Math.Pow(vector1[i] - vector2[i], 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
