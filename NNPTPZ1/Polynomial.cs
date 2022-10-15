using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Polynomial polyDerivate = new Polynomial();
            for (int coefficientNumber = 1; coefficientNumber < Coefficients.Count; coefficientNumber++)
            {
                polyDerivate.Coefficients.Add(Coefficients[coefficientNumber].Multiply(new ComplexNumber() { RealPart = coefficientNumber }));
            }

            return polyDerivate;
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
            ComplexNumber startingComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        realXInComplexNumber = realXInComplexNumber.Multiply(realXInComplexNumber);

                    coefficient = coefficient.Multiply(realXInComplexNumber);
                }

                startingComplexNumber = startingComplexNumber.Add(coefficient);
            }

            return startingComplexNumber;
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
