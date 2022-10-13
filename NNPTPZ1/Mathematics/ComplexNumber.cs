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

        public override bool Equals(object objectToCompare)
        {
            if (objectToCompare is ComplexNumber)
            {
                ComplexNumber ComplexNumberToCompare = objectToCompare as ComplexNumber;
                return ComplexNumberToCompare.RealPart == RealPart && ComplexNumberToCompare.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(objectToCompare);
        }

        public ComplexNumber Multiply(ComplexNumber complexNumberToMultiply)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * complexNumberToMultiply.RealPart - ImaginaryPart * complexNumberToMultiply.ImaginaryPart,
                ImaginaryPart = (RealPart * complexNumberToMultiply.ImaginaryPart + ImaginaryPart * complexNumberToMultiply.RealPart)
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber complexNumberToAdd)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + complexNumberToAdd.RealPart,
                ImaginaryPart = ImaginaryPart + complexNumberToAdd.ImaginaryPart
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber complexNumberToSubtract)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - complexNumberToSubtract.RealPart,
                ImaginaryPart = ImaginaryPart - complexNumberToSubtract.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber complexNumberToDivide)
        {
            ComplexNumber tempComplexNumber = Multiply(new ComplexNumber() { RealPart = complexNumberToDivide.RealPart, ImaginaryPart = -complexNumberToDivide.ImaginaryPart });
            double tempResultForDivision = Math.Pow(complexNumberToDivide.RealPart, 2) + Math.Pow(complexNumberToDivide.ImaginaryPart, 2);
            return new ComplexNumber()
            {
                RealPart = tempComplexNumber.RealPart / tempResultForDivision,
                ImaginaryPart = tempComplexNumber.ImaginaryPart / tempResultForDivision
            };
        }
    }
}
