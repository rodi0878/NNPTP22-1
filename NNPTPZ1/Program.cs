using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1 {
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program {
        static void Main(string[] args) {
            int imageWidth = 0, imageHeight = 0;
            double xMin = 0, xMax = 0, yMin = 0, yMax = 0;
            string imagePath = "";

            try {
                imageWidth = int.Parse(args[0]);
                imageHeight = int.Parse(args[1]);

                xMin = double.Parse(args[2]);
                xMax = double.Parse(args[3]);
                yMin = double.Parse(args[4]);
                yMax = double.Parse(args[5]);

                imagePath = args[6];
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Bitmap bmpImage = new Bitmap(imageWidth, imageHeight);

            double xStep = (xMax - xMin) / imageWidth;
            double yStep = (yMax - yMin) / imageHeight;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < imageWidth; i++) {
                for (int j = 0; j < imageHeight; j++) {
                    // find "world" coordinates of pixel
                    double x = xMin + j * xStep;
                    double y = yMin + i * yStep;

                    ComplexNumber complexPoint = new ComplexNumber() {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (complexPoint.RealPart == 0)
                        complexPoint.RealPart = 0.0001;
                    if (complexPoint.ImaginaryPart == 0)
                        complexPoint.ImaginaryPart = 0.0001f;

                    // find solution of equation using newton's iteration
                    int iteration = 0;
                    for (int k = 0; k < 30; k++) {
                        ComplexNumber divisionResult = polynomial.Eval(complexPoint).Divide(derivedPolynomial.Eval(complexPoint));
                        complexPoint = complexPoint.Subtract(divisionResult);

                        if (Math.Pow(divisionResult.RealPart, 2) + Math.Pow(divisionResult.ImaginaryPart, 2) >= 0.5) {
                            k--;
                        }
                        iteration++;
                    }

                    // find solution root number
                    bool known = false;
                    int id = 0;
                    for (int k = 0; k < roots.Count; k++) {
                        if (Math.Pow(complexPoint.RealPart - roots[k].RealPart, 2) + Math.Pow(complexPoint.ImaginaryPart - roots[k].ImaginaryPart, 2) <= 0.01) {
                            known = true;
                            id = k;
                        }
                    }
                    if (!known) {
                        roots.Add(complexPoint);
                        id = roots.Count;
                    }

                    // colorize pixel according to root number
                    Color selectedColor = colors[id % colors.Length];
                    selectedColor = Color.FromArgb(
                        Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                        Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                        Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255));
                    bmpImage.SetPixel(j, i, selectedColor);
                }
            }

            bmpImage.Save(imagePath ?? "../../../out.png");
        }
    }
}
