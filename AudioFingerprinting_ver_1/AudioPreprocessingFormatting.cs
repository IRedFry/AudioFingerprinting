using Microsoft.VisualBasic;
using NAudio.Lame;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Reflection.PortableExecutable;

namespace AudioFingerprinting_ver_1
{
    public class AudioPreprocessingFormatting
    {
        private int bitDepth;
        private int sampleRate;
        private int channelCount;

        private byte[] audioData;
        IWaveProvider sampleProvider;
        WaveFormat waveFormat;

        public AudioPreprocessingFormatting(int bitDepth, int sampleRate, int channelCount)
        {
            this.bitDepth = bitDepth;
            this.sampleRate = sampleRate;
            this.channelCount = channelCount;
        }

        public void ReadAudioDataFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Could not find file");
                return;
            }
            
            if (Path.GetExtension(filePath) == ".mp3")
                ReadMP3File(filePath);
            else if (Path.GetExtension(filePath) == ".wav")
                ReadWavFile(filePath);
        }

        private void ReadWavFile(string filePath)
        {
            Console.WriteLine("Read .wav file");

            using (var waveStream = new WaveFileReader(filePath))
            {
                waveFormat = waveStream.WaveFormat;
                sampleProvider = waveStream;

                WriteWaveFormat(waveFormat);

                waveFormat = ChangeBitsPerSampleTo16(waveFormat);
                waveFormat = ChangeSampleRate(waveFormat, 44100);
                waveFormat = StereoToMonoFormatting(waveFormat);

                audioData = new byte[waveStream.Length];

                int bytesRead = waveStream.Read(audioData, 0, audioData.Length);
            }
            CreateWavFromWaveFormat(filePath);
        }

        private void ReadMP3File(string filePath)
        {
            Console.WriteLine("Read .mp3 file");


            using (var waveStream = new Mp3FileReader(filePath))
            {
                waveFormat = waveStream.WaveFormat;
                sampleProvider = waveStream;

                WriteWaveFormat(waveFormat);

                waveFormat = ChangeBitsPerSampleTo16(waveFormat);
                waveFormat = ChangeSampleRate(waveFormat, 5012);
                waveFormat = StereoToMonoFormatting(waveFormat);

                audioData = new byte[waveStream.Length];

                int bytesRead = waveStream.Read(audioData, 0, audioData.Length);
            }

            CreateWavFromWaveFormatMP3(filePath);
        }


        private IWaveProvider ChangeSampleRate(IWaveProvider waveProvider, int desiredSampleRate)
        {
            var resampledAudio = new WdlResamplingSampleProvider(waveProvider.ToSampleProvider(), desiredSampleRate);
            
            //if (waveProvider.WaveFormat.BitsPerSample == 16)
                //return resampledAudio.ToWaveProvider16();
            //else
                return new SampleToWaveProvider16(resampledAudio);
        }
        private WaveFormat ChangeSampleRate(WaveFormat waveFormat, int desiredSampleRate)
        {
            var newWaveFormat = new WaveFormat(desiredSampleRate, waveFormat.BitsPerSample, waveFormat.Channels);;
            return newWaveFormat;
        }

        private IWaveProvider StereoToMonoFormatting(IWaveProvider sampleProvider)
        {
            var mono = new StereoToMonoSampleProvider(sampleProvider.ToSampleProvider());
            mono.LeftVolume = 1.0f; // discard the left channel
            mono.RightVolume = 0.0f; // keep the right channel
            return new SampleToWaveProvider16(mono);
        }

        private WaveFormat StereoToMonoFormatting(WaveFormat waveFormat)
        {
            var newWaveFormat = new WaveFormat(waveFormat.SampleRate, waveFormat.BitsPerSample, 1);
            return newWaveFormat;
        }

        private IWaveProvider ChangeBitsPerSampleTo16(IWaveProvider sampleProvider)
        {
            if (sampleProvider.WaveFormat.BitsPerSample == 16)
                return sampleProvider;
            
            return new SampleToWaveProvider16(sampleProvider.ToSampleProvider());
        }

        private WaveFormat ChangeBitsPerSampleTo16(WaveFormat waveFormat)
        {
            var newWaveFormat = new WaveFormat(waveFormat.SampleRate, 16, waveFormat.Channels);
            return newWaveFormat;
        }

        private void WriteWaveFormat(WaveFormat waveFormat)
        {
            Console.WriteLine();
            Console.WriteLine($"Формат аудио: {waveFormat.Encoding}");
            Console.WriteLine($"Бит: {waveFormat.BitsPerSample}");
            Console.WriteLine($"Частота дискретизации: {waveFormat.SampleRate} Гц");
            Console.WriteLine($"Количество каналов: {waveFormat.Channels}");
        }

        public void CreateWavFromSampleProvider()
        {
            WaveFileWriter.CreateWaveFile("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example_new.wav", sampleProvider);
            
        }

        public void CreateWavFromWaveFormat(string filename)
        {

            using (var reader = new WaveFileReader(filename))
            {
                using (var conversionStream = new WaveFormatConversionStream(waveFormat, reader))
                {
                    WaveFileWriter.CreateWaveFile("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example_new.wav", conversionStream);
                }
            }

            using (var reader = new WaveFileReader("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example_new.wav"))
            {
                WriteWaveFormat(reader.WaveFormat);
            }

        }

        public void CreateWavFromWaveFormatMP3(string filename)
        {

            using (var reader = new Mp3FileReader(filename))
            {
                using (var conversionStream = new WaveFormatConversionStream(waveFormat, reader))
                {
                    WaveFileWriter.CreateWaveFile("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example_new.wav", conversionStream);
                }
            }

            using (var reader = new WaveFileReader("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example_new.wav"))
            {
                WriteWaveFormat(reader.WaveFormat);
            }

        }
    }
}
