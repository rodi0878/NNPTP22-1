using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

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
                ImaginaryPart = (float)(this.RealPart * multiplicand.ImaginaryPart + this.ImaginaryPart * multiplicand.RealPart)
            };
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            var equationDivisor = Math.Pow(divisor.RealPart, 2) + Math.Pow(divisor.ImaginaryPart, 2);

            return new ComplexNumber()
            {
                RealPart = this.RealPart * divisor.RealPart + this.ImaginaryPart * divisor.ImaginaryPart / equationDivisor,
                ImaginaryPart = (float)(this.ImaginaryPart * divisor.RealPart - this.RealPart * divisor.ImaginaryPart / equationDivisor)
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double GetAngleInDegrees()
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
