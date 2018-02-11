using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Square : IShape
    {
        private double side;

        public Square(double side)
        {
            this.side = side;
        }

        public  double getArea()
        {
            return side * side;
        }

        public  double getPerimeter()
        {
            return 4 * side;
        }

        public  double getWidth()
        {
            return side;
        }

        public  double getHeight()
        {
            return side;
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

            var other = (Square)obj;

            return side == other.side;
        }

        public override int GetHashCode()
        {
            const int factor = 100;
            return (int)Math.Truncate(getPerimeter() * factor);
        }

        public override string ToString()
        {
            return $"Квадрат, сторона {side}";
        }
    }
}
