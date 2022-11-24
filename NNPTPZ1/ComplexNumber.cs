using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {

        public readonly static ComplexNumber Zero = new ComplexNumber(0, 0);

        public ComplexNumber(double Real, double Imaginary)
        {
            this.Real = Real;
            this.Imaginary = Imaginary;
        }

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber(this.Real * multiplier.Real - this.Imaginary * multiplier.Imaginary, this.Real * multiplier.Imaginary + this.Imaginary * multiplier.Real);
        }

        public ComplexNumber Subtract(ComplexNumber substraction)
        {
            return new ComplexNumber(this.Real - substraction.Real, this.Imaginary - substraction.Imaginary);
        }

        public ComplexNumber Add(ComplexNumber addition)
        {
            return new ComplexNumber(this.Real + addition.Real, this.Imaginary + addition.Imaginary);
        }

        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            var numerator = this.Multiply(new ComplexNumber(divisor.Real, -divisor.Imaginary));
            var denumerator = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;
            return new ComplexNumber(numerator.Real / denumerator, numerator.Imaginary / denumerator);
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber complexNumberExpression) {
                return complexNumberExpression.Real == Real && complexNumberExpression.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

    }
}