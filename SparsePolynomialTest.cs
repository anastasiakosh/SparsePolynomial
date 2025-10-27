#nullable enable
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparsePolynomials;

namespace SparsePolynomialTests
{
    [TestClass]
    public class SparsePolynomialTestSuite
    {
        [TestMethod]
        public void TestAdd()
        {
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 3.0); // 3x^2
            poly1.AddTerm(0, 1);   // +1

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(2, -3);  // -3x^2
            poly2.AddTerm(1, 5);   // +5x

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(1, 5);
            expected.AddTerm(0, 1);

            IPolynomial result = poly1.Add(poly2);
            Assert.IsTrue(expected.Equals(result), "Addition of polynomials failed");
        }

        [TestMethod]
        public void TestSubtract()
        {
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 4);
            poly1.AddTerm(1, 3);

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(1, 1);
            poly2.AddTerm(0, 2);

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 4);
            expected.AddTerm(1, 2);
            expected.AddTerm(0, -2);

            IPolynomial result = poly1.Subtract(poly2);
            Assert.IsTrue(expected.Equals(result), "Subtraction of polynomials failed");
        }

        [TestMethod]
        public void TestMultiplyByNumber()
        {
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, 2);
            poly.AddTerm(1, -3);
            poly.AddTerm(0, 1);

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 4);
            expected.AddTerm(1, -6);
            expected.AddTerm(0, 2);

            IPolynomial result = poly.Multiply(2.0);
            Assert.IsTrue(expected.Equals(result), "Multiplication by scalar failed");
        }

        [TestMethod]
        public void TestAddNumber()
        {
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(1, 2);
            poly.AddTerm(0, 3);

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(1, 2);
            expected.AddTerm(0, 8);

            IPolynomial result = poly.Add(5);
            Assert.IsTrue(expected.Equals(result), "Addition of number failed");
        }

        [TestMethod]
        public void TestMultiplyPolynomials()
        {
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(1, 1);
            poly1.AddTerm(0, 1); // x + 1

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(1, 1);
            poly2.AddTerm(0, -1); // x - 1

            SparsePolynomial expected = new SparsePolynomial();
            expected.AddTerm(2, 1); // x^2
            expected.AddTerm(0, -1); // -1

            IPolynomial result = poly1.Multiply(poly2);
            Assert.IsTrue(expected.Equals(result), "Polynomial multiplication failed");
        }

        [TestMethod]
        public void TestEvaluate()
        {
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, 2); // 2x^2
            poly.AddTerm(1, 3); // +3x
            poly.AddTerm(0, 4); // +4

            double expected = 2 * Math.Pow(2, 2) + 3 * 2 + 4; // 18
            double result = poly.Evaluate(2.0);

            Assert.AreEqual(expected, result, 1e-9, "Polynomial evaluation failed");
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            SparsePolynomial poly1 = new SparsePolynomial();
            poly1.AddTerm(2, 3);
            poly1.AddTerm(0, 1);

            SparsePolynomial poly2 = new SparsePolynomial();
            poly2.AddTerm(2, 3);
            poly2.AddTerm(0, 1);

            int hash1 = poly1.GetHashCode();
            int hash2 = poly2.GetHashCode();

            Assert.AreEqual(hash1, hash2, "Hash codes should be equal for identical polynomials");
        }

        [TestMethod]
        public void TestToString()
        {
            SparsePolynomial poly = new SparsePolynomial();
            poly.AddTerm(2, -3);
            poly.AddTerm(1, 2);
            poly.AddTerm(0, 1);

            string expected = "-3x^2 + 2x + 1";
            string result = poly.ToString().Trim();

            Assert.AreEqual(expected, result, "ToString method failed");
        }
    }
}
