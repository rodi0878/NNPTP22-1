using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {

            ComplexNumber a = new ComplexNumber() { RealPart = 10, ImaginaryPart = 20 };
            Assert.AreEqual("(10 + 20i)", a.ToString());

            ComplexNumber b = new ComplexNumber() { RealPart = 1, ImaginaryPart = 2 };
            Assert.AreEqual("(1 + 2i)", b.ToString());

            ComplexNumber actual = a.Add(b);
            ComplexNumber expected = new ComplexNumber() { RealPart = 11, ImaginaryPart = 22 };

            Assert.AreEqual(expected, actual);



            a = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            Assert.AreEqual("(1 + -1i)", a.ToString());

            b = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            Assert.AreEqual("(0 + 0i)", b.ToString());


            actual = a.Add(b);
            expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            ComplexNumber expected, actual;
            Polynomial polynomial = new Polynomial();

            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", polynomial.ToString());

            actual = polynomial.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, actual);

            actual = polynomial.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, actual);
            
            actual = polynomial.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, actual);

        }
    }
}


