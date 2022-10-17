using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    class NewtonFractal
    {
        Bitmap bitmap;

        private string imagePath;
        private int imageWidth;
        private int imageHeight;

        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        double xStep;
        double yStep;

        List<ComplexNumber> roots;
        Polynomial polynomial;
        Polynomial derivedPolynomial;

        public NewtonFractal(NewtonFractalParameters parameters)
        {
            this.imageWidth = parameters.ImageWidth;
            this.imageHeight = parameters.ImageHeight;
            this.xMin = parameters.XMin;
            this.xMax = parameters.XMax;
            this.yMin = parameters.YMin;
            this.yMax = parameters.YMax;

            bitmap = new Bitmap(imageWidth, imageHeight);
            xStep = (xMax - xMin) / imageWidth;
            yStep = (yMax - yMin) / imageHeight;

            roots = new List<ComplexNumber>();

            polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber[] { new ComplexNumber() { RealPart = 1 }, ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber() { RealPart = 1 } });
            derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);
        }

        public void CreateBitmap()
        {
            for (int i = 0; i < imageWidth; i++)        
                for (int j = 0; j < imageHeight; j++)
                {
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber number = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = y
                    };

                    if (number.RealPart == 0) number.RealPart = 0.0001;
                    if (number.ImaginaryPart == 0) number.ImaginaryPart = 0.0001f;

                    GetIteration(out int iteration, number);         
                    int rootNumber = GetRootNumber(number);
                    bitmap.SetPixel(j, i, GetPixelColor(rootNumber, iteration));
                }
        }

        private int GetIteration(out int iteration, ComplexNumber number)
        {
            iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polynomial.Evaluate(number).Divide(derivedPolynomial.Evaluate(number));
                number = number.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                    i--;

                iteration++;
            }

            return iteration;
        }

        private int GetRootNumber(ComplexNumber number)
        {
            bool known = false;
            int rootNumber = 0;
            for (int i = 0; i < roots.Count; i++)
                if (Math.Pow(number.RealPart - roots[i].RealPart, 2) + Math.Pow(number.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    rootNumber = i;
                }

            if (!known)
            {
                roots.Add(number);
                rootNumber = roots.Count;
            }

            return rootNumber;
        }

        public void SaveBitmap()
        {
            bitmap.Save(imagePath ?? "../../../out.png");
        }

        private Color GetPixelColor(int id, int iteration)
        {
            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            Color color = colors[id % colors.Length];
            return Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                Math.Min(Math.Max(0, color.B - iteration * 2), 255)
            );
        }

    }
}
