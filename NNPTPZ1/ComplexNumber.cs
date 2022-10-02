using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            // (aRe * bRe - aIm * bIm) + i(aRe * bIm + aIm * bRe)

            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                ImaginaryPart = (float)(a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Sum(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart + b.RealPart,
                ImaginaryPart = a.ImaginaryPart + b.ImaginaryPart
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
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

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        public ComplexNumber Divide(ComplexNumber b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            //  ((aRe * bRe + aIm * bIm) + i(aIm * bRe - aRe * bIm)) / (bRe^2 + bIm^2) 
            
            ComplexNumber a = this;
            double realPartNumerator = a.RealPart * b.RealPart + a.ImaginaryPart * b.ImaginaryPart;
            double denominator = Math.Pow(b.RealPart, 2) + Math.Pow(b.ImaginaryPart, 2);
            double realPart = realPartNumerator / denominator;
            double imaginaryPartNumerator = a.ImaginaryPart * b.RealPart - a.RealPart * b.ImaginaryPart;
            double imaginaryPart = imaginaryPartNumerator / denominator;

            return new ComplexNumber()
            {
                RealPart = realPart,
                ImaginaryPart = (float) imaginaryPart
            };
        }
    }
}