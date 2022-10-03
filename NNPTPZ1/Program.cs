using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.NewtonFractal;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        public const int MaxNewtonIteration = 30;
        public const byte ColorShadeMultiplier = 2;
        public const byte ImageTransparency = 255;

        static void Main(string[] args)
        {
            try
            {
                ArgumentReader inputArguments = new ArgumentReader(args);
                Bitmap outputImage = new Bitmap(inputArguments.ImageWidth, inputArguments.ImageHeight);

                NewtonFractalBase newtonFractal = new NewtonFractalBase(
                    MaxNewtonIteration,
                    ColorShadeMultiplier,
                    ImageTransparency,
                    new Color[]
                        {
                            Color.Red, 
                            Color.Blue,
                            Color.Green, 
                            Color.Yellow, 
                            Color.Orange, 
                            Color.Fuchsia, 
                            Color.Gold, 
                            Color.Cyan, 
                            Color.Magenta
                        });

                double xStep = (inputArguments.XMax - inputArguments.XMin) / inputArguments.ImageWidth;
                double yStep = (inputArguments.YMax - inputArguments.YMin) / inputArguments.ImageHeight;

                Polynomial polynomial = new Polynomial()
                {
                    Coefficients = new List<ComplexNumber> {
                        new ComplexNumber() { RealPart = -1 },
                        ComplexNumber.Zero,
                        ComplexNumber.Zero,
                        new ComplexNumber() { RealPart = 1 }
                    }
                };

                Console.WriteLine(polynomial);
                Console.WriteLine(polynomial.Derive());

                int progress = 0;
                for (int i = 0; i < inputArguments.ImageWidth; i++)
                {
                    for (int j = 0; j < inputArguments.ImageHeight; j++)
                    {
                        outputImage.SetPixel(j, i, newtonFractal.ComputePixel(
                            inputArguments.XMin + j * xStep, 
                            inputArguments.YMin + i * yStep, 
                            polynomial));
                    }

                    int currentProgress = (int)(i/(inputArguments.ImageWidth/100.0));
                    if (progress != currentProgress)
                    {
                        Console.WriteLine(currentProgress+"%");
                        progress = currentProgress;
                    }
                }

                outputImage.Save(inputArguments.OutputImagePath);

                ExitProgram("Task done succesfully");
                return;
            }
            catch(Exception ex)
            {
                ExitProgram(ex.Message);
                return;
            }
        }

        public static void ExitProgram(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to close console.");
            Console.ReadKey();
        }

        public class ArgumentReader
        {
            public const int NumberOfMandatoryArguments = 6;
            public const int NumberOfOptionalArguments = 1;
            public int ImageWidth { get; set; }
            public int ImageHeight { get; set; }
            public double XMin { get; set; }
            public double XMax { get; set; }
            public double YMin { get; set; }
            public double YMax { get; set; }
            public string OutputImagePath { get; set; } = "../../../out.png";

            public ArgumentReader(string[] args)
            {
                if (args == null || args.Length < NumberOfMandatoryArguments)
                {
                    throw new ArgumentException("There is missing mandatory arguments.");
                }

                ImageWidth = int.Parse(args[0]);
                ImageHeight = int.Parse(args[1]);

                XMin = double.Parse(args[2]);
                XMax = double.Parse(args[3]);
                YMin = double.Parse(args[4]);
                YMax = double.Parse(args[5]);

                if(args.Length >= NumberOfMandatoryArguments + NumberOfOptionalArguments)
                {
                    OutputImagePath = args[6];
                }
            }
        }
    }

    namespace NewtonFractal
    {
        public class NewtonFractalBase
        {
            /// <summary>
            /// Maximum of Newton's iterations
            /// </summary>
            public int MaxNewtonIterations { get; }
            /// <summary>
            /// Defines step between color shades
            /// </summary>
            public byte ColorShadeMultiplier { get; }
            /// <summary>
            /// Maximum of Newton's iterations
            /// </summary>
            public byte ImageTransparency { get; }
            /// <summary>
            /// List of known roots
            /// </summary>
            private List<ComplexNumber> Roots { get; }
            /// <summary>
            /// Color palette for output image
            /// </summary>
            public Color[] Colors { get; set; }

            public NewtonFractalBase(int maxNewtonIterations, byte colorShadeMultiplier, byte imageTransparency, Color[] colors)
            {
                MaxNewtonIterations = maxNewtonIterations;
                ColorShadeMultiplier = colorShadeMultiplier;
                ImageTransparency = imageTransparency;
                Roots = new List<ComplexNumber>();
                Colors=colors;
            }

            /// <summary>
            /// Computes pixel color based on given polynomial
            /// </summary>
            /// <returns>Color of pixel</returns>
            public Color ComputePixel(double x, double y, Polynomial polynomial)
            {
                ComplexNumber point = new ComplexNumber()
                {
                    RealPart = x,
                    ImaginaryPart = y
                };

                point = NewtonsIteration(polynomial, point, out int iterationCount, MaxNewtonIterations);
                return GetPixelColor(FindSolutionRootNumber(point), iterationCount);
            }

            private ComplexNumber NewtonsIteration(Polynomial polynomial, ComplexNumber startPoint , out int numberOfIterations, int maximumIterations)
            {
                numberOfIterations = 0;
                ComplexNumber point = startPoint;
                for (int k = 0; k < maximumIterations; k++)
                {
                    ComplexNumber diff = polynomial.Eval(point).Divide(polynomial.Derive().Eval(point));
                    point = point.Subtract(diff);

                    if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                    {
                        k--;
                    }
                    numberOfIterations++;
                }
                return point;
            }

            private int FindSolutionRootNumber(ComplexNumber point)
            {
                bool known = false;
                int index = 0;
                for (int i = 0; i < Roots.Count; i++)
                {
                    if (Math.Pow(point.RealPart- Roots[i].RealPart, 2) + Math.Pow(point.ImaginaryPart - Roots[i].ImaginaryPart, 2) <= 0.01)
                    {
                        known = true;
                        index = i;
                        break;
                    }
                }
                if (!known)
                {
                    Roots.Add(point);
                    index = Roots.Count;
                }
                return index;
            }

            private Color GetPixelColor(int rootToColorIndex, int colorShade)
            {
                Color baseColor = Colors[rootToColorIndex % Colors.Length];
                return Color.FromArgb(
                    Math.Min(Math.Max(0, baseColor.R - colorShade*ColorShadeMultiplier), ImageTransparency), 
                    Math.Min(Math.Max(0, baseColor.G - colorShade*ColorShadeMultiplier), ImageTransparency), 
                    Math.Min(Math.Max(0, baseColor.B - colorShade*ColorShadeMultiplier), ImageTransparency));
            }
        }

        public class Polynomial
        {
            /// <summary>
            /// Coefficients
            /// </summary>
            public List<ComplexNumber> Coefficients { get; set; }

            /// <summary>
            /// Constructors
            /// </summary>
            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public Polynomial(List<ComplexNumber> coefficients) => Coefficients = coefficients;

            public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial polynomial = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
                }

                return polynomial;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber s = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            x = x.Multiply(x);

                        coefficient = coefficient.Multiply(x);
                    }

                    s = s.Add(coefficient);
                }

                return s;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result += Coefficients[i];
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                    if (i+1<Coefficients.Count)
                        result += " + ";
                }
                return result;
            }
        }

        public class ComplexNumber
        {
            public double RealPart { get; set; }
            public double ImaginaryPart { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber complexNumberToCompare = obj as ComplexNumber;
                    return complexNumberToCompare.RealPart == RealPart && complexNumberToCompare.ImaginaryPart == ImaginaryPart;
                }
                return base.Equals(obj);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            public ComplexNumber Multiply(ComplexNumber b)
            {
                ComplexNumber a = this;
                return new ComplexNumber()
                {
                    RealPart = a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                    ImaginaryPart = (a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart)
                };
            }
            public double GetAbS()
            {
                return Math.Sqrt( RealPart * RealPart + ImaginaryPart * ImaginaryPart);
            }

            public ComplexNumber Add(ComplexNumber b)
            {
                ComplexNumber a = this;
                return new ComplexNumber()
                {
                    RealPart = a.RealPart + b.RealPart,
                    ImaginaryPart = a.ImaginaryPart + b.ImaginaryPart
                };
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(ImaginaryPart / RealPart);
            }
            public ComplexNumber Subtract(ComplexNumber b)
            {
                ComplexNumber a = this;
                return new ComplexNumber()
                {
                    RealPart = a.RealPart - b.RealPart,
                    ImaginaryPart = a.ImaginaryPart - b.ImaginaryPart
                };
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }

            internal ComplexNumber Divide(ComplexNumber divisor)
            {
                double formulaDivisor = Math.Pow(divisor.RealPart, 2) + Math.Pow(divisor.ImaginaryPart, 2);

                return new ComplexNumber()
                {
                    RealPart =
                        (this.RealPart * divisor.RealPart + this.ImaginaryPart * divisor.ImaginaryPart) / formulaDivisor,
                    ImaginaryPart = 
                        (this.ImaginaryPart * divisor.RealPart - this.RealPart * divisor.ImaginaryPart) / formulaDivisor
                };
            }
        }
    }
}
