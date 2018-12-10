using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Interview
{
    public class Adhoc
    {
        /// <summary>
        /// Returns a list of sorted T types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public ref List<T> BubbleSort<T>(ref List<T> list, Func<T, T, int> compare)
        {
            if (list == null || list.Count() == 0)
                return ref list;

            T tmp;
            bool swappedOnce;
            for(int i=0; i < list.Count; i++)
            {
                swappedOnce = false;
                for(int j=0; j < list.Count - i - 1; j++)
                {
                    if(compare(list[j], list[j+1]) < 0)
                    {
                        swappedOnce = true;
                        tmp = list[j];
                        list[j] = list[j+1];
                        list[j+1] = tmp;
                    }
                }

                if (!swappedOnce)
                    break;
            }
            return ref list;
        }

        /// <summary>
        /// Checks for balanced parenthses and returns true if balanced otherwise returns false.
        /// </summary>
        /// <param name="str">input string to check if the string has balanced parentheses</param>
        /// <param name="parens">The key is the right token. E.g. '}' => '{', ')' => '('</param>
        /// <returns></returns>
        public bool IsStringBalanced(string str, IDictionary<char, char> parens)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            var left = parens.Values;
            var stack = new Stack<char>();
            foreach(var c in str)
            {
                if (left.Contains(c))
                {
                    stack.Push(c);
                }
                else if (parens.Keys.Contains(c))
                {
                    char pop;
                    try
                    {
                        pop = stack.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    if (pop != parens[c])
                        return false;
                }
            }
            if (stack.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A custom min function to return the minimum element in an array of type T and the 
        /// indexer it is found at.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static (T, int) Minimum<T>(T[] array) where T : IComparable
        {
            if (array == null)
                throw new Exception("Empty array");

            T min = default(T);
            int minIndex = -1;
            bool first = true;
            for(int i=0; i<array.Length; i++)
            {
                if (first)
                {
                    min = array[i];
                    first = false;
                }

                if (min.CompareTo(array[i]) > 0)
                {
                    minIndex = i;
                    min = array[i];
                }
            }

            return (min, minIndex);
        }

        public static T MinimumCost<T>(List<T[]> list, Func<T, T, T> adder) where T : IComparable
        {
            T minCost = default(T);
            foreach(var array in list)
            {
                (var min, var i) = Minimum(array);
                minCost = adder(min, minCost);
            }
            return minCost;
        }

        //public static T CostOptimization<T>(List<T[]> list)
        //{

        //}
    }
}
