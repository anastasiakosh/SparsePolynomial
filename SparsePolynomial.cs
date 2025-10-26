//Перше — описати методи та поля — вони public бо є частиною api. Hash code check зробити. Не реалізовуємо методи поки що  Реалізувати Дані класу: вказівник на динамічний список ненульових коефіцієнтів
using System;
using System.Collections.Generic;
using System.Text;


namespace SparsePolynomials
{
    public class SparsePolynomial
    {
        // Вузол для зберігання коефіцієнта та степеня
        private class Node
        {
            public int Exponent { get; set; }
            public double Coefficient { get; set; }
            public Node Next { get; set; }

            public Node(int exponent, double coefficient)
            {
                Exponent = exponent;
                Coefficient = coefficient;
                Next = null;
            }
        }

        private Node head; // Вказівник на початок списку

        public SparsePolynomial()
        {
            head = null;
        }

        // Додати Додати реалізацію методів( сума двох поліномів, віднімання одного поліному від іншого, множення до полінома числа та
        //додавання до полінома числа, обчислення значення полінома для заданого
        //значення змінної, перемноження двох поліномів)

        // Додати терм (член полінома) — допоміжний метод для формування полінома
        public void AddTerm(int exponent, double coefficient)
        {
            if (coefficient == 0)
                return;

            if (head == null)
            {
                head = new Node(exponent, coefficient);
                return;
            }

            Node current = head;
            Node prev = null;

            // Вставка з урахуванням порядку за степенем
            while (current != null && current.Exponent > exponent)
            {
                prev = current;
                current = current.Next;
            }

            if (current != null && current.Exponent == exponent)
            {
                // Якщо вже є такий степінь — додаємо коефіцієнти
                current.Coefficient += coefficient;
                if (Math.Abs(current.Coefficient) < 1e-12)
                {
                    // Якщо став нульовим — видаляємо
                    if (prev == null)
                        head = current.Next;
                    else
                        prev.Next = current.Next;
                }
            }
            else
            {
                Node newNode = new Node(exponent, coefficient);
                if (prev == null)
                {
                    newNode.Next = head;
                    head = newNode;
                }
                else
                {
                    newNode.Next = current;
                    prev.Next = newNode;
                }
            }
        }

        public SparsePolynomial Add(SparsePolynomial other)
        {
            // Реалізація додавання двох поліномів
            SparsePolynomial result = new SparsePolynomial();

            Node p1 = this.head;
            Node p2 = other.head;

            while (p1 != null && p2 != null)
            {
                if (p2 == null || (p1 != null && p1.Exponent > p2.Exponent))
                {
                    result.AddTerm(p1.Exponent, p1.Coefficient);
                    p1 = p1.Next;
                }
                else if (p1 == null || p1.Exponent < p2.Exponent)
                {
                    result.AddTerm(p2.Exponent, p2.Coefficient);
                    p2 = p2.Next;
                }
                else
                {
                    // однаковий степінь
                    result.AddTerm(p1.Exponent, p1.Coefficient + p2.Coefficient);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            return result;
        }
        public SparsePolynomial Subtract(SparsePolynomial other)
        {
            // Реалізація віднімання двох поліномів
            SparsePolynomial result = new SparsePolynomial();
            
            Node p1 = this.head;
            Node p2 = other.head;

            while (p1 != null && p2 != null)
            {
                if (p2 == null || (p1 != null && p1.Exponent > p2.Exponent))
                {
                    result.AddTerm(p1.Exponent, p1.Coefficient);
                    p1 = p1.Next;
                }
                else if (p1 == null || p1.Exponent < p2.Exponent)
                {
                    result.AddTerm(p2.Exponent, -p2.Coefficient);
                    p2 = p2.Next;
                }
                else
                {
                    // однаковий степінь
                    result.AddTerm(p1.Exponent, p1.Coefficient - p2.Coefficient);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            return result;
        }
        public SparsePolynomial Multiply(double scalar)
        {
            // Реалізація множення полінома на число
            SparsePolynomial result = new SparsePolynomial();

            if (Math.Abs(scalar) < 1e-12)
                return result; // Повертаємо нульовий поліном

            Node current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient * scalar);
                current = current.Next;
            }
            return result;
        }
        public SparsePolynomial Add(double scalar)
        {
            // Реалізація додавання до полінома числа
            SparsePolynomial result = new SparsePolynomial();

            Node current = head;
            while (current != null)
            {
                result.AddTerm(current.Exponent, current.Coefficient);
                current = current.Next;
            }
            result.AddTerm(0, scalar); // Додаємо константу
            return result;
        }
        public double Evaluate(double x)
        {
            // Реалізація обчислення значення полінома для заданого значення змінної
            double result = 0;
            Node current = head;
            while (current != null)
            {
                result += current.Coefficient * Math.Pow(x, current.Exponent);
                current = current.Next;
            }
            return result;
        }
        public SparsePolynomial Multiply(SparsePolynomial other)
        {
            // Реалізація перемноження двох поліномів
            SparsePolynomial result = new SparsePolynomial();

            Node p1 = this.head;
            while (p1 != null)
            {
                Node p2 = other.head;
                while (p2 != null)
                {
                    int exponent = p1.Exponent + p2.Exponent;
                    double coefficient = p1.Coefficient * p2.Coefficient;
                    result.AddTerm(exponent, coefficient);
                    p2 = p2.Next;
                }
                p1 = p1.Next;
            }
            return result;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            Node current = head;
            while (current != null)
            {
                hash = hash * 31 + current.Exponent.GetHashCode();
                hash = hash * 31 + current.Coefficient.GetHashCode();
                current = current.Next;
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is SparsePolynomial other)
            {
                Node current1 = head;
                Node current2 = other.head;

                while (current1 != null && current2 != null)
                {
                    if (current1.Exponent != current2.Exponent || Math.Abs(current1.Coefficient - current2.Coefficient) > 1e-12)
                    {
                        return false;
                    }
                    current1 = current1.Next;
                    current2 = current2.Next;
                }

                return current1 == null && current2 == null;
            }
            return false;
        }

        public override string ToString()
        {
            if (head == null) return "0";

            StringBuilder sb = new StringBuilder();
            Node current = head;

            while (current != null)
            {
                double coefficient = current.Coefficient;
                int exponent = current.Exponent;
                if (sb.Length > 0)
                {
                    sb.Append(coefficient >= 0 ? " + " : " - ");
                    coefficient = Math.Abs(coefficient);
                }
                else if (coefficient < 0)
                {
                    sb.Append("-");
                    coefficient = Math.Abs(coefficient);
                }
                if (exponent == 0)
                    sb.AppendFormat("{0} ", coefficient);
                else if (exponent == 1)
                    sb.AppendFormat("{0}x ", coefficient);
                else
                    sb.AppendFormat("{0}x^{1} ", Math.Abs(coefficient), exponent);
                
                current = current.Next;
            }
            return sb.ToString();
        }

    }
}