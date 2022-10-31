using System;

namespace Mathematics {

    public class ComplexNumber {

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber() { }

        public ComplexNumber(double real, double imaginary) {
            Real = real;
            Imaginary = imaginary;
        }

        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public ComplexNumber Add(ComplexNumber b) {
            return this + b;
        }

        public ComplexNumber Subtract(ComplexNumber b) {
            return this - b;
        }

        public ComplexNumber Multiply(ComplexNumber b) {
            return this * b;
        }

        public ComplexNumber Divide(ComplexNumber b) {
            return this / b;
        }

        public double GetAbs() {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians() {
            return Math.Atan(Imaginary / Real);
        }

        #region Operator overloading
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) {
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) {
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber(
                a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real
            );
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b) {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            ComplexNumber temp = a * new ComplexNumber(b.Real, -b.Imaginary);
            double divisor = b.Real * b.Real + b.Imaginary * b.Imaginary;
            return new ComplexNumber(temp.Real / divisor, temp.Imaginary / divisor);
        }
        #endregion

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }
            if (this.GetType().Equals(obj.GetType())) {
                ComplexNumber complexNumber = (ComplexNumber)obj;
                return Real == complexNumber.Real && Imaginary == complexNumber.Imaginary;
            }
            return base.Equals(obj);
        }

        public override string ToString() {
            return $"({Real} + {Imaginary}i)";
        }
    }
}

