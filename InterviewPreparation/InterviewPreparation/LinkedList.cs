using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public interface IAdd<T>
    {
        T Sum(T left, T right);
    }

    public class Node<T> where T : IEquatable<T>
        //, IAdd<T>
    {
        public T Value;
        public Node<T> Next = null;

        public Node(T value, Node<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        public Node(Node<T> other)
        {
            this.Value = other.Value;
            this.Next = other.Next;
        }

        public bool Equals(Node<T> other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return this.Value.Equals(other.Value);
        }

        public void Clear()
        {
            this.Value = default(T);
            this.Next = null;
        }

        public static Node<T> operator+(Node<T> left, Node<T> right)
        {
            var tmpResult = (dynamic)left.Value + (dynamic)right.Value;
            return new Node<T>(tmpResult, left.Next);
            //return new Node<T>(left.Value.Sum(left.Value, right.Value), left.Next);
        }

        public override string ToString()
        {
            return $"Value={this.Value}";
        }
    }

    public class LinkedList<T>
        where T : IEquatable<T>  //, IAdd<T>
    {
        Node<T> head;
        Node<T> tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public Node<T> Head
        {
            get
            {
                return head;
            }
            private set
            {
            }
        }

        public Node<T> Append(T value)
        {
            return Append(value, null);
        }

        public Node<T> Append(T value, Node<T> next) 
        {
            var node = new Node<T>(value, next);
            if (head == null)
            {
                head = node;
                tail = node;
            }
            else if (next != null)
            {
                var current = head;
                while(current.Next != null && current != next)
                {
                    current = current.Next;
                }
                if (current != next)
                    throw new Exception($"Next {next} not found in list.");

                current.Next = node;
            }
            else
            {
                tail.Next = node;
                node.Next = null;
            }

            tail = node;
            return node;
        }

        public Node<T> Append(Node<T> node)
        {
            return Append(node.Value, node.Next);
        }

        public int Count()
        {
            int count = 0;
            var current = head;
            while (current != null)
            {
                current = current.Next;
                ++count;
            }
            return count;
        }

        public T SumTotal()
        {
            var sum = new Node<T>(default(T), null);
            var current = head;
            while(current != null)
            {
                sum = sum + current;
                current = current.Next;
            }
            return sum.Value;
        }

        public void Clear()
        {
            while (head != null)
            {
                head.Clear();
                head = head.Next;
            }
        }

        /// <summary>
        /// Adds elements of two linked lists.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Resultant new linked list</returns>
        public LinkedList<T> SumUp(LinkedList<T> other)
        {
            if (other == null)
                return this;

            var current = head;
            var otherHead = other.head;
            var result = new LinkedList<T>();
            Node<T> left;
            while (current != null)
            {
                if (otherHead != null)
                {
                    left = current + otherHead;
                    otherHead = otherHead.Next;
                }
                else
                {
                    left = current;
                }
                result.Append(left.Value, null);
                current = current.Next;
            }

            while(otherHead != null)
            {
                result.Append(otherHead.Value, null);
                otherHead = otherHead.Next;
            }
            
            return result;
        }
    }
}
