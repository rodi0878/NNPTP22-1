using System;

namespace NNPTPZ1.NewtonFractal.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        public static readonly ComplexNumber Zero = new ComplexNumber { RealPart = 0, ImaginaryPart = 0 };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber complexNumber)
            {
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }

            return base.Equals(obj);
        }

        public ComplexNumber Multiply(ComplexNumber multiplier) => new ComplexNumber
        {
            RealPart = RealPart * multiplier.RealPart - ImaginaryPart * multiplier.ImaginaryPart,
            ImaginaryPart = (float)(RealPart * multiplier.ImaginaryPart + ImaginaryPart * multiplier.RealPart)
        };

        public double GetAbsValue() => Math.Sqrt(Math.Pow(RealPart, 2) + Math.Pow(ImaginaryPart, 2));

        public ComplexNumber Add(ComplexNumber addend) => new ComplexNumber
        {
            RealPart = RealPart + addend.RealPart,
            ImaginaryPart = ImaginaryPart + addend.ImaginaryPart
        };

        public double GetAngleInRadians() => Math.Atan(ImaginaryPart / RealPart);

        public ComplexNumber Subtract(ComplexNumber subtrahend) => new ComplexNumber
        {
            RealPart = RealPart - subtrahend.RealPart,
            ImaginaryPart = ImaginaryPart - subtrahend.ImaginaryPart
        };

        public override string ToString() => $"({RealPart} + {ImaginaryPart}i)";

        public ComplexNumber Divide(ComplexNumber complexNumber)
        {
            var dividend = Multiply(new ComplexNumber { RealPart = complexNumber.RealPart, ImaginaryPart = -complexNumber.ImaginaryPart });
            var divisor = Math.Pow(complexNumber.RealPart, 2) + Math.Pow(complexNumber.ImaginaryPart, 2);

            return new ComplexNumber
            {
                RealPart = dividend.RealPart / divisor,
                ImaginaryPart = (float)(dividend.ImaginaryPart / divisor)
            };
        }
    }
}