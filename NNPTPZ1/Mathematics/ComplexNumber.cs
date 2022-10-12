using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart + addend.RealPart,
                ImaginaryPart = this.ImaginaryPart + addend.ImaginaryPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart - subtrahend.RealPart,
                ImaginaryPart = this.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart * multiplicand.RealPart - this.ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = this.RealPart * multiplicand.ImaginaryPart + this.ImaginaryPart * multiplicand.RealPart
            };
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            double equationDivisor = Math.Pow(divisor.RealPart, 2) + Math.Pow(divisor.ImaginaryPart, 2);

            return new ComplexNumber()
            {
                RealPart = (this.RealPart * divisor.RealPart + this.ImaginaryPart * divisor.ImaginaryPart) / equationDivisor,
                ImaginaryPart = (this.ImaginaryPart * divisor.RealPart - this.RealPart * divisor.ImaginaryPart) / equationDivisor
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Math.Pow(RealPart, 2) + Math.Pow(ImaginaryPart, 2));
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
