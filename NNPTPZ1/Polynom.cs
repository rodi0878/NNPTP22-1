using NNPTPZ1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Polynom
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynom() { Coefficients = new List<ComplexNumber>(); }

        public Polynom(List<ComplexNumber> coefficients)
        {
            Coefficients = coefficients;
        }

        public void Add(ComplexNumber newCoefficient){ Coefficients.Add(newCoefficient); }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynom Derive()
        {
            Polynom polynomAfterDerivation = new Polynom();
            for (int x = 1; x < Coefficients.Count; x++)
            {
                ComplexNumber originalComplexNumber = Coefficients[x];
                polynomAfterDerivation.Coefficients.Add(originalComplexNumber.Multiply(new ComplexNumber() { RealPart = x }));
            }

            return polynomAfterDerivation;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="pointOfEvaluation">point of evaluation</param>
        public ComplexNumber Evaluate(double pointOfEvaluation)
        {
            var y = Evaluate(new ComplexNumber() { RealPart = pointOfEvaluation, ImaginaryPart = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        public ComplexNumber Evaluate(ComplexNumber pointOfEvaluationInComplexForm)
        {
            ComplexNumber evaluatedPolynom = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber valueOfX = pointOfEvaluationInComplexForm;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        valueOfX = valueOfX.Multiply(pointOfEvaluationInComplexForm);

                    coefficient = coefficient.Multiply(valueOfX);
                }

                evaluatedPolynom = evaluatedPolynom.Sum(coefficient);
            }

            return evaluatedPolynom;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
