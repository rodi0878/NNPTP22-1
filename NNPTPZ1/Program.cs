using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            int imageWidth = int.Parse(args[0]);
            int imageHeight = int.Parse(args[1]);

            double xMin = double.Parse(args[2]);
            double xMax = double.Parse(args[3]);
            double yMin = double.Parse(args[4]);
            double yMax = double.Parse(args[5]);

            string imagePath = args[6];

            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
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

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber number = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (number.RealPart == 0)
                        number.RealPart = 0.0001;
                    if (number.ImaginaryPart == 0)
                        number.ImaginaryPart = 0.0001f;

                    float iteration = 0;
                    for (int k = 0; k < 30; k++)
                    {
                        var diff = polynomial.Evaluate(number).Divide(derivedPolynomial.Evaluate(number));
                        number = number.Subtract(diff);

                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            k--;
                        }
                        iteration++;
                    }

                    bool known = false;
                    int id = 0;
                    for (int k = 0; k < roots.Count; k++)
                    {
                        if (Math.Pow(number.RealPart - roots[k].RealPart, 2) + Math.Pow(number.ImaginaryPart - roots[k].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = k;
                        }
                    }

                    if (!known)
                    {
                        roots.Add(number);
                        id = roots.Count;
                    }

                    Color color = colors[id % colors.Length];
                    color = Color.FromArgb(
                        Math.Min(Math.Max(0, color.R - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, color.G - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, color.B - (int)iteration * 2), 255)
                    );

                    bitmap.SetPixel(j, i, color);
                }
            }

            bitmap.Save(imagePath ?? "../../../out.png");
        }
    }
}


