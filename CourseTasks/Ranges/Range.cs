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

        public double Length
        {
            get
            {
                return to - from;
            }
        }

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

        public Range[] GetAssociation(Range other)
        {
            if (GetIntersection(other) != null)
            {
                return new Range[] { new Range(Math.Min(from, other.from), Math.Max(to, other.to)) };
            }
            else
            {
                return new Range[] { this, other };
            }
        }

        public Range[] GetDifference(Range other)
        {
            var fromIsInside = IsInside(other.from);
            var toIsInside = IsInside(other.to);
            var range1 = new Range(from, other.from);
            var range2 = new Range(other.to, to);

            if (fromIsInside && toIsInside)
            {
                return range1.GetAssociation(range2);
            }
            else if (fromIsInside)
            {
                return new Range[] { range1 };
            }
            else if (toIsInside)
            {
                return new Range[] { range2 };
            }
            else
            {
                return new Range[] { this };
            }
        }

        public string Note
        {
            get
            {
                return string.Format("[{0};{1}]", from, to);
            }
        }

        public static string GetNotes(Range[] ranges)
        {
            var notes = new string[ranges.Length];
            for (int i = 0; i < ranges.Length; i++)
            {
                notes[i] = ranges[i].Note;
            }

            return string.Join(" U ", notes);
        }
    }
}
