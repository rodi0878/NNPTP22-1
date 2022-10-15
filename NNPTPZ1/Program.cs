using NNPTPZ1.NewtonFractal;

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
            ParsedArgumentData argumentData = new ParsedArgumentData(args);
            NewtonFractalGenerator newtonFractalGenerator = new NewtonFractalGenerator(argumentData);
            newtonFractalGenerator.Generate();
        }
    }
}