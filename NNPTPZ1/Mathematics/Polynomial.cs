using System.Collections.Generic;

namespace Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// ComplexNumberList
        /// </summary>
        public List<ComplexNumber> ComplexNumberList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => ComplexNumberList = new List<ComplexNumber>();

        public Polynomial(List<ComplexNumber> complexNumberList)
        {
            ComplexNumberList = complexNumberList;
        }

        /// <summary>
        /// Adds complexNumber to the list
        /// </summary>
        public void Add(ComplexNumber complexNumber) => ComplexNumberList.Add(complexNumber);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < ComplexNumberList.Count; i++)
            {
                polynomial.ComplexNumberList.Add(ComplexNumberList[i].Multiply(new ComplexNumber() { RealPart = i }));
            }
            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>ComplexNumber</returns>
        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>ComplexNumber</returns>
        public ComplexNumber Eval(ComplexNumber complexNumberToEvaluate)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < ComplexNumberList.Count; i++)
            {
                ComplexNumber tempComplexNumber = ComplexNumberList[i];
                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        complexNumberToEvaluate = complexNumberToEvaluate.Multiply(complexNumberToEvaluate);
                    }
                    tempComplexNumber = tempComplexNumber.Multiply(complexNumberToEvaluate);
                }
                result = result.Add(tempComplexNumber);
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
            for (int i = 0; i < ComplexNumberList.Count; i++)
            {
                result += ComplexNumberList[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < ComplexNumberList.Count)
                {
                    result += " + ";
                }
            }
            return result;
        }
    }
}
