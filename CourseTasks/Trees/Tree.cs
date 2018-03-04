using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class Tree<T>
    {
        private class Node
        {
            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }

            public T Value { get; set; }

            public Node(T data)
            {
                Value = data;
            }
        }

        private class DefaultComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }

                if (y == null)
                {
                    return 1;
                }

                return ((IComparable<T>)x).CompareTo(y);
            }
        }

        private Node root;

        private int changesCount;

        public IComparer<T> Comparer { get; private set; }

        public Tree(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public Tree()
        {
            var type = typeof(T);
            var icomparable = typeof(IComparable<T>);
            var interfaces = type.GetInterfaces();

            if (interfaces.Contains(icomparable))
            {
                Comparer = new DefaultComparer();
            }
            else
            {
                throw new InvalidCastException($"Класс {type.Name} не реализует интерфейс IComparable<{type.Name}>. " +
                    "Исполузуйте перегрузку конструктора.");
            }
        }

        public int Count { get; private set; }

        private void OnChange()
        {
            changesCount++;
        }

        public void Insert(T data)
        {
            OnChange();
            Count++;

            if (root == null)
            {
                root = new Node(data);
                return;
            }

            var currentNode = root;

            while (true)
            {
                if (Comparer.Compare(data, currentNode.Value) < 0)
                {
                    if (currentNode.LeftChild != null)
                    {
                        currentNode = currentNode.LeftChild;
                    }
                    else
                    {
                        currentNode.LeftChild = new Node(data);
                        return;
                    }
                }
                else
                {
                    if (currentNode.RightChild != null)
                    {
                        currentNode = currentNode.RightChild;
                    }
                    else
                    {
                        currentNode.RightChild = new Node(data);
                        return;
                    }
                }
            }
        }

        private Node FindNode(T data, out Node parent)
        {
            if (root == null)
            {
                parent = null;
                return null;
            }

            var current = root;
            Node parentNode = null;
            while (true)
            {
                var compareResult = Comparer.Compare(data, current.Value);
                if (compareResult == 0)
                {
                    parent = parentNode;
                    return current;
                }
                else if (compareResult < 0)
                {
                    if (current.LeftChild != null)
                    {
                        parentNode = current;
                        current = current.LeftChild;
                    }
                    else
                    {
                        parent = parentNode;
                        return null;
                    }
                }
                else
                {
                    if (current.RightChild != null)
                    {
                        parentNode = current;
                        current = current.RightChild;
                    }
                    else
                    {
                        parent = parentNode;
                        return null;
                    }
                }
            }
        }

        private static void ChangeChild(Node parent, Node node, Node newNode)
        {
            if (parent == null)
            {
                return;
            }

            if (parent.LeftChild != node && parent.RightChild != node)
            {
                return;
            }
            else if (parent.LeftChild == node)
            {
                parent.LeftChild = newNode;
                return;
            }

            parent.RightChild = newNode;
        }

        public bool Remove(T data)
        {
            Node parent;
            var node = FindNode(data, out parent);

            if (node == null)
            {
                return false;
            }

            OnChange();
            Count--;

            if (node.LeftChild == null && node.RightChild == null)
            {
                ChangeChild(parent, node, null);

                if (root == node)
                {
                    root = null;
                }
            }
            else if (node.RightChild == null)
            {
                ChangeChild(parent, node, node.LeftChild);

                if (root == node)
                {
                    root = node.LeftChild;
                }
            }
            else if (node.LeftChild == null)
            {
                ChangeChild(parent, node, node.RightChild);

                if (root == node)
                {
                    root = node.RightChild;
                }
            }
            else
            {
                var left = node.RightChild;
                Node parentOfLeft = null;

                while (left.LeftChild != null)
                {
                    parentOfLeft = left;
                    left = left.LeftChild;
                }

                if (parentOfLeft != null)
                {
                    parentOfLeft.LeftChild = left.RightChild;
                }

                ChangeChild(parent, node, left);

                left.LeftChild = node.LeftChild;

                if (node.RightChild != left)
                {
                    left.RightChild = node.RightChild;
                }

                if (root == node)
                {
                    root = left;
                }
            }

            return true;
        }

        public IEnumerable<T> Broadways()
        {
            if (root == null)
            {
                yield break;
            }

            var changes = changesCount;
            var queue = new Queue<Node>();

            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();

                yield return next.Value;
                if (changes != changesCount)
                {
                    throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
                }

                if (next.LeftChild != null)
                {
                    queue.Enqueue(next.LeftChild);
                }

                if (next.RightChild != null)
                {
                    queue.Enqueue(next.RightChild);
                }
            }

        }

        public IEnumerable<T> RecursiveScan()
        {
            return Visit(root);
        }

        private IEnumerable<T> Visit(Node node)
        {
            if (node == null)
            {
                yield break;
            }

            var changes = changesCount;

            yield return node.Value;
            if (changes != changesCount)
            {
                throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
            }

            foreach (var n in Visit(node.LeftChild))
            {
                yield return n;
                if (changes != changesCount)
                {
                    throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
                }
            }

            foreach (var n in Visit(node.RightChild))
            {
                yield return n;
                if (changes != changesCount)
                {
                    throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
                }
            }


        }

        public IEnumerable<T> NotRecursiveScan()
        {
            if (root == null)
            {
                yield break;
            }

            var changes = changesCount;
            var stack = new Stack<Node>();

            stack.Push(root);
            while (stack.Count > 0)
            {
                var next = stack.Pop();

                yield return next.Value;
                if (changes != changesCount)
                {
                    throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
                }

                if (next.RightChild != null)
                {
                    stack.Push(next.RightChild);
                }

                if (next.LeftChild != null)
                {
                    stack.Push(next.LeftChild);
                }


            }
        }
    }
}
