using System;
using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        /// <summary>
        /// Adds coefficient to polynomial
        /// </summary>
        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial output = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                output.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return output;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">Point of evaluation</param>
        /// <returns>Exact value at given point</returns>
        public ComplexNumber Evaluate(double point)
        {
            return Evaluate(new ComplexNumber() { RealPart = point, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">Point of evaluation</param>
        /// <returns>Exact value at given point</returns>
        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber output = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber pointCopy = point;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        pointCopy = pointCopy.Multiply(point);
                    }

                    coefficient = coefficient.Multiply(pointCopy);
                }

                output = output.Add(coefficient);
            }

            return output;
        }

        /// <summary>
        /// Converts polynomial to it's string based representation
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string output = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                output += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        output += "x";
                    }
                }

                if (i + 1 < Coefficients.Count)
                {
                    output += " + ";
                }
            }
            return output;
        }
    }
}
