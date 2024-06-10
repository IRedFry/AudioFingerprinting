namespace BLL
{
    public struct CompareResult
    {
        public int trackId;
        public int index;
        public double distance;
    }
    public interface IFingerprintComparer
    {
        public CompareResult CompareCompact(byte[] baseFingerpring, byte[] fingerpring);
        public CompareResult CompareSimple(List<float[]> baseFingerprint, List<float[]> fingerprint);
    }
}
