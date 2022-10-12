using NNPTPZ1.NewtonFractals.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1.NewtonFractals
{
    public class NewtonFractal
    {
        private const int NewtonsIterationsAmount = 30;
        private const string DefaultOutputFilename = "../../../out.png";

        private Config config;
        private double xStep;
        private double yStep;
        private Polynomial polynomial;
        private Polynomial polynomialDerivated;
        private Bitmap outputPaint;
        private Color[] colors;
        public List<ComplexNumber> Roots { get; set; } = new List<ComplexNumber>();
        public NewtonFractal(Config config, Polynomial polynomial)
        {
            this.config = config;

            outputPaint = new Bitmap(config.BitmapWidth, config.BitmapHeight);

            xStep = (config.XMax - config.XMin) / outputPaint.Width;
            yStep = (config.YMax - config.YMin) / outputPaint.Height;

            this.polynomial = polynomial;

            this.polynomialDerivated = polynomial.Derive();

            Console.WriteLine(this.polynomial);
            Console.WriteLine(this.polynomialDerivated);

            colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta

            };

        }
        public void Save()
        {
            /*Take default path if config output filename is not set*/
            string destinationPath = string.IsNullOrEmpty(config.OutputFilename) ? DefaultOutputFilename : config.OutputFilename;
            outputPaint.Save(destinationPath);
        }
        public void DrawToBitmap()
        {
            for (int i = 0; i < outputPaint.Width; i++)
            {
                for (int j = 0; j < outputPaint.Height; j++)
                {
                    outputPaint.SetPixel(j, i, FindColorByNewtonsIteration(new ComplexNumber()
                    {
                        RealPart = config.XMin + j * xStep,
                        ImaginaryPart = config.YMin + i * yStep
                    }));
                }
            }
        }

        private void AvoidDivisionByZeroWithSlightDifference(ComplexNumber point)
        {
            if (point.RealPart == 0)
                point.RealPart = 0.0001;
            if (point.ImaginaryPart == 0)
                point.ImaginaryPart = 0.0001f;
        }

        private void ApplyNewtonIteration(ComplexNumber point, out int iteratorCounter)
        {
            iteratorCounter = 0;
            for (int i = 0; i < NewtonsIterationsAmount; i++)
            {
                ComplexNumber difference = polynomial.Evaluate(point).Divide(polynomialDerivated.Evaluate(point));
                point = point.Subtract(difference);
                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                iteratorCounter++;
            }
        }

        private void FindFractalRoots(ComplexNumber point, out int colorIndexHelper)
        {
            bool known = false;
            colorIndexHelper = 0;
            for (int i = 0; i < Roots.Count; i++)
            {
                if (Math.Pow(point.RealPart - Roots[i].RealPart, 2) + Math.Pow(point.ImaginaryPart - Roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    colorIndexHelper = i;
                }
            }
            if (!known)
            {
                Roots.Add(point);
                colorIndexHelper = Roots.Count;
            }
        }

        private Color FindColorByNewtonsIteration(ComplexNumber point)
        {
            AvoidDivisionByZeroWithSlightDifference(point);

            ApplyNewtonIteration(point, out int iteratorCounter);

            FindFractalRoots(point, out int colorIndexHelper);
            
            Color pixelColor = colors[colorIndexHelper % colors.Length];

            return Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - (int)iteratorCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - (int)iteratorCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - (int)iteratorCounter * 2), 255));
        }
    }
}
