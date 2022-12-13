using System.Collections.Generic;

namespace Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> ListOfComplexNumbers { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial()
        {
            ListOfComplexNumbers = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber complexNumber) =>
            ListOfComplexNumbers.Add(complexNumber);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial newPolynomial = new Polynomial();
            for (int i = 1; i < ListOfComplexNumbers.Count; i++)
            {
                newPolynomial.ListOfComplexNumbers.Add(
                    ListOfComplexNumbers[i].Multiply(new ComplexNumber() { RealValue = i })
                );
            }

            return newPolynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="complexNumberToEvaluate">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double complexNumberToEvaluate)
        {
            return Evaluate(new ComplexNumber() { RealValue = complexNumberToEvaluate, ImaginaryValue = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.SetPropertiesZero;
            for (int i = 0; i < ListOfComplexNumbers.Count; i++)
            {
                ComplexNumber coefficient = ListOfComplexNumbers[i];
                ComplexNumber multiplication = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        multiplication = multiplication.Multiply(x);

                    coefficient = coefficient.Multiply(multiplication);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < ListOfComplexNumbers.Count; i++)
            {
                result += ListOfComplexNumbers[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < ListOfComplexNumbers.Count)
                    result += " + ";
            }
            return result;
        }
    }
}

