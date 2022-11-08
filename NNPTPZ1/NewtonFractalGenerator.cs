using NNPTPZ1.Mathematics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Generators
{
    public class NewtonFractalGenerator
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        private readonly List<ComplexNumber> koreny = new List<ComplexNumber>();
        private readonly Bitmap bitmap;

        private readonly Polygon polygon = new Polygon(
            new List<ComplexNumber>(
                new ComplexNumber[] {
                    new ComplexNumber{ RealPart = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber{ RealPart = 1 },
                }
            )
        );

        private readonly string OutputFile;
        private readonly Color[] COLOURS = new Color[] {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private NewtonFractalGenerator(int width, int height, double xMin, double xMax, double yMin, double yMax) : this(width, height, xMin, xMax, yMin, yMax, "../../../out.png")
        {

        }

        private NewtonFractalGenerator(int width, int height, double xMin, double xMax, double yMin, double yMax, string outputFile)
        {
            Width = width;
            Height = height;
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            OutputFile = outputFile;
            bitmap = new Bitmap(Width, Height);
        }

        private NewtonFractalGenerator(string[] arguments)
        {
            CheckArguments(arguments);
            Width = int.Parse(arguments[0]);
            Height = int.Parse(arguments[1]);
            XMin = double.Parse(arguments[2]);
            XMax = double.Parse(arguments[3]);
            YMin = double.Parse(arguments[4]);
            YMax = double.Parse(arguments[5]);
            OutputFile = arguments[6] ?? "../../../out.png";
            bitmap = new Bitmap(Width, Height);
        }

        public static void Generate(string[] arguments)
        {
            NewtonFractalGenerator generator = new NewtonFractalGenerator(arguments);
            Console.WriteLine(generator.polygon);
            Console.WriteLine(generator.polygon.Derive());

            generator.Cycle();
            generator.ToBitmap();
        }

        private void ToBitmap()
        {
            bitmap.Save(OutputFile);
        }

        private void Cycle()
        {
            double xStep = (XMax - XMin) / Width;
            double yStep = (YMax - YMin) / Height;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    double x = XMin + j * xStep;
                    double y = YMin + i * yStep;
                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = (x == 0 ? 0.001 : x),
                        ImaginaryPart = (y == 0 ? 0.001f : (float)y)
                    };

                    ox = Iterate(ox, out int iterations);

                    Colorize(i, j, FindRoots(ox), iterations);
                }
            }
        }

        private ComplexNumber Iterate(ComplexNumber ox, out int iteration)
        {
            iteration = 0;
            for (int k = 0; k < 30; k++)
            {
                ComplexNumber difference = polygon.Evaluate(ox).Divide(polygon.Derive().Evaluate(ox));
                ox = ox.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    k--;
                }
                iteration++;
            }

            return ox;
        }

        private int FindRoots(ComplexNumber complex)
        {
            bool known = false;
            int id = 0;
            for (int k = 0; k < koreny.Count; k++)
            {
                if (Math.Pow(complex.RealPart - koreny[k].RealPart, 2) + 
                    Math.Pow(complex.ImaginaryPart - koreny[k].ImaginaryPart, 2)
                    <= 0.01)
                {
                    known = true;
                    id = k;
                }
            }
            if (!known)
            {
                koreny.Add(complex);
                id = koreny.Count;
            }
            return id;
        }

        private void Colorize(int x, int y, int colourIndex, int iteration)
        {
            Color colour = COLOURS[colourIndex % COLOURS.Length];

            colour = Color.FromArgb(colour.R, colour.G, colour.B);
            colour = Color.FromArgb(
                Math.Min(Math.Max(0, colour.R - iteration * 2), 255), 
                Math.Min(Math.Max(0, colour.G - iteration * 2), 255), 
                Math.Min(Math.Max(0, colour.B - iteration * 2), 255)
            );

            bitmap.SetPixel(x, y, colour);
        }

        private void CheckArguments(string[] arguments)
        {
            if (arguments is null)
                throw new ArgumentNullException(nameof(arguments));

            if (arguments.Length < 6)
                throw new ArgumentException("There should be 6 or 7 arguments\n(width, height, XMin, XMax, YMin, YMax, [outputFile])");

            CheckIntParsable(arguments[0], "Argument on position 1 is not a valid positive integer");
            CheckIntParsable(arguments[1], "Argument on position 2 is not a valid positive integer");

            CheckDoubleParsable(arguments[2], "Argument on position 3 is not a valid number");
            CheckDoubleParsable(arguments[3], "Argument on position 4 is not a valid number");
            CheckDoubleParsable(arguments[4], "Argument on position 5 is not a valid number");
            CheckDoubleParsable(arguments[5], "Argument on position 6 is not a valid number");
        }

        private void CheckIntParsable(string input, string errorMessage)
        {
            if (!int.TryParse(input, out int _))
                throw new ArgumentException(errorMessage);
        }

        private void CheckDoubleParsable(string input, string errorMessage)
        {
            if (!double.TryParse(input, out double _))
                throw new ArgumentException(errorMessage);
        }
    }
}
