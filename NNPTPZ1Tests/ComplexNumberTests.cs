using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber { RealPart = 10, ImaginaryPart = 20 };
            ComplexNumber b = new ComplexNumber { RealPart = 1, ImaginaryPart = 2 };
            ComplexNumber shouldBe = new ComplexNumber { RealPart = 11, ImaginaryPart = 22 };

            ComplexNumber actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";

            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(1 + 2i)";

            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber { RealPart = 1, ImaginaryPart = -1 };
            b = new ComplexNumber { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new ComplexNumber { RealPart = 1, ImaginaryPart = -1 };

            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";

            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";

            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynomial()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Coefficients.Add(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { RealPart = 0, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });

            var expected = new ComplexNumber { RealPart = 1, ImaginaryPart = 0 };

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { RealPart = 0, ImaginaryPart = 0 });
            Assert.AreEqual(expected, result);

            expected = new ComplexNumber { RealPart = 2, ImaginaryPart = 0 };

            result = polynomial.Evaluate(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });
            Assert.AreEqual(expected, result);

            expected = new ComplexNumber { RealPart = 5.0000000000, ImaginaryPart = 0 };

            result = polynomial.Evaluate(new ComplexNumber { RealPart = 2, ImaginaryPart = 0 });
            Assert.AreEqual(expected, result);

            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";

            var r2 = polynomial.ToString();
            Assert.AreEqual(e2, r2);
        }
    }
}