using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass()]
    public class PolynomeTests
    {
        [TestMethod()]
        public void Eval_should_eval_polynome()
        {
            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });

            ComplexNumber actual = polynome.Eval(new ComplexNumber() { Real = 0, Imaginary = 0 });
            var expected = new ComplexNumber() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, actual);

            actual = polynome.Eval(new ComplexNumber() { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, actual);

            actual = polynome.Eval(new ComplexNumber() { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Derive_should_derive_polynome()
        {
            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });

            var expected = new Polynome();
            expected.Coefficients.Add(new ComplexNumber { Real = 0, Imaginary = 0 });
            expected.Coefficients.Add(new ComplexNumber { Real = 2, Imaginary = 0 });

            var actual = polynome.Derive();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_should_return_correct_string_format()
        {
            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });

            var expected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            var actual = polynome.ToString();
            Assert.AreEqual(expected, actual);


            polynome.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 2 });
            polynome.Coefficients.Add(new ComplexNumber() { Real = 5, Imaginary = 0 });

            expected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx + (0 + 2i)xxx + (5 + 0i)xxxx";
            actual = polynome.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}