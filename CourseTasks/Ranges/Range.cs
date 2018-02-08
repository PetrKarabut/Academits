using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranges
{
    class Range
    {
        public double From { get; set; }

        public double To { get; set; }

        public Range(double from, double to)
        {
            From = from;
            To = to;
        }

        public double Length => To - From;

        public Range Clone => new Range(From, To);

        public bool IsInside(double x)
        {
            return (x >= From && x <= To);
        }

        private const double epsilon = double.Epsilon * 100;

        private bool isEqual(double x, double y)
        {
            return Math.Abs(x - y) < epsilon;
        }

        public Range GetIntersection(Range other)
        {
            if (isEqual(To, other.From) || isEqual(From, other.To))
            {
                return null;
            }


            if (IsInside(other.From))
            {
                return new Range(other.From, Math.Min(To, other.To));
            }
            else if (other.IsInside(From))
            {
                return new Range(Math.Max(From, other.From), other.To);
            }
            else
            {
                return null;
            }
        }

        public Range[] GetUnion(Range other)
        {
            if (IsInside(other.From) || other.IsInside(From))
            {
                return new[] { new Range(Math.Min(From, other.From), Math.Max(To, other.To)) };
            }
            else
            {
                return new[] { Clone, other.Clone };
            }
        }

     


        // здесь разность это интервал(или два интервала), содержащий все числа из
        // первого интервала не содержащиеся во втором интервале
        public Range[] GetDifference(Range other)
        {
            if (other.IsInside(From) && other.IsInside(To))
            {
                return null;
            }

            if (isEqual(From, other.From))
            {
                return new[] { new Range(other.To, To) };
            }

            if (isEqual(To, other.To))
            {
                return new[] { new Range(From, other.From) };
            }

            var fromIsInside = IsInside(other.From);
            var toIsInside = IsInside(other.To);

            if (fromIsInside && toIsInside)
            {
                var range1 = new Range(From, other.From);
                var range2 = new Range(other.To, To);
                return range1.GetUnion(range2);
            }
            else if (fromIsInside)
            {
                return new[] { new Range(From, other.From) };
            }
            else if (toIsInside)
            {
                return new[] { new Range(other.To, To) };
            }
            else
            {
                return new[] { Clone };
            }
        }

        public string Note => $"[{From};{To}]";

        public static string GetNotes(Range[] ranges)
        {
            if (ranges == null)
            {
                return "пустое множество";
            }

            var notes = new string[ranges.Length];
            for (var i = 0; i < ranges.Length; i++)
            {
                notes[i] = ranges[i].Note;
            }

            return string.Join(" U ", notes);
        }
    }
}
