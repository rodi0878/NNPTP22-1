using System.Collections.Generic;

namespace NNPTPZ1
{
	public class Polynomial
	{

		public List<ComplexNumber> Coefficients { get; set; }

		public Polynomial() => Coefficients = new List<ComplexNumber>();

		public void Add(ComplexNumber coefficient)
		{
			Coefficients.Add(coefficient);
		}

		public Polynomial Derive()
		{
			Polynomial polynomial = new Polynomial();
			for (int i = 1; i < Coefficients.Count; i++) {
				polynomial.Add(Coefficients[i].Multiply(new ComplexNumber(i, 0)));
            }

			return polynomial;
		}

		public ComplexNumber Evaluate(double point)
		{
			return Evaluate(new ComplexNumber(point, 0));
        }

		public ComplexNumber Evaluate(ComplexNumber point)
		{
			ComplexNumber result = ComplexNumber.Zero;

			for (int i = 0; i < Coefficients.Count; i++) {
				ComplexNumber coefficient = Coefficients[i];
				ComplexNumber multiplication = point;

				if (i > 0) {
					for (int j = 0; j < i - 1; j++)
						multiplication = multiplication.Multiply(point);

                    coefficient = coefficient.Multiply(multiplication);
				}

				result = result.Add(coefficient);
			}

			return result;
		}

		public override string ToString()
		{
			string result = "";
			int i = 0;
			for (; i < Coefficients.Count; i++) {
				result += Coefficients[i];
				if (i > 0) {
					int j = 0;
					for (; j < i; j++) {
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