using System.Collections.Generic;

namespace NNPTPZ1.NewtonFractals.Mathematics
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
}