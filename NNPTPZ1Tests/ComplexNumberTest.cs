using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber() { RealPart = 10, ImaginaryPart = 20 };
            ComplexNumber b = new ComplexNumber() { RealPart = 1, ImaginaryPart = 2 };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber() { RealPart = 11, ImaginaryPart = 22 };

            Assert.AreEqual(shouldBe, actual);

            Assert.AreEqual("(10 + 20i)", a.ToString());
            Assert.AreEqual("(1 + 2i)", b.ToString());

            a = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            b = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            Assert.AreEqual("(1 + -1i)", a.ToString());
            Assert.AreEqual("(0 + 0i)", b.ToString());
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial poly = new Polynomial();
            poly.ComplexNumberList.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            poly.ComplexNumberList.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            poly.ComplexNumberList.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber result = poly.Eval(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", poly.ToString());
        }
    }
}
