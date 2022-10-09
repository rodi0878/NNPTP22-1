using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            Polynomial polynomial = new Polynomial(new List<ComplexNumber> {
                new ComplexNumber { RealPart = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber { RealPart = 1 }
            });

            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            ParsedArgumentData argumentData = new ParsedArgumentData(args);

            Bitmap bitmap = new Bitmap(argumentData.BitmapWidth, argumentData.BitmapHeight);

            double xStep = (argumentData.XMax - argumentData.XMin) / argumentData.BitmapWidth;
            double yStep = (argumentData.YMax - argumentData.YMin) / argumentData.BitmapHeight;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            for (int i = 0; i < argumentData.BitmapWidth; i++)
            {
                for (int j = 0; j < argumentData.BitmapHeight; j++)
                {
                    // find "world" coordinates of pixel
                    double y = argumentData.YMin + i * yStep;
                    double x = argumentData.XMin + j * xStep;
                    ComplexNumber coordinates = new ComplexNumber { RealPart = x, ImaginaryPart = (float)y };

                    if (coordinates.RealPart == 0) coordinates.RealPart = 0.0001;
                    if (coordinates.ImaginaryPart == 0) coordinates.ImaginaryPart = 0.0001f;

                    // find solution of equation using newton's iteration
                    int iteration = 0;

                    for (int k = 0; k < 30; k++)
                    {
                        ComplexNumber diff = polynomial.Evaluate(coordinates).Divide(derivedPolynomial.Evaluate(coordinates));

                        coordinates = coordinates.Subtract(diff);

                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            k--;
                        }
                        iteration++;
                    }

                    // find solution root number
                    bool known = false;
                    int rootsCount = 0;

                    for (int k = 0; k < roots.Count; k++)
                    {
                        if (Math.Pow(coordinates.RealPart - roots[k].RealPart, 2) + Math.Pow(coordinates.ImaginaryPart - roots[k].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            rootsCount = k;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(coordinates);
                        rootsCount = roots.Count;
                    }

                    // colorize pixel according to root number
                    Color color = colors[rootsCount % colors.Length];

                    color = Color.FromArgb(
                        Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                        Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                        Math.Min(Math.Max(0, color.B - iteration * 2), 255)
                    );

                    bitmap.SetPixel(j, i, color);
                }
            }
            bitmap.Save(argumentData.FilePath ?? "../../../out.png");
        }
    }

    internal class ParsedArgumentData
    {
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string FilePath { get; set; }

        public ParsedArgumentData(string[] args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));
            try
            {
                BitmapWidth = int.Parse(args[0]);
                BitmapHeight = int.Parse(args[1]);
                XMin = double.Parse(args[2]);
                XMax = double.Parse(args[3]);
                YMin = double.Parse(args[4]);
                YMax = double.Parse(args[5]);
                FilePath = args[6];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}