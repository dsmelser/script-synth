﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptSynth
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        ToneFunction pureTone440 =
        (start, current, end, rate) =>
        {
            double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
            return sample;
        };

        ToneFunction thirdHarmonic =
        (start, current, end, rate) =>
        {
            double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
            sample += Math.Sin(440.0 * 3.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
            return sample;
        };

        ToneFunction fifthHarmonic =
        (start, current, end, rate) =>
        {
            double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
            sample += Math.Sin(440.0 * 5.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
            return sample;
        };

        ToneFunction seventhHarmonic =
(start, current, end, rate) =>
{
    double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 7.0 / 8.0 * 2.0 * Math.PI * (current - start) / rate);
    return sample;
};

        ToneFunction thirdAndFifthChord =
(start, current, end, rate) =>
{
    double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 3.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 5.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    return sample;
};

        ToneFunction thirdAndSeventhChord =
(start, current, end, rate) =>
{
double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
sample += Math.Sin(440.0 * 3.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 7.0 / 8.0 * 2.0 * Math.PI * (current - start) / rate);
    return sample;
};

        ToneFunction fifthAndSeventhChord =
(start, current, end, rate) =>
{
double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 5.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 7.0 / 8.0 * 2.0 * Math.PI * (current - start) / rate);
return sample;
};

        ToneFunction thirdFifthAndSeventhChord =
(start, current, end, rate) =>
{
    double sample = Math.Sin(440.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 3.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 5.0 / 4.0 * 2.0 * Math.PI * (current - start) / rate);
    sample += Math.Sin(440.0 * 7.0 / 8.0 * 2.0 * Math.PI * (current - start) / rate);
    return sample;
};




        private void Tone440Button_Click(object sender, EventArgs e)
        {


            //ToneFunction pureTone440 =
            //    (start, current, end, rate) =>
            //    {

            //        //(current - start)/rate is essentially just how many seconds have passed
            //        double cycles = 2.0 * Math.PI * (current - start) / rate;
            //        double sample = Math.Sin(220.0 * cycles);
            //        sample += Math.Sin(220.0 * 3.0 / 2.0 * cycles) / 3.0;
            //        sample += Math.Sin(220.0 * 5.0 / 4.0 * cycles) / 5.0;
            //        sample += Math.Sin(220.0 * 7.0 / 4.0 * cycles) / 7.0;
            //        sample += Math.Sin(220.0 * 9.0 / 8.0 * cycles) / 9.0;
            //        sample += Math.Sin(220.0 * 11.0 / 8.0 * cycles) / 11.0;
            //        sample += Math.Sin(220.0 * 13.0 / 8.0 * cycles) / 13.0;
            //        sample += Math.Sin(220.0 * 15.0 / 8.0 * cycles) / 15.0;


            //        //sample += Math.Sin(440.0 * dist);

            //        //sample /= 8.0;

            //        sample /= 1.0 + 1.0 / 3.0 + 1.0 / 5.0 + 1.0 / 7.0 + 1.0 / 9.0 + 1.0 / 11.0 + 1.0 / 13.0 + 1.0 / 15.0;

            //        return sample;
            //    };


            //ToneFunction pureTone440 =
            //    (start, current, end, rate) =>
            //    {
            //        //base frequency goes from 220 to 110 over the course of the sample
            //        //although REALLY this should be an exponential curve, not linear
            //        //sliding linearly makes all of the shift happen at the one end of the sample
            //        //double freq = 220.0 + 220.0 * (current - start) / (end - start);

            //        //let's try exponential
            //        double percentOfSound = ((double)(current - start)) / ((double)(end - start));
            //        double freq = 220.0 * Math.Pow(0.5, percentOfSound);
            //        double cycles = 2.0 * Math.PI * (current - start) / rate;

            //        double weight = 1.0;


            //        double sample = 0.0;
            //        //let's add some white noise
            //        //double sample = 0.05 * (2.0 * RNG.NextDouble() - 1.0);

            //        sample += Math.Sin(freq * cycles); weight += 1.0;
            //        sample += Math.Sin(freq * 3.0 * cycles) / 3.0; weight += 1.0/3.0;   //some 3x harmonics
            //        //sample += Math.Sin(freq * 7.0 / 4.0 * cycles); weight += 1.0;   //some 7x harmonics
            //        //sample += Math.Sin(freq * 49.0 / 32.0 * cycles); weight += 1.0;   //some 7x harmonics of those harmonics
            //        sample /= weight;

            //        return sample;
            //    };

            double seconds = 1.0;
            ulong bitsPerSecond = 16;
            ulong samplesPerSec = 44100;

            ulong startSample = 0;
            ulong endSample = (ulong)(samplesPerSec * seconds);


            ToneGenerator oneSecondOfPure440 = new ToneGenerator(startSample, endSample, pureTone440);
            ToneGenerator oneSecondOfThirdHarmonic = new ToneGenerator(endSample, endSample * 2, thirdHarmonic);
            ToneGenerator oneSecondOfFifthHarmonic = new ToneGenerator(endSample * 2, endSample * 3, fifthHarmonic);
            ToneGenerator oneSecondOfSeventhHarmonic = new ToneGenerator(endSample * 3, endSample * 4, seventhHarmonic);
            ToneGenerator oneSecondOfThirdAndFifthChord = new ToneGenerator(endSample * 4, endSample * 5, thirdAndFifthChord);
            ToneGenerator oneSecondOfThirdAndSeventhChord = new ToneGenerator(endSample * 5, endSample * 6, thirdAndSeventhChord);
            ToneGenerator oneSecondOfFifthAndSeventhChord = new ToneGenerator(endSample * 6, endSample * 7, fifthAndSeventhChord);
            ToneGenerator oneSecondOfThirdFifthAndSeventhChord = new ToneGenerator(endSample * 7, endSample * 8, thirdFifthAndSeventhChord);

            List<ToneGenerator> generators = new List<ToneGenerator>();
            generators.Add(oneSecondOfPure440);
            generators.Add(oneSecondOfThirdHarmonic);
            generators.Add(oneSecondOfFifthHarmonic);
            generators.Add(oneSecondOfSeventhHarmonic);
            generators.Add(oneSecondOfThirdAndFifthChord);
            generators.Add(oneSecondOfThirdAndSeventhChord);
            generators.Add(oneSecondOfFifthAndSeventhChord);
            generators.Add(oneSecondOfThirdFifthAndSeventhChord);


            TonePlayer player = new TonePlayer(samplesPerSec, bitsPerSecond);

            player.Play(generators);

        }
    }
}
