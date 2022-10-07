using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void Add_should_sum_two_complex_numbers()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new ComplexNumber() { Real = 0, Imaginary = 0 };
            actual = a.Add(b);

            shouldBe = new ComplexNumber() { Real = 1, Imaginary = -1 };

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod]
        public void Subtract_should_subtract_two_complex_numbers()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };
            ComplexNumber actual = a.Subtract(b);

            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 9,
                Imaginary = 18
            };

            Assert.AreEqual(shouldBe, actual);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 0
            };
            b = new ComplexNumber() { Real = 0, Imaginary = -1 };
            actual = a.Subtract(b);

            shouldBe = new ComplexNumber() { Real = 1, Imaginary = 1 };
            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod]
        public void Multiply_should_multiply_two_complex_numbers()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };
            ComplexNumber actual = a.Multiply(b);

            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = -30,
                Imaginary = 40
            };

            Assert.AreEqual(shouldBe, actual);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 3
            };
            b = new ComplexNumber() { Real = 2, Imaginary = -1 };
            actual = a.Multiply(b);

            shouldBe = new ComplexNumber() { Real = 5, Imaginary = 5 };
            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod]
        public void Divide_should_divide_two_complex_numbers()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };
            ComplexNumber actual = a.Divide(b);

            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 0
            };

            Assert.AreEqual(shouldBe, actual);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 3
            };
            b = new ComplexNumber() { Real = 2, Imaginary = -1 };
            actual = a.Divide(b);

            shouldBe = new ComplexNumber() { Real = -0.2, Imaginary = 1.4 };
            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod]
        public void ToString_should_return_correct_string_format()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };

            var expected = "(10 + 20i)";
            var actual = a.ToString();
            Assert.AreEqual(expected, actual);


            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = -1
            };

            expected = "(1 + -1i)";
            actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}