namespace BLL
{
    public class SamplesExtractor
    {
        public float[] samples { get; private set; }
        private int sampleByteLength;

        public SamplesExtractor(AudioData audioData)
        {
            CalculateSampleArrayFromBytes(audioData);
        }
        private void CalculateSampleArrayFromBytes(AudioData audioData)
        {
            // Calculate sample size and store samples
            sampleByteLength = (audioData.waveFormat.BitsPerSample / 8);
            int samplesCount = (audioData.data.Length / sampleByteLength);
            samples = new float[samplesCount];

            byte[] sampleBytes = new byte[sampleByteLength];
            for (int i = 0; i < samplesCount; i++)
            {
                Array.Copy(audioData.data, i * sampleByteLength, sampleBytes, 0, sampleByteLength);
                samples[i] = (BitConverter.ToInt16(sampleBytes, 0)) / 32767.0f; // from -1 to 1
            }
        }
    }
}
