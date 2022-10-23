using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polygon
    {
        public List<ComplexNumber> Coefficient { get; set; }

        public Polygon()
        {
            Coefficient = new List<ComplexNumber>();
        }

        public Polygon(List<ComplexNumber> Coefficients) { 
            Coefficient = Coefficients; 
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficient.Add(coefficient);
        }

        public Polygon Derive()
        {
            Polygon polygon = new Polygon();
            for (int i = 1; i < Coefficient.Count; i++)
            {
                polygon.Coefficient.Add(
                    Coefficient[i].Multiply(new ComplexNumber() { RealPart = i })
                );
            }

            return polygon;
        }

        public ComplexNumber Evaluate(double realPart)
        {
            ComplexNumber evaluator = new ComplexNumber() {
                RealPart = realPart, 
                ImaginaryPart = 0 
            
            };

            return Evaluate(evaluator);
        }

        public ComplexNumber Evaluate(ComplexNumber evaluated)
        {
            ComplexNumber evaluationResult = ComplexNumber.Zero;
            for (int i = 0; i < Coefficient.Count; i++)
            {
                ComplexNumber coefficient = Coefficient[i];
                ComplexNumber multiplied = evaluated;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                        multiplied = multiplied.Multiply(evaluated);

                    coefficient = coefficient.Multiply(multiplied);
                }

                evaluationResult = evaluationResult.Add(coefficient);
            }

            return evaluationResult;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < Coefficient.Count; i++)
            {
                output.Append(Coefficient[i]);

                for (int j = 0; j < i; j++)
                    output.Append("x");
                
                if (i + 1 < Coefficient.Count)
                    output.Append(" + ");
            }

            return output.ToString();
        }
    }
}
