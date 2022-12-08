using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace NNPTPZ1
{
    class NewtonFractal
    {
        private static readonly double PixelInitialValue = 0.0001;
        private static readonly double RootAccuracy = 0.5;
        private static readonly double RootTolerance = 0.01;
        private static readonly int MaximumIterations = 30;
        private static readonly string DefaultOutputFile = "../../../out.png";

        private double xMin, xMax, xStep;
        private double yMin, yMax, yStep;
        string outputFile;
        private Bitmap bitmap;
        private static Polynomial polynomial, polynomialDerived;
        private List<ComplexNumber> roots = new List<ComplexNumber>();

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

            xStep = (xMax - xMin) / bitmap.Width;
            yStep = (yMax - yMin) / bitmap.Height;

            outputFile = (args.Length < 7 || args[6] is null) ? DefaultOutputFile : args[6];  
        }

        private static void PolynomialInitialization()
        {
            polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber(1, 0));

            polynomialDerived = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerived);

        }
        public ComplexNumber FindPixel(int x, int y)
        {

            ComplexNumber complexNumber = new ComplexNumber(xMin + x * xStep, yMin + y * yStep);

            if (complexNumber.Real == 0)
                complexNumber.Real = PixelInitialValue;
            if (complexNumber.Imaginary == 0)
                complexNumber.Imaginary = PixelInitialValue;

            return complexNumber;
        }

        public void CreateFractalBitmap()
        {
            for (int y = 0; y < bitmap.Width; y++)
            {
                for (int x = 0; x < bitmap.Height; x++)
                {
                    ComplexNumber complexNumber = FindPixel(x, y);
                    ColorizePixel(x, y, FindSolutionRootNumber(FindSolutionOfEquation(complexNumber)), MaximumIterations);

                }
            }
            SaveImage();
        }

        private ComplexNumber FindSolutionOfEquation(ComplexNumber complexNumber)
        {
            for (int iIteration = 0; iIteration < MaximumIterations; iIteration++)
            {
                var difference = polynomial.Evaluate(complexNumber).Divide(polynomialDerived.Evaluate(complexNumber));
                complexNumber = complexNumber.Subtract(difference);


                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= RootAccuracy)
                {
                    iIteration--;
                }

            }
            return complexNumber;
        }

        private int FindSolutionRootNumber(ComplexNumber complexNumber)
        {
            var known = false;
            int rootCount = 0;
            for (int count = 0; count < roots.Count; count++)
            {
                if (Math.Pow(complexNumber.Real - roots[count].Real, 2) + Math.Pow(complexNumber.Imaginary - roots[count].Imaginary, 2) <= RootTolerance)
                {
                    known = true;
                    rootCount = count;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                rootCount = roots.Count;
            }
            return rootCount;
        }

        private void ColorizePixel(int x, int y, int rootCount, int iterations)
        {
            var color = colors[rootCount % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterations * 2), 255), Math.Min(Math.Max(0, color.G - iterations * 2), 255), Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            bitmap.SetPixel(x, y, color);
        }

        public void SaveImage()
        {
            bitmap.Save(outputFile);
        }

    }
}
