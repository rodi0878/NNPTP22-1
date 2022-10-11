using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber numberA = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber numberB = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actual = numberA.Add(numberB);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = numberA.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(1 + 2i)";
            r2 = numberB.ToString();
            Assert.AreEqual(e2, r2);

            numberA = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            numberB = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = numberA.Add(numberB);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = numberA.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = numberB.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var r2 = polynomial.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


