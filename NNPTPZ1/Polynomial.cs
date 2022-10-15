using System.Collections.Generic;

namespace NNPTPZ1
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
        public Polynomial(List<ComplexNumber> coefficients)
        {
            Coefficients = coefficients;
        }

        public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomialDerivate = new Polynomial();
            for (int coefficientNumber = 1; coefficientNumber < Coefficients.Count; coefficientNumber++)
            {
                polynomialDerivate.Coefficients.Add(Coefficients[coefficientNumber].Multiply(new ComplexNumber() { RealPart = coefficientNumber }));
            }

            return polynomialDerivate;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="realX">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double realX)
        {
            return Eval(new ComplexNumber() { RealPart = realX, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="realXInComplexNumber">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber realXInComplexNumber)
        {
            ComplexNumber startingComplexNumbers = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber complexNumberWithRealX = realXInComplexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        complexNumberWithRealX = complexNumberWithRealX.Multiply(realXInComplexNumber);

                    coefficient = coefficient.Multiply(complexNumberWithRealX);
                }

                startingComplexNumbers = startingComplexNumbers.Add(coefficient);
            }

            return startingComplexNumbers;
        }

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
}
