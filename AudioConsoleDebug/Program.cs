using AudioFingerprinting_ver_1;
using NAudio.Wave;
using NWaves.FeatureExtractors.Options;
using NWaves.FeatureExtractors;
using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string[]> audioFiles = new List<string[]>();
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track1.mp3", "Три дня дождя - Ищу тебя"});
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track2.mp3", "VØJ, Narvent - Memory Reboot" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track3.mp3", "Neverlove - Волшебник" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track4.mp3", "weeklyn - I Don't Wanna Talk to People" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track5.mp3", "IVOXYGEN - ROOM (TIERRA Remix)" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track6.mp3", "Falling In Reverse - The Drug In Me Is Reimagined" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track7.mp3", "Epidemic Sound - Once Upon A Time In Detroit 1 (Remix Version)" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track8.mp3", "Три дня дождя - Слёзы на ветер" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track9.mp3", "Ice Nine Kills - The Shower Scene (Acoustic)" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track10.mp3", "Кишлак - Кодировка" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track11.mp3", "Кишлак - Мнение" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track12.mp3", "Мэйби Бэйби - Москоу Невер Слипс" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track13.mp3", "Erik Satie - Gymnopédie No. 3" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track14.mp3", "Eminem feat. Juice WRLD - Godzilla" });
        audioFiles.Add(new string[]{ "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\track15.mp3", "SLAVA MARLOW - Забуду" });

        
        // Build needed wave format
        WaveFormatBuilder builder = new WaveFormatBuilder();
        builder.SetChannels(1);
        builder.SetBitsPerSample(16);
        builder.SetSampleRate(44000);
        WaveFormat waveFormat = builder.GetWaveFormat();

        // Create options of mfcc
        var mfccOptions = new MfccOptions
        {
            SamplingRate = waveFormat.SampleRate,
            FeatureCount = 13,
            FrameDuration = 0.025/*sec*/,
            HopDuration = 0.010/*sec*/,
            FilterBankSize = 26,
            PreEmphasis = 0.97,
            Window = NWaves.Windows.WindowType.Hamming
        };
        var mfccExtractor = new MfccExtractor(mfccOptions);

        //Console.WriteLine("Создаю отпечатки треков");
        List<List<float[]>> mfccVectors = new List<List<float[]>>();
        List<BitArray> mfccCompactVectors = new List<BitArray>();

        for (int i = 0; i < audioFiles.Count; ++i)
        {
            AudioData audioData = new AudioData(audioFiles[i][0]);
            audioData.ChangeAudioFormat(waveFormat);
            float[] audioSamples = audioData.GetSamples();

            // Read mfcc from file
            string mfccFileName = "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\bins\\" + audioFiles[i][1] + "_mfcc.bin";
            string mfccCompactFileName = "Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\bins\\compact_" + audioFiles[i][1] + "_mfcc.bin";
            if (File.Exists(mfccFileName))
            {
                //Console.WriteLine("Файл для " + audioFiles[i][1] + " уже существует, считываю из файла");
                using (BinaryReader reader = new BinaryReader(File.Open(mfccFileName, FileMode.Open)))
                {
                    // Чтение размерности матрицы
                    int rows = reader.ReadInt32();
                    int cols = reader.ReadInt32();
                    List<float[]> mfcc = new List<float[]>();

                    // Чтение элементов матрицы
                    for (int k = 0; k < rows; k++)
                    {
                        mfcc.Add(new float[cols]);
                        for (int j = 0; j < cols; j++)
                        {
                            mfcc[k][j] = reader.ReadSingle();
                        }
                    }
                    mfccVectors.Add(mfcc);
                }
                BitArray compactMfcc = new CompactMFCC().ReadCompactMFCCFromFile(mfccCompactFileName);
                mfccCompactVectors.Add(compactMfcc);
            }
            else
            {
                //Console.WriteLine("Создаю отпечаток для " + audioFiles[i][1]);
                var mfcc = mfccExtractor.ComputeFrom(audioSamples);
                CompactMFCC compactMFCC = new CompactMFCC(mfcc);
                var compactMfcc = compactMFCC.ComputeCompactMFCC();
                compactMFCC.WriteCompactMFCCToFile(mfccCompactFileName);
                using (BinaryWriter writer = new BinaryWriter(File.Open(mfccFileName, FileMode.Create)))
                {
                    int rows = mfcc.Count;
                    int cols = mfcc[0].Length;

                    // Запись размерности матрицы
                    writer.Write(rows);
                    writer.Write(cols);

                    // Запись элементов матрицы
                    foreach (var row in mfcc)
                    {
                        foreach (var element in row)
                        {
                            writer.Write(element);
                        }
                    }
                }
                mfccVectors.Add(mfcc);
                mfccCompactVectors.Add(compactMfcc);
            }
            //Console.WriteLine("Отпечаток для " + audioFiles[i][1] + " создан/считан");
        }

        List<string> audioSamlpes = new List<string>();
        audioSamlpes.Add("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\example1.1_10sec_4.mp3"); // example1.1_10sec_4.mp3
        audioSamlpes.Add("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track2.mp3");
        audioSamlpes.Add("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track5.mp3");
        audioSamlpes.Add("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track6.mp3");

        Console.WriteLine("Выберете вариант сэмпла\n1 - Три дня дождя - Ищу тебя\n2 - VØJ, Narvent - Memory Reboot\n3 - IVOXYGEN - ROOM (TIERRA Remix)\n4 - Falling In Reverse - The Drug In Me Is Reimagined\n");
        int input = -1;
        while (input < 1 || input > 4)
        {
            input = Int32.Parse(Console.ReadLine());
        }

        string samplePath = audioSamlpes[input - 1];
      
        AudioData audioSample = new AudioData(samplePath);
        
        audioSample.ChangeAudioFormat(waveFormat);
        audioSample.PlayAudioData();
        Console.WriteLine("Рассчитываю отпечаток для выбранного сэмпла");
        float[] samples = audioSample.GetSamples();
        var mfccVector = mfccExtractor.ComputeFrom(samples);
        var mfccCompactVector = new CompactMFCC(mfccVector).ComputeCompactMFCC();
        Console.WriteLine("Создание отпечатка завершено\nНачинаю сравение");

        //EuclidFingerprintComparer comparer = new EuclidFingerprintComparer();
        //CompareResult[] results = new CompareResult[audioFiles.Count];
        //Parallel.For(0, audioFiles.Count, i =>
        //    {
        //        results[i] = comparer.Compare(mfccVectors[i], mfccVector);
        //        results[i].trackName = audioFiles[i][1];
        //        Console.WriteLine("Сравнение для " + results[i].trackName + " завершено");
        //    }
        //);

        //results = results.OrderBy(i => i.distance).ToArray();

        //Console.WriteLine("Результаты сравнения:\n");
        //for (int i = 0; i < results.Length; ++i)
        //    Console.WriteLine(results[i].trackName + " - Степень соответствия = " + results[i].distance + ", Начало соответствия (фрейм) = " + results[i].index + "\n");

        Console.WriteLine("СРАВНЕНИЕ ДЛЯ КОМПАКТНЫХ");

        EuclidCompactFingerprintComparer compactComparer = new EuclidCompactFingerprintComparer();
        CompareResult[] compactResults = new CompareResult[audioFiles.Count];
        Parallel.For(0, audioFiles.Count, i =>
        {
            compactResults[i] = compactComparer.Compare(mfccCompactVectors[i], mfccCompactVector);
            compactResults[i].trackName = audioFiles[i][1];
            Console.WriteLine("Сравнение для " + compactResults[i].trackName + " завершено");
        }
        );

        compactResults = compactResults.OrderBy(i => i.distance).ToArray();

        Console.WriteLine("Результаты сравнения:\n");
        for (int i = 0; i < compactResults.Length; ++i)
            Console.WriteLine(compactResults[i].trackName + " - Степень соответствия = " + compactResults[i].distance + ", Начало соответствия (фрейм) = " + compactResults[i].index + "\n");


        Console.ReadKey();
    }
}
