using NNPTPZ1.NewtonFractals;
using NNPTPZ1.NewtonFractals.Mathematics;
using System.Collections.Generic;

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
            NewtonFractal newtonFractal = new NewtonFractal(
                Config.GetConfigFromArguments(args),
                new Polynomial() {
                    Coefficients = new List<ComplexNumber>() {
                        new ComplexNumber() { RealPart = 1 },
                        ComplexNumber.Zero,
                        ComplexNumber.Zero,
                        new ComplexNumber() { RealPart = 1 }
                    }
                }
                );
            newtonFractal.EvaluateToBitmap();
            newtonFractal.Save();
        }
    }
}
