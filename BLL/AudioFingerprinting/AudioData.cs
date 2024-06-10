using NAudio.Wave;

namespace BLL
{
    public class AudioData
    {
        const int byteBufferSize = 262144;

        public byte[] data { get; private set; }
        public WaveFormat waveFormat { get; private set; }

        public AudioData(string filename) 
        {
            LoadFromFile(filename);
        }
        public AudioData(byte[] data)
        {
            using (var reader = new StreamMediaFoundationReader(new MemoryStream(data)))
            {
                SetData(reader);
            }

        }
        private void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string extension = Path.GetExtension(filename);

            switch(extension)
            {
                case ".mp3":
                    ReadMP3File(filename); 
                    break;
                case ".wav":
                    ReadWAVFile(filename); 
                    break;
                default:
                    ReadCommonFile(filename); 
                    break;
            }
        }
        private void ReadMP3File(string filename)
        {
            using (var reader = new Mp3FileReader(filename))
            {
                SetData(reader);
            }
        }
        private void ReadWAVFile(string filename)
        {
            using (var reader = new WaveFileReader(filename))
            {
                SetData(reader);
            }
        }
        private void ReadCommonFile(string filename)
        {
            using (var reader = new MediaFoundationReader(filename))
            {
               SetData(reader);
            }
        }
        private void SetData(WaveStream reader)
        {
            waveFormat = reader.WaveFormat;

            data = new byte[reader.Length];
            byte[] byteBuffer = new byte[byteBufferSize];
            int bytesRead = 0;
            int totalBytesRead = 0;
            while ((bytesRead = reader.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
            {
                var willBytes = (totalBytesRead + bytesRead);
                Array.Copy(byteBuffer, 0, data, totalBytesRead, willBytes > reader.Length ? (willBytes - reader.Length) : bytesRead);
                //for (int i = 0; i < bytesRead; ++i)
                //    data[totalBytesRead + i] = byteBuffer[i];
                totalBytesRead += bytesRead;
            }
        }
        public void ChangeAudioFormat(WaveFormat newWaveFormat)
        {
            using (MemoryStream inputStream = new MemoryStream(data))
            {
                using (WaveStream waveStream = new RawSourceWaveStream(inputStream, waveFormat))
                {
                    WaveFormatConversionStream conversionStream = new WaveFormatConversionStream(newWaveFormat, waveStream);

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, conversionStream.WaveFormat))
                        {
                            byte[] buffer = new byte[data.Length];
                            int totalBytesRead = 0;
                            int bytesRead = 0;
                            while ((bytesRead = conversionStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                waveFileWriter.Write(buffer, 0, bytesRead);
                                totalBytesRead += bytesRead;
                            }
                            data = new byte[totalBytesRead];
                            Buffer.BlockCopy(buffer, 0, data, 0, totalBytesRead);
                        }

                        waveFormat = newWaveFormat;
                    }
                }
            }
        }
    }
}
