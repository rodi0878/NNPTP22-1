using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;
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
            int[] intArgs = new int[2];
            for (int i = 0; i < intArgs.Length; i++)
            {
                intArgs[i] = int.Parse(args[i]);
            }
            double[] doubleArgs = new double[4];
            for (int i = 0; i < doubleArgs.Length; i++)
            {
                doubleArgs[i] = double.Parse(args[i + 2]);
            }
            string output = args[6];
            // TODO: add parameters from args?
            Bitmap bitmap = new Bitmap(intArgs[0], intArgs[1]);
            double xMin = doubleArgs[0];
            double xMax = doubleArgs[1];
            double yMin = doubleArgs[2];
            double yMax = doubleArgs[3];

            double xStep = (xMax - xMin) / intArgs[0];
            double yStep = (yMax - yMin) / intArgs[1];

            List<ComplexNumber> roots = new List<ComplexNumber>();
            // TODO: poly should be parameterised?
            Poly poly = new Poly();
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            //p.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            Poly tempPoly = poly;
            Poly derivedPoly = poly.Derive();

            Console.WriteLine(poly);
            Console.WriteLine(derivedPoly);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxId = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < intArgs[0]; i++)
            {
                for (int j = 0; j < intArgs[1]; j++)
                {
                    // find "world" coordinates of pixel
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        Real = x,
                        Imaginary = y
                    };

                    if (ox.Real == 0)
                        ox.Real = 0.0001;
                    if (ox.Imaginary == 0)
                        ox.Imaginary = 0.0001f;

                    //Console.WriteLine(ox);

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int k = 0; k < 30; k++)
                    {
                        var diff = poly.Eval(ox).Divide(derivedPoly.Eval(ox));
                        ox = ox.Subtract(diff);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                        {
                            k--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int l = 0; l < roots.Count; l++)
                    {
                        if (Math.Pow(ox.Real - roots[l].Real, 2) + Math.Pow(ox.Imaginary - roots[l].Imaginary, 2) <= 0.01)
                        {
                            known = true;
                            id = l;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count;
                        maxId = id + 1;
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var selectedColor = colors[id % colors.Length];
                    selectedColor = Color.FromArgb(selectedColor.R, selectedColor.G, selectedColor.B);
                    selectedColor = Color.FromArgb(Math.Min(Math.Max(0, selectedColor.R - (int)it * 2), 255), Math.Min(Math.Max(0, selectedColor.G - (int)it * 2), 255), Math.Min(Math.Max(0, selectedColor.B - (int)it * 2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bitmap.SetPixel(j, i, selectedColor);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bmp.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}

            bitmap.Save(output ?? "../../../out.png");
            //Console.ReadKey();
        }
    }

    namespace Mathematics
    {
        public class Poly
        {
            /// <summary>
            /// Coefficients
            /// </summary>
            public List<ComplexNumber> Coefficients { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Poly() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber Coefficients) => Coefficients.Add(Coefficients);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Poly Derive()
            {
                Poly polyDerivate = new Poly();
                for (int coefficientNumber = 1; coefficientNumber < Coefficients.Count; coefficientNumber++)
                {
                    polyDerivate.Coefficients.Add(Coefficients[coefficientNumber].Multiply(new ComplexNumber() { Real = coefficientNumber }));
                }

                return polyDerivate;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                return Eval(new ComplexNumber() { Real = x, Imaginary = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber startingComplexNumber = ComplexNumber.Zero;
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

                    startingComplexNumber = startingComplexNumber.Add(coefficient);
                }

                return startingComplexNumber;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string output = "";        
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    output += Coefficients[i];

                        for (int j = 0; j < i; j++)
                        {
                            output += "x";
                        }
                  
                    if (i + 1 < Coefficients.Count)
                        output += " + ";
                }
                return output;
            }
        }

        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber numberForComparision = obj as ComplexNumber;
                    return numberForComparision.Real == Real && numberForComparision.Imaginary == Imaginary;
                }
                return base.Equals(obj);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Multiply(ComplexNumber argumentComplexNumber)
            {
                ComplexNumber baseComplexNumber = this;
                return new ComplexNumber()
                {
                    Real = baseComplexNumber.Real * argumentComplexNumber.Real - baseComplexNumber.Imaginary * argumentComplexNumber.Imaginary,
                    Imaginary = baseComplexNumber.Real * argumentComplexNumber.Imaginary + baseComplexNumber.Imaginary * argumentComplexNumber.Real
                };
            }
            public double GetAbS()
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }

            public ComplexNumber Add(ComplexNumber argumentComplexNumber)
            {
                ComplexNumber baseComplexNumber = this;
                return new ComplexNumber()
                {
                    Real = baseComplexNumber.Real + argumentComplexNumber.Real,
                    Imaginary = baseComplexNumber.Imaginary + argumentComplexNumber.Imaginary
                };
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(Imaginary / Real);
            }
            public ComplexNumber Subtract(ComplexNumber argumentComplexNumber)
            {
                ComplexNumber baseComplexNumber = this;
                return new ComplexNumber()
                {
                    Real = baseComplexNumber.Real - argumentComplexNumber.Real,
                    Imaginary = baseComplexNumber.Imaginary - argumentComplexNumber.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber b)
            {
                // (aReal + aIm*i) / (bReal + bIm*i)
                // ((aReal + aIm*i) * (bReal - bIm*i)) / ((bReal + bIm*i) * (bReal - bIm*i))
                //  bReal*bReal - bIm*bIm*i*i
                var tmp = this.Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
                var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

                return new ComplexNumber()
                {
                    Real = tmp.Real / tmp2,
                    Imaginary = tmp.Imaginary / tmp2
                };
            }
        }
    }
}
