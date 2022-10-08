using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynome
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynome() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynome Derive()
        {
            Polynome derivated = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivated.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return derivated;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            var y = Eval(new ComplexNumber() { Real = x, Imaginary = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber s = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coefficient = coefficient.Multiply(bx);
                }

                s = s.Add(coefficient);
            }

            return s;
        }

        protected bool Equals(Polynome other)
        {
            return Coefficients.SequenceEqual(other.Coefficients);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polynome)obj);
        }

        public override int GetHashCode()
        {
            return (Coefficients != null ? Coefficients.GetHashCode() : 0);
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                builder.Append(Coefficients[i]);
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        builder.Append("x");
                    }
                }

                if (i + 1 < Coefficients.Count)
                    builder.Append(" + ");
            }

            return builder.ToString();
        }
    }
}