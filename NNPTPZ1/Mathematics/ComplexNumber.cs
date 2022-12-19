using System;

namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealValue { get; set; }
        public double ImaginaryValue { get; set; }
        public readonly static ComplexNumber SetPropertiesZero = new ComplexNumber()
        {
            RealValue = 0,
            ImaginaryValue = 0
        };

        public override bool Equals(object objectToCompare)
        {
            if (objectToCompare is ComplexNumber)
            {
                ComplexNumber x = objectToCompare as ComplexNumber;
                return x.RealValue == RealValue && x.ImaginaryValue == ImaginaryValue;
            }
            return base.Equals(objectToCompare);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealValue = this.RealValue + b.RealValue,
                ImaginaryValue = this.ImaginaryValue + b.ImaginaryValue
            };
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealValue = this.RealValue - b.RealValue,
                ImaginaryValue = this.ImaginaryValue - b.ImaginaryValue
            };
        }

        public ComplexNumber Multiply(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                RealValue = this.RealValue * b.RealValue - this.ImaginaryValue * b.ImaginaryValue,
                ImaginaryValue = this.RealValue * b.ImaginaryValue + this.ImaginaryValue * b.RealValue
            };
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            var temporary = this.Multiply(new ComplexNumber() { RealValue = b.RealValue, ImaginaryValue = -b.ImaginaryValue });
            var temporary2 = b.RealValue * b.RealValue + b.ImaginaryValue * b.ImaginaryValue;

            return new ComplexNumber()
            {
                RealValue = temporary.RealValue / temporary2,
                ImaginaryValue = temporary.ImaginaryValue / temporary2
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealValue * RealValue + ImaginaryValue * ImaginaryValue);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryValue / RealValue);
        }

        public override string ToString()
        {
            return $"({RealValue} + {ImaginaryValue}i)";
        }
    }
}
