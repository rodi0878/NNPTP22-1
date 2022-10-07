using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginary == Imaginary;
            }

            return base.Equals(obj);
        }

        public static readonly ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber right)
        {
            ComplexNumber left = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber
            {
                Real = left.Real * right.Real - left.Imaginary * right.Imaginary,
                Imaginary = left.Real * right.Imaginary + left.Imaginary * right.Real
            };
        }

        public double GetAbS()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public ComplexNumber Add(ComplexNumber right)
        {
            ComplexNumber left = this;
            return new ComplexNumber()
            {
                Real = left.Real + right.Real,
                Imaginary = left.Imaginary + right.Imaginary
            };
        }

        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginary / Real);
        }

        public ComplexNumber Subtract(ComplexNumber right)
        {
            ComplexNumber left = this;
            return new ComplexNumber
            {
                Real = left.Real - right.Real,
                Imaginary = left.Imaginary - right.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        public ComplexNumber Divide(ComplexNumber right)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var dividend = Multiply(new ComplexNumber { Real = right.Real, Imaginary = -right.Imaginary });
            var divisor = right.Real * right.Real + right.Imaginary * right.Imaginary;

            return new ComplexNumber
            {
                Real = dividend.Real / divisor,
                Imaginary = dividend.Imaginary / divisor
            };
        }
    }
}