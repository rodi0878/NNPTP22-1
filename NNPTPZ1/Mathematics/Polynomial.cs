
using System.Collections.Generic;

namespace Mathematics
{
    public class Polynomial
    {

        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealNumber = i }));
            }
            return polynomial;
        }

        public ComplexNumber Evaluate(double pointOfEvaluation)
        {
            return Evaluate(new ComplexNumber() { RealNumber = pointOfEvaluation, ImaginaryUnit = 0 });
        }

        public ComplexNumber Evaluate(ComplexNumber pointOfEvaluation)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber evalComplexNumber = pointOfEvaluation;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        evalComplexNumber = evalComplexNumber.Multiply(pointOfEvaluation);

                    coefficient = coefficient.Multiply(pointOfEvaluation);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        public override string ToString()
        {
            string stringBuilder = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                stringBuilder += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        stringBuilder += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                {
                    stringBuilder += " + ";
                }
            }
            return stringBuilder;
        }
    }
}
