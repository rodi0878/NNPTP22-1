using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    class NewtonFractalProducer
    {
        static void Main(string[] args)
        {
            int pointerForArguments = 0;
            int widthOfBitmap = 512, heightOfBitmap = 512;
            if (args.Length >= 2)
            {
                try
                {
                    widthOfBitmap = int.Parse(args[pointerForArguments++]);
                    heightOfBitmap = int.Parse(args[pointerForArguments++]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    pointerForArguments = 0;
                }
            }

            double xmin = 0, xmax = 0, ymin = 0, ymax = 0;

            if (args.Length >= 6)
            {
                try
                {
                    xmin = double.Parse(args[pointerForArguments++]);
                    xmax = double.Parse(args[pointerForArguments++]);
                    ymin = double.Parse(args[pointerForArguments++]);
                    ymax = double.Parse(args[pointerForArguments++]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string output = null;
            if (args.Length == 7)
            {
                output = args[pointerForArguments];
            }

            Bitmap bitmapToPaint = new Bitmap(widthOfBitmap, heightOfBitmap);

            double xstep = (xmax - xmin) / widthOfBitmap;
            double ystep = (ymax - ymin) / heightOfBitmap;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomials = new Polynomial();
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(ComplexNumber.Zero);
            polynomials.Coefficients.Add(new ComplexNumber() { RealNumber = 1 });
            Polynomial derivedPolynomials = polynomials.Derive();

            Console.WriteLine(polynomials);
            Console.WriteLine(derivedPolynomials);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
            for (int i = 0; i < widthOfBitmap; i++)
            {
                for (int j = 0; j < heightOfBitmap; j++)
                {
                    double x = xmin + j * xstep;
                    double y = ymin + i * ystep;

                    ComplexNumber Point = new ComplexNumber()
                    {
                        RealNumber = x,
                        ImaginaryUnit = (float)(y)
                    };

                    if (Point.RealNumber == 0)
                        Point.RealNumber = 0.0001;
                    if (Point.ImaginaryUnit == 0)
                        Point.ImaginaryUnit = 0.0001f;

                    float iteration = 0;
                    for (int k = 0; k < 30; k++)
                    {
                        var difference = polynomials.Evaluate(Point).Divide(derivedPolynomials.Evaluate(Point));
                        Point = Point.Subtract(difference);

                        if (Math.Pow(difference.RealNumber, 2) + Math.Pow(difference.ImaginaryUnit, 2) >= 0.5)
                        {
                            k--;
                        }
                        iteration++;
                    }

                    var known = false;
                    var id = 0;
                    for (int k = 0; k < roots.Count; k++)
                    {
                        if (Math.Pow(Point.RealNumber - roots[k].RealNumber, 2) + Math.Pow(Point.ImaginaryUnit - roots[k].ImaginaryUnit, 2) <= 0.01)
                        {
                            known = true;
                            id = k;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(Point);
                        id = roots.Count;
                    }

                    var pixelColor = colors[id % colors.Length];
                    pixelColor = Color.FromArgb(
                        Math.Min(Math.Max(0, pixelColor.R - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.G - (int)iteration * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.B - (int)iteration * 2), 255)
                    );
                    bitmapToPaint.SetPixel(j, i, pixelColor);
                }
            }
            bitmapToPaint.Save(output ?? "../../../out.png");
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

            public Polynomial Derive()
            {
                Polynomial polynomial = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealNumber = i }));
                }
                return polynomial;
            }

            public ComplexNumber Evaluate(double pointOfEvaluation)
            {
                return Evaluate(new ComplexNumber() { RealNumber = pointOfEvaluation, ImaginaryUnit = 0 });
            }
            
            public ComplexNumber Evaluate(ComplexNumber pointOfEvaluation)
            {
                ComplexNumber result = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber evalComplexNumber = pointOfEvaluation;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            evalComplexNumber = evalComplexNumber.Multiply(pointOfEvaluation);

                        coefficient = coefficient.Multiply(pointOfEvaluation);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }

            public override string ToString()
            {
                string stringBuilder = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    stringBuilder += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            stringBuilder += "x";
                        }
                    }
                    if (i + 1 < Coefficients.Count)
                    {
                        stringBuilder += " + ";
                    }
                }
                return stringBuilder;
            }
        }

        public class ComplexNumber
        {
            public double RealNumber { get; set; }
            public float ImaginaryUnit { get; set; }

            public override bool Equals(object objectToCompare)
            {
                if (objectToCompare is ComplexNumber)
                {
                    ComplexNumber complexNumberToCompare = objectToCompare as ComplexNumber;
                    return complexNumberToCompare.RealNumber == RealNumber && complexNumberToCompare.ImaginaryUnit == ImaginaryUnit;
                }
                return base.Equals(objectToCompare);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                RealNumber = 0,
                ImaginaryUnit = 0
            };

            public ComplexNumber Multiply(ComplexNumber multiplicand)
            {
                ComplexNumber multiplier = this;
                return new ComplexNumber()
                {
                    RealNumber = multiplier.RealNumber * multiplicand.RealNumber - multiplier.ImaginaryUnit * multiplicand.ImaginaryUnit,
                    ImaginaryUnit = (float)(multiplier.RealNumber * multiplicand.ImaginaryUnit + multiplier.ImaginaryUnit * multiplicand.RealNumber)
                };
            }

            public ComplexNumber Add(ComplexNumber termToAdd)
            {
                ComplexNumber term = this;
                return new ComplexNumber()
                {
                    RealNumber = term.RealNumber + termToAdd.RealNumber,
                    ImaginaryUnit = term.ImaginaryUnit + termToAdd.ImaginaryUnit
                };
            }


            public ComplexNumber Subtract(ComplexNumber subtrahend)
            {
                ComplexNumber minuend = this;
                return new ComplexNumber()
                {
                    RealNumber = minuend.RealNumber - subtrahend.RealNumber,
                    ImaginaryUnit = minuend.ImaginaryUnit - subtrahend.ImaginaryUnit
                };
            }

            public override string ToString()
            {
                return $"({RealNumber} + {ImaginaryUnit}i)";
            }

            internal ComplexNumber Divide(ComplexNumber complexNumberForDivision)
            {
                var dividend = this.Multiply(new ComplexNumber() { RealNumber = complexNumberForDivision.RealNumber, ImaginaryUnit = -complexNumberForDivision.ImaginaryUnit });
                var divisor = complexNumberForDivision.RealNumber * complexNumberForDivision.RealNumber + complexNumberForDivision.ImaginaryUnit * complexNumberForDivision.ImaginaryUnit;

                return new ComplexNumber()
                {
                    RealNumber = dividend.RealNumber / divisor,
                    ImaginaryUnit = (float)(dividend.ImaginaryUnit / divisor)
                };
            }
        }
    }
}
