using System;
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

        private void Tone440Button_Click(object sender, EventArgs e)
        {
            ulong samplesPerSec = 44100;

            //ToneFunction pureTone440 =
            //    (start, current, end, rate) =>
            //    {
            //        double sample = Math.Sin(220 * 2.0 * Math.PI * (current - start) / rate);
            //        return sample;
            //    };

            ToneFunction pureTone440 =
                (start, current, end, rate) =>
                {
                    //(current - start)/rate is essentially just how many seconds have passed
                    double cycles = 2.0 * Math.PI * (current - start) / rate;
                    double sample = Math.Sin(220.0 * cycles);
                    sample += Math.Sin(220.0 * 3.0 / 2.0 * cycles);
                    sample += Math.Sin(220.0 * 5.0 / 4.0 * cycles);
                    sample += Math.Sin(220.0 * 7.0 / 4.0 * cycles);
                    sample += Math.Sin(220.0 * 9.0 / 8.0 * cycles);
                    sample += Math.Sin(220.0 * 11.0 / 8.0 * cycles);
                    sample += Math.Sin(220.0 * 13.0 / 8.0 * cycles);
                    sample += Math.Sin(220.0 * 15.0 / 8.0 * cycles);


                    //sample += Math.Sin(440.0 * dist);

                    sample /= 8.0;

                    return sample;
                };

            ulong startSample = 0;
            ulong endSample = samplesPerSec;

            ToneGenerator oneSecondOfPure440 = new ToneGenerator(startSample, endSample, pureTone440);


            List<ToneGenerator> generators = new List<ToneGenerator>();
            generators.Add(oneSecondOfPure440);


            TonePlayer player = new TonePlayer(samplesPerSec, 16);

            player.Play(generators);

        }
    }
}
