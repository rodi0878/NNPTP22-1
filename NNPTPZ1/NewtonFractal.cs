using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;
using NNPTPZ1.Structure;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private static readonly Polynomial polynomial;
        private static readonly Color[] colors = new[]
            {
                Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        private Bitmap bitmap;
        private Size<int> bitmapSize;
        private MinMax<double> x;
        private MinMax<double> y;
        private double stepX;
        private double stepY;
        private string outputLocation;
        private List<ComplexNumber> roots;

        static NewtonFractal()
        {
            polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber() { RealPart = 1 });
        }

        public NewtonFractal(string[] args)
        {
            if (args.Length == 0)
            {
                ApplyDefaultValues();
                return;
            }

            try
            {
                bitmapSize = new Size<int>(int.Parse(args[0]), int.Parse(args[1]));
                x = new MinMax<double>(double.Parse(args[2]), double.Parse(args[3]));
                y = new MinMax<double>(double.Parse(args[4]), double.Parse(args[5]));
                outputLocation = args[6];

                CalculateSteps();
                ReloadBitmap();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong arguments.");
                Console.WriteLine("Arguments should be: width height xMin xMax yMin yMax outputLocation");
                System.Environment.Exit(1);
            }
        }

        private void ApplyDefaultValues()
        {
            bitmapSize = new Size<int>(300, 300);
            x = new MinMax<double>(-1, 1);
            y = new MinMax<double>(-1, 1);
            outputLocation = "../../../out.png";

            CalculateSteps();
            ReloadBitmap();
        }

        private void CalculateSteps()
        {
            stepX = (x.Max - x.Min) / bitmapSize.Width;
            stepY = (y.Max - y.Min) / bitmapSize.Height;
        }

        private void ReloadBitmap()
        {
            bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        }

        public void CalculateFractal()
        {
            roots = new List<ComplexNumber>();
            ReloadBitmap();

            for (int pixelPositionY = 0; pixelPositionY < bitmapSize.Width; pixelPositionY++)
            {
                for (int pixelPositionX = 0; pixelPositionX < bitmapSize.Height; pixelPositionX++)
                {
                    ComplexNumber point = new ComplexNumber()
                    {
                        RealPart = x.Min + pixelPositionX * stepX,
                        ImaginaryPart = y.Min + pixelPositionY * stepY
                    };

                    int iterationCount = FindIterationCountUsingNewtonIteration(ref point);
                    int rootNumber = FindRootNumber(point);
                    ColorizePixel(pixelPositionX, pixelPositionY, iterationCount, rootNumber);
                }
            }

            bitmap.Save(outputLocation);
        }

        private int FindIterationCountUsingNewtonIteration(ref ComplexNumber point)
        {
            int iterationCount = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polynomial.Evaluate(point).Divide(polynomial.Derive().Evaluate(point));
                point = point.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                iterationCount++;
            }
            return iterationCount;
        }

        private int FindRootNumber(ComplexNumber point)
        {
            bool known = false;
            int rootCount = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(point.RealPart - roots[i].RealPart, 2) + Math.Pow(point.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    rootCount = i;
                }
            }

            if (!known)
            {
                roots.Add(point);
                rootCount = roots.Count;
            }
            return rootCount;
        }

        private void ColorizePixel(int pixelPositionX, int pixelPositionY, int iterationCount, int rootNumber)
        {
            Color color = colors[rootNumber % colors.Length];
            color = Color.FromArgb(
                Math.Max(0, color.R - iterationCount * 2),
                Math.Max(0, color.G - iterationCount * 2),
                Math.Max(0, color.B - iterationCount * 2)
                );
            bitmap.SetPixel(pixelPositionX, pixelPositionY, color);
        }
    }
}
