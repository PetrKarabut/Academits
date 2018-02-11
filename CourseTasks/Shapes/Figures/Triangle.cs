using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Triangle : IShape
    {
        private double side1;
        private double side2;
        private double side3;

        private double width;
        private double height;

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (Math.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) < epsilon)
            {
                throw new ArgumentException("Все точки лежат на одной прямой");
            }

            side1 = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            side2 = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3));
            side3 = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3));

            width = Math.Max(x1, Math.Max(x2, x3)) - Math.Min(x1, Math.Min(x2, x3));
            height = Math.Max(y1, Math.Max(y2, y3)) - Math.Min(y1, Math.Min(y2, y3));
        }

        private double epsilon = double.Epsilon * 100;

        public double getArea()
        {
            var p = getPerimeter() / 2;
            return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
        }

        public double getPerimeter()
        {
            return side1 + side2 + side3;
        }

        public double getWidth()
        {
            return width;
        }

        public double getHeight()
        {
            return height;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Triangle)obj;

            return side1 == other.side1 && side2 == other.side2 && side3 == other.side3;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(getPerimeter() * factor);
        }

        public override string ToString()
        {
            return $"Треугольник, стороны: {side1}, {side2}, {side3}";
        }
    }
}
