using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber()
        {
        }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                Real = this.Real + b.Real,
                Imaginary = this.Imaginary + b.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                Real = this.Real - b.Real,
                Imaginary = this.Imaginary - b.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                Real = this.Real * b.Real - this.Imaginary * b.Imaginary,
                Imaginary = this.Real * b.Imaginary + this.Imaginary * b.Real
            };
        }

        public ComplexNumber Divide(ComplexNumber b)
        {
            var tmp = this.Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexNumber()
            {
                Real = tmp.Real / tmp2,
                Imaginary = tmp.Imaginary / tmp2
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}
