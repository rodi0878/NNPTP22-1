using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class ComplexNumber
    {

        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber numberForComparision = obj as ComplexNumber;
                return numberForComparision.RealPart == RealPart && numberForComparision.ImaginaryPart == ImaginaryPart;
            }
            return false;
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber argumentComplexNumber)
        {
            ComplexNumber baseComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart * argumentComplexNumber.RealPart - baseComplexNumber.ImaginaryPart * argumentComplexNumber.ImaginaryPart,
                ImaginaryPart = baseComplexNumber.RealPart * argumentComplexNumber.ImaginaryPart + baseComplexNumber.ImaginaryPart * argumentComplexNumber.RealPart
            };
        }
        public double GetAbS()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber argumentComplexNumber)
        {
            ComplexNumber baseComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart + argumentComplexNumber.RealPart,
                ImaginaryPart = baseComplexNumber.ImaginaryPart + argumentComplexNumber.ImaginaryPart
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }
        public ComplexNumber Subtract(ComplexNumber argumentComplexNumber)
        {
            ComplexNumber baseComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart - argumentComplexNumber.RealPart,
                ImaginaryPart = baseComplexNumber.ImaginaryPart - argumentComplexNumber.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber dividingNumber)
        {
            ComplexNumber dividedNumber = this;
            double calculatedDivider = Math.Pow(dividingNumber.RealPart, 2) + Math.Pow(dividingNumber.ImaginaryPart, 2);

            return new ComplexNumber()
            {
                RealPart = (dividedNumber.RealPart * dividingNumber.RealPart + dividedNumber.ImaginaryPart * dividingNumber.ImaginaryPart) / calculatedDivider,
                ImaginaryPart = (dividedNumber.ImaginaryPart * dividingNumber.RealPart - dividedNumber.RealPart * dividingNumber.ImaginaryPart) / calculatedDivider
            };
        }
    }
}
