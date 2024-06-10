using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioFingerprinting_ver_1
{
    public class Frames
    {
        private float[] samples; // Samples of audio
        private float[][] frames; // Resulted frames

        private int sampleRate; // Sample rate of audio
        private float frameSizeInMilliseconds; // Frame size in milliseconds
        private int frameSizeInSamples; // Frame size in count of samples
        private float hopDuration; // Hop duration in milliseconds
        private int hopDurationInSamples; // Hop duration in milliseconds
        public Frames(float frameSizeInMilliseconds, float hopDuration)
        {
            this.frameSizeInMilliseconds = frameSizeInMilliseconds;
            this.hopDuration = hopDuration;
        }

        public Frames(float[] samples, int sampleRate, float frameSizeInMilliseconds, float hopDuration) 
        {
            this.samples = samples;
            this.sampleRate = sampleRate;
            this.frameSizeInMilliseconds = frameSizeInMilliseconds;
            this.hopDuration = hopDuration;
            CalculateFrameSizeInSamples();
            CreateFrames();
        }

        public void SetSamples(float[] samples, int sampleRate)
        {
            this.samples = samples;
            this.sampleRate = sampleRate;
            CalculateFrameSizeInSamples();
            CreateFrames();
        }

        public float[][] GetFrames()
        {
            return frames;
        }
        
        private void CalculateFrameSizeInSamples()
        {
            if (samples == null)
                return;

            frameSizeInSamples = (int)Math.Floor((sampleRate / 1000.0f) * frameSizeInMilliseconds);
            int minFrameSizeOfPowerTwo = (int)Math.Floor(Math.Log2(frameSizeInSamples));
            if (Math.Pow(2, minFrameSizeOfPowerTwo) < frameSizeInSamples)
                frameSizeInSamples = (int)(Math.Pow(2, minFrameSizeOfPowerTwo + 1));
            hopDurationInSamples = (int)(frameSizeInSamples / (frameSizeInMilliseconds / hopDuration)) + 1;
        }

        private void CreateFrames()
        {
            int framesCount = (int)(samples.Length / hopDurationInSamples);
            frames = new float[framesCount][];
            int currentSample = 0;
            int currentFrame = 0;
            while (currentSample + frameSizeInSamples < samples.Length)
            {
                frames[currentFrame] = new float[frameSizeInSamples];
                for (int j = 0; j < frameSizeInSamples; ++j)
                    frames[currentFrame][j] = samples[currentSample + j];
                currentFrame++;
                currentSample += hopDurationInSamples;
            }

            int leftFrames = samples.Length - currentSample;
            frames[currentFrame] = new float[frameSizeInSamples];
            for (int i = 0; i < frameSizeInSamples; i++)
            {
                if (i < leftFrames)
                    frames[currentFrame][i] = samples[currentSample + i];
                else
                    frames[currentFrame][i] = 0.0f;
            }
        }
    }
}
