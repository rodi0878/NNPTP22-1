using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1 {
    namespace Mathematics {
        public class Polynomial {
            public List<ComplexNumber> Coeficients { get; set; }

            public Polynomial() {
                Coeficients = new List<ComplexNumber>();
            }

            public void Add(ComplexNumber coeficient) {
                Coeficients.Add(coeficient);
            }

            public Polynomial Derive() {
                Polynomial polynom = new Polynomial();
                for (int i = 1; i < Coeficients.Count; i++) {
                    polynom.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber(i, 0)));
                }
                return polynom;
            }

            public ComplexNumber Evaluate(double valueX) {
                return Evaluate(new ComplexNumber(valueX, 0));
            }


            public ComplexNumber Evaluate(ComplexNumber valueX) {
                ComplexNumber valueOfPolynom = ComplexNumber.Zero;
                for (int i = 0; i < Coeficients.Count; i++) {
                    ComplexNumber valueOfCoeficient = Coeficients[i];
                    ComplexNumber multiplication = valueX;

                    if (i > 0) {
                        for (int j = 0; j < i - 1; j++)
                            multiplication = multiplication.Multiply(valueX);

                        valueOfCoeficient = valueOfCoeficient.Multiply(multiplication);
                    }
                    valueOfPolynom = valueOfPolynom.Add(valueOfCoeficient);
                }

                return valueOfPolynom;
            }


            public override string ToString() {
                string outputString = "";

                for (int i = 0; i < Coeficients.Count; i++) {
                    outputString += Coeficients[i];
                    if (i > 0) {
                        int j = 0;
                        for (; j < i; j++) {
                            outputString += "x";
                        }
                    }
                    if (i + 1 < Coeficients.Count)
                        outputString += " + ";
                }
                return outputString;
            }
        }
    }
}
