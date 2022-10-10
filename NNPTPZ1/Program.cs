using NNPTPZ1.NewtonFractals;
using NNPTPZ1.NewtonFractals.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractal newtonFractal = new NewtonFractal(
                Config.GetConfigFromArguments(args),
                new Polynomial() {
                    Coefficients = new List<ComplexNumber>() {
                        new ComplexNumber() { RealPart = 1 },
                        ComplexNumber.Zero,
                        ComplexNumber.Zero,
                        new ComplexNumber() { RealPart = 1 }
                    }
                }
                );
            newtonFractal.EvaluateToBitmap();
            newtonFractal.Save();
        }
    }
    namespace NewtonFractals
    {
        public class NewtonFractal {
            private const int NewtonsIterationsAmount = 30;

            private Config config;
            private double xStep;
            private double yStep;
            private Polynomial polynomial;
            private Polynomial polynomialDerivated;
            private Bitmap outputPaint;
            private Color[] colors;
            public List<ComplexNumber> Roots { get; set; } = new List<ComplexNumber>();
            public NewtonFractal(Config config, Polynomial polynomial) {
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
            public void Save() {
                outputPaint.Save(string.IsNullOrEmpty(config.OutputFilename)?  "../../../out.png" : config.OutputFilename);
            }
            public void EvaluateToBitmap() {
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
            private Color FindColorByNewtonsIteration(ComplexNumber point) {
                float iteratorCounter = 0;
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
        class NewtonFractalsException : Exception
        {
            public NewtonFractalsException() { }

            public NewtonFractalsException(string message)
                : base(message)
            {

            }
        }
        public class Config {
            private int _bitmapWidth, _bitmapHeight;
            private double _xMin, _xMax, _yMin, _yMax;
            private string _outputFilename;
            public int BitmapWidth { get => _bitmapWidth; private set => _bitmapWidth = value; }
            public int BitmapHeight { get => _bitmapHeight; private set => _bitmapHeight = value; }
            public double XMin { get => _xMin; private set => _xMin = value; }
            public double XMax { get => _xMax; private set => _xMax = value; }
            public double YMin { get => _yMin; private set => _yMin = value; }
            public double YMax { get => _yMax; private set => _yMax = value; }
            public string OutputFilename { get => _outputFilename; private set => _outputFilename = value; }
            public static Config GetConfigFromArguments(string[] arguments) {
                Config config = new Config();
                if (
                    arguments.Length > 5
                    && int.TryParse(arguments[0], out config._bitmapWidth)
                    && int.TryParse(arguments[1], out config._bitmapHeight)
                    && double.TryParse(arguments[2], out config._xMin)
                    && double.TryParse(arguments[3], out config._xMax)
                    && double.TryParse(arguments[4], out config._yMin)
                    && double.TryParse(arguments[5], out config._yMax)
                    )
                {
                    config.OutputFilename = arguments[6] ?? String.Empty;
                    return config;
                }
                throw new NewtonFractalsException("Arguments contract violated");
            }
        }
        namespace Mathematics
        {
            public class Polynomial
            {
                public List<ComplexNumber> Coefficients { get; set; }

                public Polynomial() => Coefficients = new List<ComplexNumber>();

                public void Add(ComplexNumber coefficient) =>
                    Coefficients.Add(coefficient);

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
                /// <param name="point">point of evaluation</param>
                /// <returns>y</returns>
                public ComplexNumber Evaluate(double point)
                {
                    return Evaluate(new ComplexNumber() { RealPart = point, ImaginaryPart = 0 });
                }

                /// <summary>
                /// Evaluates polynomial at given point
                /// </summary>
                /// <param name="point">point of evaluation</param>
                /// <returns>ComplexNumber</returns>
                public ComplexNumber Evaluate(ComplexNumber point)
                {
                    ComplexNumber returnResult = ComplexNumber.Zero;
                    for (int i = 0; i < Coefficients.Count; i++)
                    {
                        ComplexNumber coefficient = Coefficients[i];
                        ComplexNumber pointMultiplier = point;
                        int power = i;

                        if (i > 0)
                        {
                            for (int j = 0; j < power - 1; j++)
                                pointMultiplier = pointMultiplier.Multiply(point);

                            coefficient = coefficient.Multiply(pointMultiplier);
                        }

                        returnResult = returnResult.Add(coefficient);
                    }

                    return returnResult;
                }

                /// <summary>
                /// ToString
                /// </summary>
                /// <returns>String representation of polynomial</returns>
                public override string ToString()
                {
                    string returnResult = "";
                    int i = 0;
                    for (; i < Coefficients.Count; i++)
                    {
                        returnResult += Coefficients[i];
                        if (i > 0)
                        {
                            int j = 0;
                            for (; j < i; j++)
                            {
                                returnResult += "x";
                            }
                        }
                        if (i + 1 < Coefficients.Count)
                            returnResult += " + ";
                    }
                    return returnResult;
                }
            }

            public class ComplexNumber
            {
                public double RealPart { get; set; }
                public float ImaginaryPart { get; set; }
                public override bool Equals(object obj)
                {
                    if (obj is ComplexNumber comparativeComplexNumber)
                    {
                        return comparativeComplexNumber.RealPart == RealPart && comparativeComplexNumber.ImaginaryPart == ImaginaryPart;
                    }
                    return base.Equals(obj);
                }

                public readonly static ComplexNumber Zero = new ComplexNumber()
                {
                    RealPart = 0,
                    ImaginaryPart = 0
                };

                public ComplexNumber Multiply(ComplexNumber multiplier)
                {
                    return new ComplexNumber()
                    {
                        RealPart = RealPart * multiplier.RealPart - ImaginaryPart * multiplier.ImaginaryPart,
                        ImaginaryPart = (float)(RealPart * multiplier.ImaginaryPart + ImaginaryPart * multiplier.RealPart)
                    };
                }
                public double GetAbs()
                {
                    return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
                }

                public ComplexNumber Add(ComplexNumber addend)
                {
                    return new ComplexNumber()
                    {
                        RealPart = RealPart + addend.RealPart,
                        ImaginaryPart = ImaginaryPart + addend.ImaginaryPart
                    };
                }
                public double GetAngleInRadians()
                {
                    return Math.Atan(ImaginaryPart / RealPart);
                }
                public ComplexNumber Subtract(ComplexNumber subtrahned)
                {
                    return new ComplexNumber()
                    {
                        RealPart = RealPart - subtrahned.RealPart,
                        ImaginaryPart = ImaginaryPart - subtrahned.ImaginaryPart
                    };
                }

                public override string ToString()
                {
                    return $"({RealPart} + {ImaginaryPart}i)";
                }

                internal ComplexNumber Divide(ComplexNumber divisor)
                {
                    ComplexNumber complexNumberDivident = this.Multiply(new ComplexNumber() { 
                        RealPart = divisor.RealPart,
                        ImaginaryPart = -divisor.ImaginaryPart });

                    double complexNumberDivisor = Math.Pow(divisor.RealPart, 2) + Math.Pow(divisor.ImaginaryPart, 2);

                    return new ComplexNumber()
                    {
                        RealPart = complexNumberDivident.RealPart / complexNumberDivisor,
                        ImaginaryPart = (float)(complexNumberDivident.ImaginaryPart / complexNumberDivisor)
                    };
                }
            }

        }
    }
}
