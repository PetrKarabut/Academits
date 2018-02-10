using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors
{
    class Vector
    {
        private double[] components;

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("размерность вектора меньше единицы", "size");
            }

            components = new double[size];
        }

        public Vector(int size, double[] array) : this(size)
        {
            var minimum = Math.Min(size, array.Length);
            Array.Copy(array, components, minimum);
        }

        public Vector(double[] array) : this(array.Length, array)
        {
        }

        public Vector(Vector other) : this(other.components)
        {
        }

        public int Size => components.Length;

        public override string ToString()
        {
            return $"({string.Join(";", components)})";
        }

        public Vector Cut(int size)
        {
            return new Vector(size, components);
        }

        public void Plus(Vector other)
        {
            var minimumSize = Math.Min(Size, other.Size);

            for (var i = 0; i < minimumSize; i++)
            {
                components[i] += other.components[i];
            }
        }

        public void Minus(Vector other)
        {
            var minimumSize = Math.Min(Size, other.Size);

            for (var i = 0; i < minimumSize; i++)
            {
                components[i] -= other.components[i];
            }
        }

        public void Multiply(double x)
        {
            for (var i = 0; i < Size; i++)
            {
                components[i] *= x;
            }
        }

        public void Turn()
        {
            Multiply(-1);
        }

        public double Magnitude => Math.Sqrt(Dot(this, this));

        public double GetComponent(int i)
        {
            return components[i];
        }

        public void SetComponent(int i, double value)
        {
            components[i] = value;
        }

        private const double epsilon = double.Epsilon * 100;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Vector)obj;

            return Size == other.Size && Difference(this, other).Magnitude < epsilon;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(Magnitude * factor);
        }

        public static double Dot(Vector v, Vector w)
        {
            var minimumSize = Math.Min(v.Size, w.Size);

            double dot = 0;
            for (var i = 0; i < minimumSize; i++)
            {
                dot += w.components[i] * v.components[i];
            }

            return dot;
        }

        public static Vector Sum(Vector vector1, Vector vector2)
        {
            var maximumSize = Math.Max(vector1.Size, vector2.Size);

            var vector = vector1.Cut(maximumSize);
            vector.Plus(vector2.Cut(maximumSize));

            return vector;
        }

        public static Vector Difference(Vector vector1, Vector vector2)
        {
            var maximumSize = Math.Max(vector1.Size, vector2.Size);

            var vector = vector1.Cut(maximumSize);
            vector.Minus(vector2.Cut(maximumSize));

            return vector;
        }
    }
}
