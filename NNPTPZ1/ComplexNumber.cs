using System;

namespace NNPTPZ1.Mathematics {
    public class ComplexNumber {
        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

        public static readonly ComplexNumber Zero = new ComplexNumber() {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public override bool Equals(object obj) {
            if (obj is ComplexNumber) {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand) {
            return new ComplexNumber() {
                RealPart = this.RealPart * multiplicand.RealPart - this.ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = (float)(this.RealPart * multiplicand.ImaginaryPart + this.ImaginaryPart * multiplicand.RealPart)
            };
        }

        public double GetAbsoluteValue() {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber addend) {
            return new ComplexNumber() {
                RealPart = this.RealPart + addend.RealPart,
                ImaginaryPart = this.ImaginaryPart + addend.ImaginaryPart
            };
        }

        public double GetAngleInRadians() {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend) {
            return new ComplexNumber() {
                RealPart = this.RealPart - subtrahend.RealPart,
                ImaginaryPart = this.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public override string ToString() {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber number) {
            var dividend = this.Multiply(new ComplexNumber() { RealPart = number.RealPart, ImaginaryPart = -number.ImaginaryPart });
            var divisor = Math.Pow(number.RealPart, 2) + Math.Pow(number.ImaginaryPart, 2);

            return new ComplexNumber() {
                RealPart = dividend.RealPart / divisor,
                ImaginaryPart = (float)(dividend.ImaginaryPart / divisor)
            };
        }
    }
}
