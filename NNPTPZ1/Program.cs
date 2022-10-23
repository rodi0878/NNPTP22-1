using System;
using NNPTPZ1.Generators;

namespace NNPTPZ1
{
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length  < 6)
            {
                throw new ArgumentException("There should be 6 or 7 arguments\n(width, height, XMin, XMax, YMin, YMax, [outputFile])");
            }

            NewtonFractalGenerator.Generate(args);
        }
    }
}
