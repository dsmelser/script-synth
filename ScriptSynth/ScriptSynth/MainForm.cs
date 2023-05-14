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
            //tonesDataGridView.Columns.Add(new DataGridViewTimeColumn());
            //tonesDataGridView.Columns.Add(new DataGridViewTimeColumn());
            //tonesDataGridView.Columns.Add("script", "Script");

            MessageBox.Show("Press OK to play tone");


            //var func = ToneGeneratorParser.ParseToneFunction("start * duration * index");

            //double result = func(2, 3, 5);

            //MessageBox.Show("result is : " + result);

            ulong samplesPerSec = 44100;

            
            ToneFunction pureTone440 = 
                (start, current, end, rate) => 
                    Math.Sin(440 * 2.0 * Math.PI * (current - start) / rate);

            ToneGenerator oneSecondOfPure440 = new ToneGenerator(0, 1 * samplesPerSec, pureTone440 );

            
            List<ToneGenerator> generators = new List<ToneGenerator>();
            generators.Add(oneSecondOfPure440);

            
            TonePlayer player = new TonePlayer(samplesPerSec, 16);

            player.Play(generators);

        }


    }
}
