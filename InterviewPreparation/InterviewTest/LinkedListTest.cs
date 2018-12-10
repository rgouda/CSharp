using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Interview;

namespace InterviewTest
{
    [TestFixture]
    class LinkedListTest
    {
        Interview.LinkedList<Int32> intLinkedList;

        [SetUp]
        public void Setup()
        {
            intLinkedList = new Interview.LinkedList<Int32>();
            intLinkedList.Append(10);
            intLinkedList.Append(20);
            intLinkedList.Append(30);
        }

        [Test]
        public void IntLinkedListInstanceTest()
        {
            Assert.That(intLinkedList != null, Is.True);
        }

        [Test]
        public void IntLinkedListCountTest()
        {
            Assert.That(intLinkedList.Count() == 3, Is.True);
        }

        [Test]
        public void IntLinkedListSumTest()
        {
            Assert.That(intLinkedList.SumTotal() == 60, Is.True);
        }

        [Test]
        public void SumLinkedListTest()
        {
            var ll1 = new Interview.LinkedList<Int32>();
            var ll2 = new Interview.LinkedList<Int32>();
            ll1.Append(10); ll1.Append(20); ll1.Append(30);
            ll2.Append(10); ll2.Append(20); ll2.Append(30);
            var ll3 = ll1.SumUp(ll2);
            Assert.That(ll3.Count() == 3, Is.True);
            Assert.That(ll3.Head.Value == 20, Is.True);
            Assert.That(ll3.Head.Next.Value == 40, Is.True);
        }

        [Test]
        public void SumLinkedListUnevenTest()
        {
            var ll1 = new Interview.LinkedList<Int32>();
            var ll2 = new Interview.LinkedList<Int32>();
            ll1.Append(10); ll1.Append(20); ll1.Append(30);
            ll2.Append(10); ll2.Append(20); ll2.Append(31); ll2.Append(40);
            var ll3 = ll1.SumUp(ll2);
            Assert.That(ll3.Count() == 4, Is.True);
            Assert.That(ll3.Head.Value == 20, Is.True);
            Assert.That(ll3.Head.Next.Value == 40, Is.True);

            ll1.Append(25); ll1.Append(25);
            var ll4 = ll1.SumUp(ll2);
            Assert.That(ll1.Count() == 5, Is.True);
            Assert.That(ll2.Count() == 4, Is.True);
            Assert.That(ll4.Count() == 5, Is.True);
            Assert.That(ll4.SumTotal() == 211, Is.True);
        }
    }
}
