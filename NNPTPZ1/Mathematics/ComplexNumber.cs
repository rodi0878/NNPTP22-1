namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealNumber { get; set; }
        public double ImaginaryUnit { get; set; }

        public override bool Equals(object objectToCompare)
        {
            if (objectToCompare is ComplexNumber)
            {
                ComplexNumber complexNumberToCompare = objectToCompare as ComplexNumber;
                return complexNumberToCompare.RealNumber == RealNumber && complexNumberToCompare.ImaginaryUnit == ImaginaryUnit;
            }
            return base.Equals(objectToCompare);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealNumber = 0,
            ImaginaryUnit = 0
        };

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            ComplexNumber multiplier = this;
            return new ComplexNumber()
            {
                RealNumber = multiplier.RealNumber * multiplicand.RealNumber - multiplier.ImaginaryUnit * multiplicand.ImaginaryUnit,
                ImaginaryUnit = multiplier.RealNumber * multiplicand.ImaginaryUnit + multiplier.ImaginaryUnit * multiplicand.RealNumber
            };
        }

        public ComplexNumber Add(ComplexNumber termToAdd)
        {
            ComplexNumber term = this;
            return new ComplexNumber()
            {
                RealNumber = term.RealNumber + termToAdd.RealNumber,
                ImaginaryUnit = term.ImaginaryUnit + termToAdd.ImaginaryUnit
            };
        }


        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            ComplexNumber minuend = this;
            return new ComplexNumber()
            {
                RealNumber = minuend.RealNumber - subtrahend.RealNumber,
                ImaginaryUnit = minuend.ImaginaryUnit - subtrahend.ImaginaryUnit
            };
        }

        public override string ToString()
        {
            return $"({RealNumber} + {ImaginaryUnit}i)";
        }

        internal ComplexNumber Divide(ComplexNumber complexNumberForDivision)
        {
            var dividend = this.Multiply(new ComplexNumber() { RealNumber = complexNumberForDivision.RealNumber, ImaginaryUnit = -complexNumberForDivision.ImaginaryUnit });
            var divisor = complexNumberForDivision.RealNumber * complexNumberForDivision.RealNumber + complexNumberForDivision.ImaginaryUnit * complexNumberForDivision.ImaginaryUnit;

            return new ComplexNumber()
            {
                RealNumber = dividend.RealNumber / divisor,
                ImaginaryUnit = (dividend.ImaginaryUnit / divisor)
            };
        }
    }
}
}
