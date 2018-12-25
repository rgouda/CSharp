using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Interview;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace Tests
{
    public class AdhocTest
    {
        private Adhoc adhoc;

        [SetUp]
        public void Setup()
        {
            adhoc = new Adhoc();
        }

        [Test]
        [TestCase("{}{}[](){[()]}")]
        [TestCase("[{([{([{()}])}])}]")]
        public void TestStringParenBalanced(string str)
        {
            var parens = new Dictionary<char, char> {
                { '}', '{' },
                { ')', '(' },
                { ']', '[' },
            };
            Assert.That(adhoc.IsStringBalanced(str, parens), Is.True);
        }

        [Test]
        [TestCase("[[[}}}")]
        public void TestStringParenUnbalanced(string str)
        {
            var parens = new Dictionary<char, char> {
                { '}', '{' },
                { ')', '(' },
                { ']', '[' },
            };
            Assert.That(adhoc.IsStringBalanced(str, parens), Is.False);
        }

        [Test]
        public void TestBubbleSort()
        {
            var numbers = new List<int> { 20, 10, -5, 40, 30 };
            var sorted = new List<int> { -5, 10, 20, 30, 40 };
            var results = this.adhoc.BubbleSort<int>(ref numbers, (x, y) => { return Math.Sign(y - x); });
            Assert.That(results.SequenceEqual(sorted), Is.True);
        }

        [Test]
        public void TestBubbleSort2()
        {
            var numbers = new List<int> { 100, 90, 20, 40, 50, -10, -4, -6, -9 };
            var sorted = new List<int> { -10, -9, -6, -4, 20, 40, 50, 90, 100 };
            var results = this.adhoc.BubbleSort<int>(ref numbers, (x, y) => { return Math.Sign(y - x); });
            Assert.That(results.SequenceEqual(sorted), Is.True);
        }

        [Test]
        public void MinimumCostTest()
        {
            var numbers = new List<int[]> {
                new int[] { 500, 300 },
                new int[] { 300, 100 },
                new int[] { 10, 100 }
            };
            var minCost = Adhoc.MinimumCost<int>(numbers, (a, b) => a + b );
            Assert.That(minCost == 410);
        }

        [Test]
        public void MinimumCostForNegativesTest()
        {
            var numbers = new List<int[]> {
                new int[] { -500, -300 },
                new int[] { -300, -100 },
                new int[] { -10, -100 }
            };
            var minCost = Adhoc.MinimumCost<int>(numbers, (a, b) => a + b);
            Assert.That(minCost == -900);
        }

        [Test]
        [TestCase("AAABBCCDDE", "E")]
        [TestCase("EDGE", "DG")]
        [TestCase("DCAACD", "")]
        [TestCase("EABCD", "ABCDE")]
        public void SupressAnyDuplicatesMethod1(string input, string output)
        {
            var str = input;
            var nonDuplicates =
                from s in str
                group s by s into g
                where g.Count() == 1
                orderby g.Key
                select g.Key;
            var nonDupStr = String.Join("", nonDuplicates);
            Assert.That(nonDupStr == output, Is.True);
        }

        [Test]
        [TestCase("AAABBCCDDE", "E")]
        [TestCase("EDGE", "DG")]
        [TestCase("DCAACD", "")]
        [TestCase("EABCD", "EABCD")]
        public void SupressAnyDuplicatesMethod2(string input, string output)
        {
            var dict = new Dictionary<char, int>();
            foreach(char c in input)
            {
                if (!dict.ContainsKey(c))
                    dict.Add(c, 0);
                dict[c]++;
            }
            var nonDupStr = "";
            foreach(var item in dict)
            {
                if (item.Value == 1)
                    nonDupStr += item.Key;
            }

            Assert.That(nonDupStr == output, Is.True);
        }

        [Test]
        public void PermutationCharsTest()
        {
            Assert.DoesNotThrow(() => { adhoc.Permute("abcd"); });
            var permutationArray = adhoc.Permute<char>("abcd".ToCharArray());
            Assert.That(permutationArray != null, Is.True);
            Assert.That(permutationArray.Count() == 24, Is.True);
            Assert.That(permutationArray.Any(a => a.SequenceEqual("abc")), Is.True);
        }

        [Test]
        public void PermutationNrsTest()
        {
            Assert.DoesNotThrow(() => { adhoc.Permute(Enumerable.Range(1, 4)); });
            var permutations = adhoc.Permute(Enumerable.Range(1, 4));
            Assert.That(permutations != null, Is.True);
            Assert.That(permutations.Count() == 24, Is.True);
            Assert.That(permutations.Any(a => a.SequenceEqual(new int[] { 4, 3, 2, 1 })));
            Assert.That(permutations.Any(a => a.SequenceEqual(new int[] { 1, 2, 3, 4 })));
        }

        [Test]
        public void PermutationsWordsTest()
        {
            Assert.DoesNotThrow(() => { adhoc.Permute(new string[] { "this", "is", "a", "word" }); });
            var permutations = adhoc.Permute(new string[] { "this", "is", "a", "word" });
            Assert.That(permutations != null, Is.True);
            Assert.That(permutations.Count() == 24, Is.True);
            Assert.That(permutations.Any(a => a.SequenceEqual( new string[] { "this", "is", "a", "word" })), Is.True);
            Assert.That(permutations.Any(a => a.SequenceEqual(new string[] { "word", "a", "is", "this" })), Is.True);
        }

        [Test]
        public void CombinationsAllSizesTest()
        {
            Assert.DoesNotThrow(() => adhoc.Combinations<int>(Enumerable.Range(1, 4)));
            var combinations = adhoc.Combinations(Enumerable.Range(1, 4));
            Assert.That(combinations != null, Is.True);
            Assert.That(combinations.Count() == 15, Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new int[] { 2, 3, 4 })), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new List<int> { 2, 3, 4 })), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new List<int> { 1, 4 })), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new List<int> { 1 })), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new List<int> { 1, 2, 3, 4 })), Is.True);
        }

        [Test]
        public void CombinationsREqualsTwoTest()
        {
            var combinations = adhoc.Combinations(Enumerable.Range(1, 4), 2);
            Assert.That(combinations != null, Is.True);
            Assert.That(combinations.Count() == 6, Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new int[] { 1, 2 })));
            Assert.That(combinations.Any(a => a.SequenceEqual(new int[] { 1, 3 })));
            Assert.That(combinations.Any(a => a.SequenceEqual(new int[] { 1, 4 })));
            Assert.That(combinations.Any(a => a.SequenceEqual(new int[] { 2, 3 })));
        }

        [Test]
        public void CombinationsOnStringTest()
        {
            var combinations = adhoc.Combinations("abcd");
            Assert.That(combinations != null, Is.True);
            Assert.That(combinations.Count() == 15, Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("a")), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("ab")), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("abc")), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("abcd")), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("b")), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual("bc")), Is.True);

            Assert.That(combinations.Any(a => a.SequenceEqual("ba")), Is.False);
        }

        [Test]
        public void CombinationsWithWords()
        {
            var combinations = adhoc.Combinations(new List<string> { "this", "is", "a", "word" });
            Assert.That(combinations != null, Is.True);
            Assert.That(combinations.Count() == 15, Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new string[] { "this", "is" })), Is.True);
            Assert.That(combinations.Any(a => a.SequenceEqual(new string[] { "this", "is", "a", "word" })));
        }

        [Test]
        public void TestConcurrent()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i < 10; ++i)
                {
                    bag.Add(i);
                    Thread.Sleep(200);
                }
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (i != 4)
                {
                    foreach (var item in bag)
                    {
                        Console.WriteLine(i + "-" + item);
                        Thread.Sleep(200);
                    }
                    i++;
                    Thread.Sleep(200);
                }

            });

            Task.WaitAll(t1, t2);
        }
    }
}
