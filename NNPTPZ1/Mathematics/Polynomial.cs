using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) =>
            Coefficients.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            var y = Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber complex = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coefficient = coefficient.Multiply(bx);
                }

                complex = complex.Add(coefficient);
            }

            return complex;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string output = string.Empty;

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
                    output += " + ";
            }
            return output;
        }
    }
}
