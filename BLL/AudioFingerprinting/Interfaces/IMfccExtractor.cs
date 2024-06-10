using NAudio.Wave;
using NWaves.FeatureExtractors.Options;

namespace BLL
{
    public interface IMfccExtractor
    {
        byte[] ComputeFrom(WaveFormat waveFormat, float[] samples);
    }
}
