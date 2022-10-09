using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalGenerator
    {
        private int ImageWidth { get; set; }
        private int ImageHeight { get; set; }
        private double XMin { get; set; }
        private double XMax { get; set; }
        private double YMin { get; set; }
        private double YMax { get; set; }
        private string OutputPath { get; set; }
        private Polynomial Polynomial { get; set; }
        private Polynomial DerivedPolynomial { get; set; }
        private List<ComplexNumber> Roots { get; set; }
        private Color[] Colors { get; set; }

        public NewtonFractalGenerator(string[] args)
        {
            LoadArguments(args);
            CreatePolynomials();
            Roots = new List<ComplexNumber>();
            Colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }

        private void LoadArguments(string[] args)
        {
            ImageWidth = int.Parse(args[0]);
            ImageHeight = int.Parse(args[1]);
            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);
            OutputPath = args[6];
        }

        private void CreatePolynomials()
        {
            Polynomial = new Polynomial(new List<ComplexNumber> {
                new ComplexNumber { RealPart = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber { RealPart = 1 }
            });
            DerivedPolynomial = Polynomial.Derive();

            Console.WriteLine(Polynomial);
            Console.WriteLine(DerivedPolynomial);
        }

        public void Generate()
        {
            int maxIterations = 30;
            int maxColorDepth = 255;
            Bitmap bitmap = new Bitmap(ImageWidth, ImageHeight);
            double xStep = (XMax - XMin) / ImageWidth;
            double yStep = (YMax - YMin) / ImageHeight;
            // for every pixel in image...
            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    // find "world" coordinates of pixel
                    double x = XMin + j * xStep;
                    double y = YMin + i * yStep;

                    ComplexNumber complexNumber = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = y
                    };

                    if (complexNumber.RealPart == 0)
                    {
                        complexNumber.RealPart = 0.0001;
                    }
                    if (complexNumber.ImaginaryPart == 0)
                    {
                        complexNumber.ImaginaryPart = 0.0001f;
                    }

                    // find solution of equation using newton's iteration
                    int iterations = 0;
                    for (int k = 0; k < maxIterations; k++)
                    {
                        ComplexNumber diffComplexNumber = Polynomial.Eval(complexNumber).Divide(DerivedPolynomial.Eval(complexNumber));
                        complexNumber = complexNumber.Subtract(diffComplexNumber);
                        if (Math.Pow(diffComplexNumber.RealPart, 2) + Math.Pow(diffComplexNumber.ImaginaryPart, 2) >= 0.5)
                        {
                            k--;
                        }
                        iterations++;
                    }

                    // find solution root number
                    bool known = false;
                    int id = 0;
                    for (int k = 0; k < Roots.Count; k++)
                    {
                        if (Math.Pow(complexNumber.RealPart - Roots[k].RealPart, 2) + Math.Pow(complexNumber.ImaginaryPart - Roots[k].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = k;
                        }
                    }
                    if (!known)
                    {
                        Roots.Add(complexNumber);
                        id = Roots.Count;
                    }

                    // colorize pixel according to root number
                    Color color = Colors[id % Colors.Length];
                    color = Color.FromArgb(color.R, color.G, color.B);
                    color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterations * 2), maxColorDepth), Math.Min(Math.Max(0, color.G - iterations * 2), maxColorDepth), Math.Min(Math.Max(0, color.B - iterations * 2), maxColorDepth));
                    bitmap.SetPixel(j, i, color);
                }
            }
            bitmap.Save(OutputPath ?? "../../../out.png");
        }
    }
}
