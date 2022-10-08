using NNPTPZ1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void Add(ComplexNumber newCoefficient) { Coefficients.Add(newCoefficient); }

        /// <summary>
        /// Derives this polynomial and creates a new one
        /// </summary>
        /// <returns>Derived polynomial</returns>
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
        /// <param name="pointOfEvaluation"></param>
        /// <returns></returns>
        public ComplexNumber Evaluate(double pointOfEvaluation)
        {
            var y = Evaluate(new ComplexNumber() { RealPart = pointOfEvaluation, ImaginaryPart = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="pointOfEvaluationInComplexForm"></param>
        /// <returns></returns>
        public ComplexNumber Evaluate(ComplexNumber pointOfEvaluationInComplexForm)
        {
            ComplexNumber evaluatedPolynom = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber valueOfX = pointOfEvaluationInComplexForm;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                        valueOfX = valueOfX.Multiply(pointOfEvaluationInComplexForm);

                    coefficient = coefficient.Multiply(valueOfX);
                }

                evaluatedPolynom = evaluatedPolynom.Add(coefficient);
            }

            return evaluatedPolynom;
        }
        public override string ToString()
        {
            string stringRepresentation = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                stringRepresentation += Coefficients[i];
                    for (int j = 1; j < i; j++)
                    {
                        stringRepresentation += "x";
                    }
                if (i + 1 < Coefficients.Count)
                    stringRepresentation += " + ";
            }
            return stringRepresentation;
        }
    }
}
