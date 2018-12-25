using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Interview;

namespace InterviewTest
{
    [TestFixture]
    class CostOptimizationTest
    {
        CostOptimization costOptimization;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            costOptimization = new CostOptimization();
        }

        [Test]
        public void TestInstanceCreated()
        {
            Assert.That(costOptimization != null, Is.True);
        }

        [Test]
        public void TestInitialize()
        {
            costOptimization.Clear();
            costOptimization.InsertCandidateCosts(500, 700);
            costOptimization.InsertCandidateCosts(200, 600);
            costOptimization.InsertCandidateCosts(400, 500);
            costOptimization.InsertCandidateCosts(600, 200);
            Assert.That(costOptimization.CandidateCount() == 4, Is.True);
        }

        [Test]
        public void TestCalculateCost()
        {
            costOptimization.Clear();
            costOptimization.InsertCandidateCosts(500, 700);
            costOptimization.InsertCandidateCosts(200, 600);
            costOptimization.InsertCandidateCosts(400, 500);
            costOptimization.InsertCandidateCosts(600, 200);
            /* 
 SF NY
A 500 700
B 200 600
C 400 500
D 600 200
Output : 1400 (A:500 + B:200 + C:500 +D: 200)  
             */
            Assert.That(costOptimization.CalculateCost() == 1400, Is.True);
        }

        [Test]
        public void TestThrowsOnOddElement()
        {
            costOptimization.Clear();
            costOptimization.InsertCandidateCosts(500, 700);
            costOptimization.InsertCandidateCosts(200, 600);
            costOptimization.InsertCandidateCosts(400, 500);
            costOptimization.InsertCandidateCosts(600, 200);
            costOptimization.InsertCandidateCosts(600, 200);

            Assert.Throws<Exception>(() => costOptimization.CalculateCost());
            Assert.That(() => { costOptimization.CalculateCost(); }, Throws.Exception);
        }

    }
}
