using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalGenerator
    {
        private static readonly Color[] Colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
            Color.Magenta
        };

        private static readonly int MaxIterations = 30;
        private static readonly double Tolerance = 0.01;

        private int canvasWidth;
        private int canvasHeight;

        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        private double yStep;
        private double xStep;

        private Bitmap bitmap;

        private List<ComplexNumber> roots;
        private Polynome polynome;
        private Polynome polynomeDerived;

        public NewtonFractalGenerator(int canvasWidth, int canvasHeight, double xMin, double xMax, double yMin,
            double yMax)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;

            xStep = (xMax - xMin) / canvasWidth;
            yStep = (yMax - yMin) / canvasHeight;
            bitmap = new Bitmap(canvasWidth, canvasHeight);
            roots = new List<ComplexNumber>();

            polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomeDerived = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(polynomeDerived);
        }

        public Bitmap Generate()
        {
            for (int yPosition = 0; yPosition < canvasWidth; yPosition++)
            {
                for (int xPosition = 0; xPosition < canvasHeight; xPosition++)
                {
                    // find "world" coordinates of pixel
                    var worldCoordinatesOfPixel = FindWorldCoordinatesOfPixel(yPosition, xPosition);

                    // find solution of equation using newton's iteration
                    var iterationNumber = NewtonsIteration(ref worldCoordinatesOfPixel);

                    // find solution root number
                    var rootNumber = FindRootNumber(worldCoordinatesOfPixel);

                    ColorizePixelByRootNumber(rootNumber, iterationNumber, xPosition, yPosition);
                }
            }

            return bitmap;
        }

        private int NewtonsIteration(ref ComplexNumber complexNumber)
        {
            int iterations = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                var diff = polynome.Eval(complexNumber).Divide(polynomeDerived.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                {
                    i--;
                }

                iterations++;
            }

            return iterations;
        }

        private ComplexNumber FindWorldCoordinatesOfPixel(int yPosition, int xPosition)
        {
            double y = yMin + yPosition * yStep;
            double x = xMin + xPosition * xStep;

            ComplexNumber worldCoordinatesOfPixel = new ComplexNumber()
            {
                Real = x,
                Imaginary = y
            };

            if (worldCoordinatesOfPixel.Real == 0)
                worldCoordinatesOfPixel.Real = 0.0001;
            if (worldCoordinatesOfPixel.Imaginary == 0)
                worldCoordinatesOfPixel.Imaginary = 0.0001;
            return worldCoordinatesOfPixel;
        }

        private int FindRootNumber(ComplexNumber ox)
        {
            bool known = false;
            int rootNumber = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (Math.Pow(ox.Real - roots[w].Real, 2) + Math.Pow(ox.Imaginary - roots[w].Imaginary, 2) <= Tolerance)
                {
                    known = true;
                    rootNumber = w;
                }
            }

            if (!known)
            {
                roots.Add(ox);
                rootNumber = roots.Count;
            }

            return rootNumber;
        }

        private void ColorizePixelByRootNumber(int rootNumber, int iteration, int xPosition, int yPosition)
        {
            var color = Colors[rootNumber % Colors.Length];
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                Math.Min(Math.Max(0, color.B - iteration * 2), 255));
            bitmap.SetPixel(xPosition, yPosition, color);
        }
    }
}