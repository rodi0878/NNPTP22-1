using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private const int MAX_NEWTONS_ITERATIONS = 30;
        private const double ROOT_TOLERANCE = 0.01;
        private const double QUOTIENT_TOLERANCE = 0.5;
        private const double PIXEL_INITIAL_VALUE = 0.0001;
        private const string DEFAULT_IMAGE_PATH = "../../../out.png";

        private int ImageHeight { get; set; }
        private int ImageWidth { get; set; }
        private double XMin { get; set; }
        private double XMax { get; set; }
        private double YMin { get; set; }
        private double YMax { get; set; }
        private string ImagePath { get; set; }
        private List<ComplexNumber> Roots { get; set; }
        private Color[] Colors { get; set; }

        public Polynomial Polynomial { get; set; }

        public NewtonFractal(string[] args)
        {
            ParseArguments(args);
            CreateInitialPolynomial();
            Roots = new List<ComplexNumber>();
            Colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }

        private void ParseArguments(string[] args)
        {
            ImageWidth = int.Parse(args[0]);
            ImageHeight = int.Parse(args[1]);

            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);

            ImagePath = args[6];
        }

        private void CreateInitialPolynomial()
        {

            Polynomial = new Polynomial(
                new List<ComplexNumber> {
                    new ComplexNumber{ RealPart = 1},
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber{ RealPart = 1},
                }
             );
        }

        private int FindRoot(ComplexNumber pixel)
        {
            bool known = false;
            int id = 0;
            for (int i = 0; i < Roots.Count; i++)
            {
                if (Math.Pow(pixel.RealPart - Roots[i].RealPart, 2) + Math.Pow(pixel.ImaginaryPart - Roots[i].ImaginaryPart, 2) <= ROOT_TOLERANCE)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                Roots.Add(pixel);
                id = Roots.Count;
            }
            return id;

        }

        private int FindNewtonIteration(ComplexNumber pixel)
        {
            int iteration = 0;
            for (int i = 0; i < MAX_NEWTONS_ITERATIONS; i++)
            {
                Polynomial DerivedPolynomial = Polynomial.Derive();
                ComplexNumber quotient = Polynomial.Evaluate(pixel).Divide(DerivedPolynomial.Evaluate(pixel));
                pixel = pixel.Subtract(quotient);

                if (Math.Pow(quotient.RealPart, 2) + Math.Pow(quotient.ImaginaryPart, 2) >= QUOTIENT_TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        private ComplexNumber GetPixel(double x, double y)
        {
            ComplexNumber pixel = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (pixel.RealPart == 0)
                pixel.RealPart = PIXEL_INITIAL_VALUE;
            if (pixel.ImaginaryPart == 0)
                pixel.ImaginaryPart = PIXEL_INITIAL_VALUE;

            return pixel;

        }
        private Color GetColor(ComplexNumber pixel)
        {
            int iteration = FindNewtonIteration(pixel);
            int rootIndex = FindRoot(pixel);

            Color color = Colors[rootIndex % Colors.Length];
            color = Color.FromArgb(
                    Math.Min(Math.Max(0, color.R - iteration * 2), 255),
                    Math.Min(Math.Max(0, color.G - iteration * 2), 255),
                    Math.Min(Math.Max(0, color.B - iteration * 2), 255)
                    );
            return color;
        }

        public void Generate()
        {

            Bitmap fractal = new Bitmap(ImageWidth, ImageHeight);

            double xStep = (XMax - XMin) / ImageWidth;
            double yStep = (YMax - YMin) / ImageHeight;

            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    // find "world" coordinates of pixel
                    double x = XMin + j * xStep;
                    double y = YMin + i * yStep;
                    ComplexNumber pixel = GetPixel(x, y);

                    fractal.SetPixel(j, i, GetColor(pixel));

                }
            }
            fractal.Save(ImagePath ?? DEFAULT_IMAGE_PATH);
        }

    }
}
