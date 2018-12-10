using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Interview;

namespace InterviewTest
{
    [TestFixture]
    public class RacersAndCheckpointTest
    {
        RacersAndCheckpoint racers;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            racers = new RacersAndCheckpoint();
        }

        [Test]
        public void TestObjectCreation()
        {
            Assert.That(racers != null, Is.True);
        }

        [Test]
        public void TestInitializeFirstCheckpoint()
        {
            var player = new RacersAndCheckpoint.Racer("P1", "Player 1", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P2", "Player 2", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P3", "Player 3", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P4", "Player 4", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            Assert.That(racers.Buckets.Count == 1, Is.True);
            Assert.That(racers.Buckets[RacersAndCheckpoint.Checkpoints.C1].Count == 4, Is.True);
            racers.PrintPlayers();
            racers.Reset();
        }

        [Test]
        public void TestInitializeSecondCheckpoint()
        {
            var player = new RacersAndCheckpoint.Racer("P1", "Player 1", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P2", "Player 2", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P3", "Player 3", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);
            player = new RacersAndCheckpoint.Racer("P4", "Player 4", "C1");
            racers.UpdatePlayer(player, player.Checkpoint);

            player = new RacersAndCheckpoint.Racer("P1", "Player 1", "C1");
            racers.UpdatePlayer(player, RacersAndCheckpoint.Checkpoints.C2);
            player = new RacersAndCheckpoint.Racer("P3", "Player 3", "C1");
            racers.UpdatePlayer(player, RacersAndCheckpoint.Checkpoints.C2);
            player = new RacersAndCheckpoint.Racer("P1", "Player 1", "C2");
            racers.UpdatePlayer(player, RacersAndCheckpoint.Checkpoints.C3);
            Assert.That(racers.Buckets.Count == 3, Is.True);
            Assert.That(racers.Buckets[RacersAndCheckpoint.Checkpoints.C1].Count == 2, Is.True);
            Assert.That(racers.Buckets[RacersAndCheckpoint.Checkpoints.C2].Count == 1, Is.True);
            Assert.That(racers.Buckets[RacersAndCheckpoint.Checkpoints.C3].Count == 1, Is.True);
            racers.PrintPlayers();
            racers.Reset();
        }
    }
}
