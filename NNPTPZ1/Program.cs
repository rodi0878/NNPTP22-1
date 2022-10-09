using System;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractalGenerator newtonFractalGenerator = new NewtonFractalGenerator(args);
            newtonFractalGenerator.Generate();

            Console.ReadKey();
        }
    }
}
