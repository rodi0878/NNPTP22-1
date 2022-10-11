using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polygon
    {
        public List<ComplexNumber> Coefficient { get; set; }

        public Polygon() => Coefficient = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) =>
            Coefficient.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polygon Derive()
        {
            Polygon p = new Polygon();
            for (int q = 1; q < Coefficient.Count; q++)
            {
                p.Coefficient.Add(Coefficient[q].Multiply(new ComplexNumber() { RealPart = q }));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            var y = Evaluate(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber s = ComplexNumber.Zero;
            for (int i = 0; i < Coefficient.Count; i++)
            {
                ComplexNumber coef = Coefficient[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }

            return s;
        }

        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coefficient.Count; i++)
            {
                s += Coefficient[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i+1<Coefficient.Count)
                s += " + ";
            }
            return s;
        }
    }
}
