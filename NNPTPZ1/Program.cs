using System;


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
            try
            {
                int imageWidth;
                int imageHeight;
                double xMin;
                double xMax;
                double yMin;
                double yMax;
                string pathToImageFile;
                if (args.Length == 7)
                {
                    imageWidth = int.Parse(args[0]);
                    imageHeight = int.Parse(args[1]);
                    xMin = double.Parse(args[2]);
                    xMax = double.Parse(args[3]);
                    yMin = double.Parse(args[4]);
                    yMax = double.Parse(args[5]);
                    pathToImageFile = args[6];
                }
                else
                {
                    imageWidth = 500;
                    imageHeight = 500;
                    xMin = -38;
                    xMax = 68;
                    yMin = -10;
                    yMax = 33;
                    pathToImageFile = "../../../out.png";
                }


                FractalGenerator fractalVisualiser = new FractalGenerator(imageWidth, imageHeight, xMin, xMax, yMin, yMax);
                fractalVisualiser.GenerateFractalImage(pathToImageFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid number of arguments or invalid argument type");
            }



        }
    }
}




