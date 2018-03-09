using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Graph
    {
        private int[,] transitions;

        private int length;

        private bool[] visited;

        public Graph(int[,] transitions)
        {
            if (transitions.GetLength(0) != transitions.GetLength(1))
            {
                throw new ArgumentException("Матрица ребер должна быть квадратной");
            }

            length = transitions.GetLength(0);

            this.transitions = new int[length, length];

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    this.transitions[i, j] = transitions[i, j];

                    if (transitions[i, j] != transitions[j, i])
                    {
                        throw new ArgumentException($"Матрица ребер дожна быть симметричной. Симметричность нарушена по индексам {i}, {j} ");
                    }
                }
            }
        }

        public IEnumerable<int> Broadways()
        {
            visited = new bool[length];
            return Broadways(0);
        }

        public IEnumerable<int> Scan()
        {
            visited = new bool[length];
            return Scan(0);
        }

        private IEnumerable<int> Broadways(int begining)
        {
            var queue = new Queue<int>();

            queue.Enqueue(begining);
            visited[begining] = true;

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                yield return next;

                for (var i = 0; i < length; i++)
                {
                    if (i == next || visited[i])
                    {
                        continue;
                    }

                    var transition = transitions[next, i];

                    if (transition == 1)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                    }
                }
            }

            for (var i = 0; i < length; i++)
            {
                if (!visited[i])
                {
                    foreach (var n in Broadways(i))
                    {
                        yield return n;
                    }

                    break;
                }
            }
            
        }

        private IEnumerable<int> Scan(int begining)
        {
            var stack = new Stack<int>();
            stack.Push(begining);
            visited[begining] = true;

            while (stack.Count > 0)
            {
                var next = stack.Pop();
                yield return next;

                for (var i = length - 1; i >= 0; i--)
                {
                    if (i == next || visited[i])
                    {
                        continue;
                    }

                    var transition = transitions[next, i];

                    if (transition == 1)
                    {
                        stack.Push(i);
                        visited[i] = true;
                    }
                }

            }

            for (var i = 0; i < length; i++)
            {
                if (!visited[i])
                {
                    foreach (var n in Scan(i))
                    {
                        yield return n;
                    }

                    break;
                }
            }

        }


    }
}
