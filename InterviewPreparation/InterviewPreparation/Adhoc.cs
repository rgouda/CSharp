using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

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

        public IEnumerable<List<T>> Permute<T>(IEnumerable<T> array)
        {
            var length = array.Count();
            var used = new bool[length];
            var sb = new List<T>();
            return doPermute<T>(array, sb, used, length, 0);
        }

        public IEnumerable<List<T>> Permute<T>(IEnumerable<T> array, int r)
        {
            var used = new bool[r];
            var sb = new List<T>();
            return doPermute<T>(array, sb, used, r, 0);
        }

        private IEnumerable<List<T>> doPermute<T>(IEnumerable<T> array, List<T> sb, bool[] used, int length, int level)
        {
            var permuteArray = new List<List<T>>();
            if (level == length)
            {
                permuteArray.Add(sb.ToList());
                return permuteArray;
            }

            for(int i=0; i<length; i++)
            {
                if (used[i]) continue;

                used[i] = true;
                sb.Add(array.ElementAt(i));
                
                var permute = doPermute(array, sb, used, length, level + 1);
                permuteArray = permuteArray.Concat(permute).ToList();
                used[i] = false;
                sb.RemoveAt(sb.Count()-1);
            }

            return permuteArray;
        }

        private int fact(int n)
        {
            if (n < 1)
                return 0;

            int f = 1;
            while (n > 0)
                f *= n--;

            return f;
        }

        public IEnumerable<List<T>> Combinations<T>(IEnumerable<T> arr)
        {
            return this.Combinations<T>(arr, -1);
        }

        public IEnumerable<List<T>> Combinations<T>(IEnumerable<T> arr, int r)
        {
            Stack<T> stack = new Stack<T>(arr);
            int capacity = fact(arr.Count());
            var combinations = new List<List<T>>(capacity);
            
            while (stack.Count() != 0)
            {
                var e = stack.Pop();
                var element = new List<T>(1) { e };
                foreach(var c in combinations.ToArray())
                {
                    if (r > 0 && r < c.Count)
                        continue;

                    var currentCombinaion = element.Concat(c);
                    combinations.Add(currentCombinaion.ToList());
                }

                combinations.Add(element);
            }

            return combinations.Where(c => r < 1 || c.Count == r).AsEnumerable();
        }
    }
    
}
