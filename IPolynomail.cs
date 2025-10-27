#nullable enable
using System;

namespace SparsePolynomials
{
    public interface IPolynomial
    {
        void AddTerm(int exponent, double coefficient);
        IPolynomial Add(IPolynomial other);
        IPolynomial Subtract(IPolynomial other);
        IPolynomial Multiply(double scalar);
        IPolynomial Add(double scalar);
        IPolynomial Multiply(IPolynomial other);
        double Evaluate(double x);
    }
}
