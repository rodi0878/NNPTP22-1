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
            NewtonFractal newtonFractal = new NewtonFractal(args);

            Console.WriteLine(newtonFractal.Polynomial);
            Console.WriteLine(newtonFractal.Polynomial.Derive());

            Console.WriteLine("Generating fractal..");
            newtonFractal.Generate();
            Console.WriteLine("Fractal image saved.");

            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }
    }
}
