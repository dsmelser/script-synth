using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSynth
{
    public class ToneCombiner
    {
        //private static readonly double tau = 2 * Math.PI;

        public static double[] GenerateAudioBuffer(ulong sampleRate, List<ToneGenerator> toneGenerators)
        {
            ulong maxStopIndex = 0;

            foreach (ToneGenerator toneGenerator in toneGenerators)
            {
                if (toneGenerator.EndIndex > maxStopIndex)
                {
                    maxStopIndex = toneGenerator.EndIndex;
                }
            }

            //ulong bufferLength = (sampleRate * maxStopIndex) / 1000L;
            ulong bufferLength = maxStopIndex;

            double[] buffer = new double[bufferLength];

            foreach (ToneGenerator toneGenerator in toneGenerators)
            {
                //ulong startSample = sampleRate * toneGenerator.StartTime / 1000L;
                //ulong stopSample = sampleRate * toneGenerator.StopTime / 1000L;

                ulong startSample = toneGenerator.StartIndex;
                ulong stopSample = toneGenerator.EndIndex;

                for (ulong i = startSample; i < stopSample; i++)
                {
                    //ulong timeInMs = (i- startSample) / sampleRate;
                    //double timeInSeconds = i / (double)sampleRate;
                    double sample = toneGenerator.Generate(startSample, i, stopSample, sampleRate);
                    buffer[i] += sample;
                }
            }

            //normalize the buffer to [-1, 1]
            double max = double.NegativeInfinity;
            double min = double.PositiveInfinity;

            foreach (double sample in buffer)
            {
                if (sample > max) max = sample;
                if (sample < min) min = sample;
            }

            double range = max - min;

            for (ulong i = 0; i < bufferLength; i++)
            {
                buffer[i] = (buffer[i] - min) / range * 2.0 - 1.0;
            }

            return buffer;
        }
    }
}
