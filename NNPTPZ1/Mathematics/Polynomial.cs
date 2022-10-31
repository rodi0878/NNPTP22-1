using System;
using System.Collections.Generic;
using System.Text;

namespace Mathematics {

    public class Polynomial {

        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynomial(params ComplexNumber [] collection) : this() {
            Coefficients.AddRange(collection);
        }

        public void Add(ComplexNumber coefficient) {
            Coefficients.Add(coefficient);
        }

        public void AddRange(params ComplexNumber [] coefficients) { 
            Coefficients.AddRange(coefficients);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive() {
            Polynomial p = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++) {
                p.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }
            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x) {
            return Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x) {
            ComplexNumber result = ComplexNumber.Zero;

            // i -> power
            for (int i = 0; i < Coefficients.Count; i++) {
                if (Coefficients[i].Real > 0 || Coefficients[i].Imaginary > 0) {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber bx = x;

                    if (i > 0) {
                        for (int j = 0; j < i - 1; j++) {
                            bx = bx.Multiply(x);
                        }
                        coefficient = coefficient.Multiply(bx);
                    }
                    result = result.Add(coefficient);
                }
            }
            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++) {
                sb.Append(Coefficients[i]);
                if (i > 0) {
                    for (int j = 0; j < i; j++) {
                        sb.Append("x");
                    }
                }
                if (i + 1 < Coefficients.Count)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}

