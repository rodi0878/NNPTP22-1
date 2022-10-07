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
            var newtonFractalGenerator = ParseArgumentsAndReturnNewNewtonFractalGenerator(args);
            var newtonFractal = newtonFractalGenerator.Generate();

            string outputFileName = args[6];
            newtonFractal.Save(outputFileName ?? "../../../out.png");
        }

        private static NewtonFractalGenerator ParseArgumentsAndReturnNewNewtonFractalGenerator(string[] args)
        {
            int canvasWidth = int.Parse(args[0]);
            int canvasHeight = int.Parse(args[1]);
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }

            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];
            return new NewtonFractalGenerator(canvasWidth, canvasHeight, xmin, xmax, ymin, ymax);
        }
    }
}