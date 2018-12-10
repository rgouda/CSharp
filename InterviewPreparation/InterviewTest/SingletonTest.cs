using NUnit.Framework;
using InterviewPreparation;

namespace Tests
{
    public class SingletonClassTest
    {
        Singleton singleton;

        [SetUp]
        public void Setup()
        {
            singleton = Singleton.CreateSingleton();
        }

        [Test]
        public void TestInstanceCreated()
        {
            Assert.That(this.singleton != null, Is.True);
            Assert.That(this.singleton.Value == 100, Is.True);
        }

        [Test]
        public void TestAnotherInstancePointsToSameObject()
        {
            var anotherObject = Singleton.CreateSingleton();
            Assert.That(this.singleton == anotherObject, Is.True);
        }

        [Test]
        public void TestValueIsConsistentBetweenObjects()
        {
            var anotherObject = Singleton.CreateSingleton();
            anotherObject.Value = 200;
            Assert.That(this.singleton == anotherObject, Is.True);
            Assert.That(this.singleton.Value == anotherObject.Value, Is.True);
        }
    }
}