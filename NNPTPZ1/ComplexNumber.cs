using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1 {
    namespace Mathematics {
        public class ComplexNumber {

            public readonly static ComplexNumber Zero = new ComplexNumber(0, 0);

            public double Re { get; set; }
            public double Im { get; set; }

            public ComplexNumber(double Re, double Im) {
                this.Re = Re;
                this.Im = Im;
            }

            public override bool Equals(object obj) {
                if (obj is ComplexNumber) {
                    ComplexNumber complexNumber = obj as ComplexNumber;
                    return complexNumber.Re == Re && complexNumber.Im == Im;
                }
                return base.Equals(obj);
            }

            public double GetAbsoluteValue() {
                return Math.Sqrt(Re * Re + Im * Im);
            }

            public double GetAngleInRadian() {
                return Math.Atan(Im / Re);
            }

            public ComplexNumber Add(ComplexNumber addition) {
                return new ComplexNumber(this.Re + addition.Re, this.Im + addition.Im);
            }

            public ComplexNumber Subtract(ComplexNumber reducer) {
                return new ComplexNumber(this.Re - reducer.Re, this.Im - reducer.Im);
            }

            public ComplexNumber Multiply(ComplexNumber factor) {
                return new ComplexNumber(this.Re * factor.Re - this.Im * factor.Im, this.Re * factor.Im + this.Im * factor.Re);
            }

            public ComplexNumber Divide(ComplexNumber divisor) {
                var numerator = this.Multiply(new ComplexNumber(divisor.Re, -divisor.Im));
                var denumerator = divisor.Re * divisor.Re + divisor.Im * divisor.Im;
                return new ComplexNumber(numerator.Re / denumerator, numerator.Im / denumerator);
            }

            public override string ToString() {
                return $"({Re} + {Im}i)";
            }

        }
    }
}
