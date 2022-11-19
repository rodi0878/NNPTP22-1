using System.Collections.Generic;

namespace Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() {
            Coefficients = new List<ComplexNumber>();
        }
        public Polynomial(List<ComplexNumber> coefficients)
        {
            Coefficients = coefficients;
        }
        public void Add(ComplexNumber complexNumber) { 
            Coefficients.Add(complexNumber);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                ComplexNumber derivative = Coefficients[i].Multiply(new ComplexNumber() { RealPart = i });
                polynomial.Coefficients.Add(derivative);
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>ComplexNumber</returns>
        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>ComplexNumber</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];

                if (i > 0)
                {
                    ComplexNumber intermidiator = x;
                    for (int j = 0; j < i - 1; j++) 
                    {
                        intermidiator = intermidiator.Multiply(x);
                    }

                    coefficient = coefficient.Multiply(intermidiator);
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
            int i = 0;
            for (; i < Coefficients.Count; i++)
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
                    result += " + ";
            }
            return result;
        }
    }

}
