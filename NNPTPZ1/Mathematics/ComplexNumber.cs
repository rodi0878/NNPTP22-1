using System;

namespace NNPTPZ1.Mathematics {
    public class ComplexNumber {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public static readonly ComplexNumber Zero = new ComplexNumber() {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public double GetAbsoluteValue() {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double GetAngleInRadians() {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Add(ComplexNumber addend) {
            return new ComplexNumber() {
                RealPart = this.RealPart + addend.RealPart,
                ImaginaryPart = this.ImaginaryPart + addend.ImaginaryPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend) {
            return new ComplexNumber() {
                RealPart = this.RealPart - subtrahend.RealPart,
                ImaginaryPart = this.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand) {
            return new ComplexNumber() {
                RealPart = this.RealPart * multiplicand.RealPart - this.ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = this.RealPart * multiplicand.ImaginaryPart + this.ImaginaryPart * multiplicand.RealPart
            };
        }

        internal ComplexNumber Divide(ComplexNumber number) {
            var dividend = this.Multiply(new ComplexNumber() { RealPart = number.RealPart, ImaginaryPart = -number.ImaginaryPart });
            var divisor = Math.Pow(number.RealPart, 2) + Math.Pow(number.ImaginaryPart, 2);

            return new ComplexNumber() {
                RealPart = dividend.RealPart / divisor,
                ImaginaryPart = dividend.ImaginaryPart / divisor
            };
        }

        public override bool Equals(object obj) {
            if (obj is ComplexNumber) {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public override string ToString() {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
