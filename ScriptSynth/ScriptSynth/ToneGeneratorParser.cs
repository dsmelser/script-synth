using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ScriptSynth
{
    public class ToneGeneratorParser
    {
        private static readonly CultureInfo cultureInfo = CultureInfo.InvariantCulture;
        private static readonly TimeSpan zeroTimeSpan = TimeSpan.Zero;

        public static List<ToneGenerator> ParseFile(string filePath, ulong sampleRate)
        {
            List<ToneGenerator> toneGenerators = new List<ToneGenerator>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length < 3) continue;

                TimeSpan startTime = ParseTime(parts[0]);
                TimeSpan stopTime = ParseTime(parts[1]);
                ToneFunction toneFunction = ParseToneFunction(string.Join(" ", parts.Skip(2)));

                toneGenerators.Add(new ToneGenerator((ulong)startTime.TotalMilliseconds * sampleRate, (ulong)stopTime.TotalMilliseconds * sampleRate, toneFunction));
            }

            return toneGenerators;
        }

        private static TimeSpan ParseTime(string timeString)
        {
            if (!TimeSpan.TryParseExact(timeString, @"m\:ss", cultureInfo, out TimeSpan time))
            {
                time = zeroTimeSpan;
            }

            return time;
        }

        //private static ToneFunction ParseToneFunction(string luaCode)
        //{
        //    // Add your implementation for parsing a Lua function here
        //    return null;
        //}

        public static ToneFunction ParseToneFunction(
            string toneFunctionString)
        {

            Type[] parameterTypes = new Type[] { typeof(ulong), typeof(ulong), typeof(ulong) };

            var parameters = new[] {
                Expression.Parameter(typeof(ulong), "start"),
                Expression.Parameter(typeof(ulong), "duration"),
                Expression.Parameter(typeof(ulong), "index"),
                Expression.Parameter(typeof(ulong), "rate")
            };


            
        string lambdaString = "(start, duration, index) => " + toneFunctionString;
            LambdaExpression lambdaExpression =
                //DynamicExpressionParser.ParseLambda<ToneFunction>(parameters, typeof(double),lambdaString);
                //return ParseLambda(null, createParameterCtor: true, parameters, resultType, expression, values);
                DynamicExpressionParser.ParseLambda(
                    null, 
                    createParameterCtor: true, 
                    parameters, 
                    typeof(double),
                    toneFunctionString);
            var compiled = lambdaExpression.Compile();
            return (s,d,i,rate) => (double)compiled.DynamicInvoke(s,d,i,rate);
        }
    }
}
