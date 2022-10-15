using System.Collections.Generic;

namespace NNPTPZ1.NewtonFractal.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coefficients">Coefficients</param>
        public Polynomial(List<ComplexNumber> coefficients) => Coefficients = coefficients;

        public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one.
        /// </summary>
        /// <returns>Derived polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();

            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber { RealPart = i }));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point.
        /// </summary>
        /// <param name="point">Point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double point) => Evaluate(new ComplexNumber { RealPart = point, ImaginaryPart = 0 });

        /// <summary>
        /// Evaluates polynomial at given point.
        /// </summary>
        /// <param name="point">Point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber multipliedPoint = point;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        multipliedPoint = multipliedPoint.Multiply(point);
                    }

                    coefficient = coefficient.Multiply(multipliedPoint);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }

                if (i + 1 < Coefficients.Count)
                {
                    result += " + ";
                }
            }

            return result;
        }
    }
}