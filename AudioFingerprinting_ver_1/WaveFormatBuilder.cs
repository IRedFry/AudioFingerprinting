
using NAudio.Wave;

namespace AudioFingerprinting_ver_1
{
    public class WaveFormatBuilder
    {
        private WaveFormat waveFormat;

        public WaveFormatBuilder() 
        {
            waveFormat = new WaveFormat(44100, 1);  
        }

        public WaveFormatBuilder(WaveFormat waveFormat)
        {
            this.waveFormat = waveFormat;
        }

        public WaveFormatBuilder(int sampleRate, int bitsPerSample, int channels)
        {
            waveFormat = new WaveFormat(sampleRate, bitsPerSample, channels);
        }

        public WaveFormat SetSampleRate(int sampleRate) 
        { 
            waveFormat = new WaveFormat(sampleRate, waveFormat.BitsPerSample, waveFormat.Channels);
            return waveFormat;
        }

        public WaveFormat SetBitsPerSample(int bitsPerSample)
        {
            waveFormat = new WaveFormat(waveFormat.SampleRate, bitsPerSample, waveFormat.Channels);
            return waveFormat;
        }

        public WaveFormat SetChannels(int channels)
        {
            waveFormat = new WaveFormat(waveFormat.SampleRate, waveFormat.BitsPerSample, channels);
            return waveFormat;
        }

        public WaveFormat GetWaveFormat()
        {
            return waveFormat;
        }
    }
}
