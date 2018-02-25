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
        private class TreeNode<T>
        {
            public TreeNode<T> LeftChild { get; set; }

            public TreeNode<T> RightChild { get; set; }

            public T NodeValue { get; set; }

            public TreeNode(T data)
            {
                NodeValue = data;
            }
        }

        private TreeNode<T> root;

        private int changesCount;

        public IComparer<T> Comparer { get; private set; }

        public Tree(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        private void OnChange()
        {
            changesCount++;
        }

        public void Insert(T data)
        {
            OnChange();

            if (root == null)
            {
                root = new TreeNode<T>(data);
                return;
            }

            var currentNode = root;

            while (true)
            {
                if (Comparer.Compare(data, currentNode.NodeValue) < 0)
                {
                    if (currentNode.LeftChild != null)
                    {
                        currentNode = currentNode.LeftChild;
                    }
                    else
                    {
                        currentNode.LeftChild = new TreeNode<T>(data);
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
                        currentNode.RightChild = new TreeNode<T>(data);
                        return;
                    }
                }
            }
        }

        private TreeNode<T> FindNode(T data, out TreeNode<T> parent)
        {
            if (root == null)
            {
                parent = null;
                return null;
            }

            var current = root;
            TreeNode<T> parentNode = null;
            while (true)
            {
                var compareResult = Comparer.Compare(data, current.NodeValue);
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

        public void Remove(T data)
        {
            TreeNode<T> parent;
            var node = FindNode(data, out parent);

            if (node == null)
            {
                return;
            }

            OnChange();

            if (parent == null)
            {
                root = null;
                return;
            }

            if (node.LeftChild == null && node.RightChild == null)
            {
                if (parent.LeftChild == node)
                {
                    parent.LeftChild = null;
                    return;
                }

                if (parent.RightChild == node)
                {
                    parent.RightChild = null;
                    return;
                }
            }
            else if (node.RightChild == null)
            {
                if (parent.LeftChild == node)
                {
                    parent.LeftChild = node.LeftChild;
                    return;
                }

                if (parent.RightChild == node)
                {
                    parent.RightChild = node.LeftChild; ;
                    return;
                }
            }
            else if (node.LeftChild == null)
            {
                if (parent.LeftChild == node)
                {
                    parent.LeftChild = node.RightChild;
                    return;
                }

                if (parent.RightChild == node)
                {
                    parent.RightChild = node.RightChild; ;
                    return;
                }
            }
            else
            {
                var left = node.RightChild;
                TreeNode<T> parentOfLeft = null;

                while (left.LeftChild != null)
                {
                    parentOfLeft = left;
                    left = left.LeftChild;
                }

                if (parentOfLeft != null)
                {
                    parentOfLeft.LeftChild = null;
                }

                if (parent.LeftChild == node)
                {
                    parent.LeftChild = left;
                    return;
                }

                if (parent.RightChild == node)
                {
                    parent.RightChild = left;
                    return;
                }
            }

        }

        public IEnumerable<T> Broadways()
        {
            if (root == null)
            {
                yield break;
            }

            var changes = changesCount;
            var queue = new Queue<TreeNode<T>>();

            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();

                yield return next.NodeValue;
                if (changes != changesCount)
                {
                    throw new InvalidOperationException("Ошибка: нельзя изменять дерево во время работы итератора");
                }

                //олордлдлодл


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

        private IEnumerable<T> Visit(TreeNode<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            var changes = changesCount;

            yield return node.NodeValue;
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
            var stack = new Stack<TreeNode<T>>();

            stack.Push(root);
            while (stack.Count > 0)
            {
                var next = stack.Pop();

                yield return next.NodeValue;
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

        public int GetCount()
        {
            var count = 0;

            foreach (var node in Broadways())
            {
                count++;
            }

            return count;
        }

    }
}
