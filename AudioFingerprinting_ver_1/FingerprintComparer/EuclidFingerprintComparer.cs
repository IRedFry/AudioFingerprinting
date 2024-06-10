
namespace AudioFingerprinting_ver_1
{
    public struct CompareResult
    {
        public int index;
        public double distance;
        public string trackName;
    }

    public class EuclidFingerprintComparer
    {

        public EuclidFingerprintComparer() { }

        public CompareResult Compare(List<float[]> baseFingerprint, List<float[]> fingerprint)
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

            //minDistance /= frameCount;

            return new CompareResult() {index = startIndexMinDistance, distance = minDistance, trackName= "" };
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
            double sum  = 0.0f; 

            for (int i =0; i < vector1.Length; ++i)
            {
                sum += Math.Pow(vector1[i] - vector2[i], 2);
            }

            return Math.Sqrt(sum);
        }
        
        
    }
}
