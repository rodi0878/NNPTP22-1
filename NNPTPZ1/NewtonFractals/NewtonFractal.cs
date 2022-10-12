using NNPTPZ1.NewtonFractals.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1.NewtonFractals
{
    public class NewtonFractal
    {
        private const int NewtonsIterationsAmount = 30;

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
            outputPaint.Save(string.IsNullOrEmpty(config.OutputFilename) ? "../../../out.png" : config.OutputFilename);
        }
        public void EvaluateToBitmap()
        {
            for (int i = 0; i < outputPaint.Width; i++)
            {
                for (int j = 0; j < outputPaint.Height; j++)
                {
                    ComplexNumber pixelPoint = new ComplexNumber()
                    {
                        RealPart = config.XMin + j * xStep,
                        ImaginaryPart = (float)(config.YMin + i * yStep)
                    };

                    if (pixelPoint.RealPart == 0)
                        pixelPoint.RealPart = 0.0001;
                    if (pixelPoint.ImaginaryPart == 0)
                        pixelPoint.ImaginaryPart = 0.0001f;

                    Color pixelColor = FindColorByNewtonsIteration(pixelPoint);

                    outputPaint.SetPixel(j, i, pixelColor);
                }
            }
        }
        private Color FindColorByNewtonsIteration(ComplexNumber point)
        {
            int iteratorCounter = 0;
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
            bool known = false;
            int colorIndexHelper = 0;
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
            Color pixelColor = colors[colorIndexHelper % colors.Length];

            return Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - (int)iteratorCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - (int)iteratorCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - (int)iteratorCounter * 2), 255));
        }
    }
}
