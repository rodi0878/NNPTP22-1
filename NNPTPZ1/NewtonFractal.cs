using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1;

namespace INPTPZ1
{

    class NewtonFractal
    {
        private static readonly double PIXEL_INITIAL_VALUE = 0.0001;
        private static readonly double ROOT_ACCURACY = 0.5;
        private static readonly double ROOT_TOLERANCE = 0.01;
        private static readonly int MAXIMUM_ITERATIONS = 30;
        private static readonly string DEFAULT_OUTPUT_FILE = "../../../out.png";

        private double xMin, xMax, xStep;
        private double yMin, yMax, yStep;
        string outputFile;
        private Bitmap bitmap;
        private static Polynomial polynomial, polynomialDerived;

        private readonly static Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public NewtonFractal(string[] args)
        {
            ParseArguments(args);
            PolynomialInitialization();
        }

        private void ParseArguments(string[] args)
        {
            bitmap = new Bitmap(int.Parse(args[0]), int.Parse(args[1]));

            xMin = double.Parse(args[2]);
            xMax = double.Parse(args[3]);
            yMin = double.Parse(args[4]);
            yMax = double.Parse(args[5]);

            outputFile = args[6];
            xStep = (xMax - xMin) / bitmap.Width;
            yStep = (yMax - yMin) / bitmap.Height;
        }

        private static void PolynomialInitialization()
        {
            polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber(1, 0));

            polynomialDerived = polynomial.Derive();

        }

        public void CreateFractalImage()
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    ComplexNumber pixel = FindPixel(i, j);
                    int iteration = FindSolution(ref pixel);
                    int index = FindRootIndex(roots, pixel);
                    ColorizePixel(i, j, iteration, index);
                }
            }
        }

        private static int FindRootIndex(List<ComplexNumber> roots, ComplexNumber pixel)
        {
            for (int i = 0; i < roots.Count; i++) {
                if (Math.Pow(pixel.Real - roots[i].Real, 2) + Math.Pow(pixel.Imaginary - roots[i].Imaginary, 2) <= ROOT_TOLERANCE) {
                    return i;
                }
            }
            roots.Add(pixel);
            return roots.Count;
        }

        private static int FindSolution(ref ComplexNumber pixel)
        {
            int iterations = 0;
            for (int i = 0; i < MAXIMUM_ITERATIONS; i++) {
                ComplexNumber diffrenece = polynomial.Evaluate(pixel).Divide(polynomialDerived.Evaluate(pixel));
                pixel = pixel.Subtract(diffrenece);
                if (Math.Pow(diffrenece.Real, 2) + Math.Pow(diffrenece.Imaginary, 2) >= ROOT_ACCURACY) {
                    i--;
                }
                iterations++;
            }

            return iterations;
        }

        public ComplexNumber FindPixel(int x, int y)
        {

            ComplexNumber point = new ComplexNumber(xMin + x * xStep, yMin + y * yStep);

            if (point.Real == 0)
                point.Real = PIXEL_INITIAL_VALUE;
            if (point.Imaginary == 0)
                point.Imaginary = PIXEL_INITIAL_VALUE;

            return new ComplexNumber(x, y);
        }

        private void ColorizePixel(int x, int y, int index, int iterations)
        {
            Color color = colors[index % colors.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iterations * 2), 255),
                Math.Min(Math.Max(0, color.G - iterations * 2), 255),
                Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            bitmap.SetPixel(x, y, color);
        }

        public void SaveImage()
        {
            bitmap.Save(outputFile ?? DEFAULT_OUTPUT_FILE);
        }
    }

}
