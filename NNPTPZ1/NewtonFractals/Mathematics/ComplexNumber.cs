using System;

namespace NNPTPZ1.NewtonFractals.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber comparativeComplexNumber)
            {
                return comparativeComplexNumber.RealPart == RealPart &&
                       comparativeComplexNumber.ImaginaryPart == ImaginaryPart;
            }

            return base.Equals(obj);
        }

        public static readonly ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * multiplier.RealPart - ImaginaryPart * multiplier.ImaginaryPart,
                ImaginaryPart = RealPart * multiplier.ImaginaryPart + ImaginaryPart * multiplier.RealPart
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + addend.RealPart,
                ImaginaryPart = ImaginaryPart + addend.ImaginaryPart
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - subtrahend.RealPart,
                ImaginaryPart = ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            ComplexNumber complexNumberDivident = this.Multiply(new ComplexNumber()
            {
                RealPart = divisor.RealPart,
                ImaginaryPart = -divisor.ImaginaryPart
            });

            double complexNumberDivisor = Math.Pow(divisor.RealPart, 2) + Math.Pow(divisor.ImaginaryPart, 2);

            return new ComplexNumber()
            {
                RealPart = complexNumberDivident.RealPart / complexNumberDivisor,
                ImaginaryPart = complexNumberDivident.ImaginaryPart / complexNumberDivisor
            };
        }
    }
}
