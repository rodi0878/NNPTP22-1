using System;

namespace Mathematics
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

        public double GetAbs()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }
        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                ImaginaryPart = a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart
            };
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart + b.RealPart,
                ImaginaryPart = a.ImaginaryPart + b.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart - b.RealPart,
                ImaginaryPart = a.ImaginaryPart - b.ImaginaryPart
            };
        }
        public override bool Equals(object objectToCompare)
        {
            if (objectToCompare is ComplexNumber)
            {
                ComplexNumber complexNumber = objectToCompare as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(objectToCompare);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
        internal ComplexNumber Divide(ComplexNumber b)
        {
            ComplexNumber numerator = this.Multiply(new ComplexNumber() { RealPart = b.RealPart, ImaginaryPart = -b.ImaginaryPart }); // a * conjugate b
            double denominator = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = numerator.ImaginaryPart / denominator
            };
        }
    }
}
