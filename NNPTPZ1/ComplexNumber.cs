using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }       

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * multiplier.RealPart - ImaginaryPart * multiplier.ImaginaryPart,
                ImaginaryPart = (float)(RealPart * multiplier.ImaginaryPart + ImaginaryPart * multiplier.RealPart)
            };
        }

        public double GetAbsoluteValue()
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

        internal ComplexNumber Divide(ComplexNumber number)
        {
            var dividend = Multiply(new ComplexNumber() { RealPart = number.RealPart, ImaginaryPart = -number.ImaginaryPart });
            var divisor = number.RealPart * number.RealPart + number.ImaginaryPart * number.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = dividend.RealPart / divisor,
                ImaginaryPart = (float)(dividend.ImaginaryPart / divisor)
            };
        }
    }
}
