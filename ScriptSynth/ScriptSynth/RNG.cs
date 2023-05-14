using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSynth
{
    public class RNG
    {
        static Random _gen = new Random();

        public static double NextDouble() { return _gen.NextDouble(); }
    }
}
