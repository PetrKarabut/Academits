using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranges
{
    class Range
    {
        private double from;
        private double to;

        public Range(double from, double to)
        {
            this.from = from;
            this.to = to;
        }

        public double From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        public double To
        {
            get
            {
                return to;
            }

            set
            {
                to = value;
            }
        }

        public double Length => to - from;

        public Range clone => new Range(from, to);

        public bool IsInside(double x)
        {
            return (x >= from && x <= to);
        }

        public Range GetIntersection(Range other)
        {
            if (IsInside(other.from))
            {
                return new Range(other.from, Math.Min(to, other.to));
            }
            else if (other.IsInside(from))
            {
                return new Range(Math.Max(from, other.from), other.to);
            }
            else
            {
                return null;
            }
        }

        public Range[] GetUnion(Range other)
        {
            if (GetIntersection(other) != null)
            {
                return new[] { new Range(Math.Min(from, other.from), Math.Max(to, other.to)) };
            }
            else
            {
                return new[] { clone, other.clone };
            }
        }

        // здесь разность это интервал(или два интервала), содержащий все числа из
        // первого интервала не содержащиеся во втором интервале
        public Range[] GetDifference(Range other)
        {
            var fromIsInside = IsInside(other.from);
            var toIsInside = IsInside(other.to);
            var range1 = new Range(from, other.from);
            var range2 = new Range(other.to, to);

            if (fromIsInside && toIsInside)
            {
                return range1.GetUnion(range2);
            }
            else if (fromIsInside)
            {
                return new[] { range1 };
            }
            else if (toIsInside)
            {
                return new[] { range2 };
            }
            else
            {
                return new[] { clone};
            }
        }

        public string Note
        {
            get
            {
                return $"[{from};{to}]";
            }
        }

        public static string GetNotes(Range[] ranges)
        {
            var notes = new string[ranges.Length];
            for (var i = 0; i < ranges.Length; i++)
            {
                notes[i] = ranges[i].Note;
            }

            return string.Join(" U ", notes);
        }
    }
}
