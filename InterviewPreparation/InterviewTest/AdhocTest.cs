using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Interview;

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
    }
}
