using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1Tests
{
    [TestClass()]
    public class PolynomialTests
    {

        [TestMethod()]
        public void AddTestPolynome()
        {

            Polynomial polynomial = new Polynomial();

            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(new ComplexNumber(0, 0));
            polynomial.Add(new ComplexNumber(1, 0));

            var result = polynomial.ToString();
            var expected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expected, result);



        }
        [TestMethod()]
        public void EvaluatePolynomial0()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(new ComplexNumber(0, 0));
            polynomial.Add(new ComplexNumber(1, 0));

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber(0, 0));
            var expected = new ComplexNumber(1, 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluatePolynomial1()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(new ComplexNumber(0, 0));
            polynomial.Add(new ComplexNumber(1, 0));

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber(1, 0));
            var expected = new ComplexNumber(2, 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluatePolynomial2()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber(1, 0));
            polynomial.Add(new ComplexNumber(0, 0));
            polynomial.Add(new ComplexNumber(1, 0));

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber(2, 0));
            var expected = new ComplexNumber(5.0000000000, 0);

            Assert.AreEqual(expected, result);


        }
    }
}