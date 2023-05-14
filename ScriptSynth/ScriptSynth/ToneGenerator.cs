using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSynth
{
    public delegate double ToneFunction(
        ulong start,
        ulong current,
        ulong end,
        ulong samplesPerSecond);

    public class ToneGenerator
    {
        public ulong StartIndex { get; set; }
        public ulong EndIndex { get; set; }

        public ToneFunction Generate { get; set; }

        public ToneGenerator(ulong startIndex, ulong endIndex, ToneFunction toneFunction)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Generate = toneFunction;
        }

    }
}
