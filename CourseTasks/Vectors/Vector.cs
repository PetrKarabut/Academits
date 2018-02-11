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
            var vector = Size < other.Size ? Cut(other.Size) : this;

            for (var i = 0; i < other.Size; i++)
            {
                vector.components[i] += other.components[i];
            }

            components = vector.components;
        }

        public void Minus(Vector other)
        {
            var vector = Size < other.Size ? Cut(other.Size) : this;

            for (var i = 0; i < other.Size; i++)
            {
                vector.components[i] -= other.components[i];
            }

            components = vector.components;
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

        public static bool AreNear(Vector v, Vector w)
        {
            return Difference(v, w).Magnitude < epsilon;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != typeof(Vector) || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Vector)obj;

            if (other == this)
            {
                return true;
            }

            if (Size != other.Size)
            {
                return false;
            }

            bool equal = true;

            for (var i = 0; i < Size; i++)
            {
                if (components[i] != other.components[i])
                {
                    equal = false;
                }
            }

            return equal;
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
