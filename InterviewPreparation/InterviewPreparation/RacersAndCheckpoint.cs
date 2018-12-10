using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public class RacersAndCheckpoint
    {
        public enum Checkpoints { C5 = 0, C4 = 1, C3 = 2, C2 = 3, C1 = 4 }

        public sealed class Racer : IEquatable<Racer>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public Checkpoints Checkpoint { get; set; }

            public Racer(string id, string name, string ckp)
            {
                this.Id = id;
                this.Name = name;
                this.Checkpoint = (Checkpoints) Enum.Parse(typeof(Checkpoints), ckp);
            }

            public bool Equals(Racer other)
            {
                if (Object.ReferenceEquals(other, null))
                    return false;
                if (Object.ReferenceEquals(other, this))
                    return true;
                return this.Id == other.Id;
            }
        }

        public IDictionary<Checkpoints, List<Racer>> Buckets;

        public RacersAndCheckpoint()
        {
            Buckets = new Dictionary<Checkpoints, List<Racer>>();
        }

        public void UpdatePlayer(Racer racer, Checkpoints ckp)
        {
            if (!Buckets.ContainsKey(ckp))
            {
                Buckets.Add(ckp, new List<Racer>());
            }

            var status = Buckets[racer.Checkpoint].Remove(racer);
            if (!status && (int)ckp < 4)
            {
                throw new Exception($"Player {racer.Id} skipping C1");
            }
            racer.Checkpoint = ckp;
            Buckets[ckp].Add(racer);
        }

        public void Reset()
        {
            foreach(var list in Buckets.Values)
            {
                list.RemoveAll((x) => true);
            }
            Buckets.Clear();
        }

        public void PrintPlayers()
        {
            foreach(Checkpoints ckp in Enum.GetValues(typeof(Checkpoints)))
            {
                if (!Buckets.ContainsKey(ckp))
                    continue;

                foreach(var player in Buckets[ckp])
                {
                    Console.WriteLine(player.Checkpoint.ToString(), player.Id);
                }
            }
        }
    }
}
