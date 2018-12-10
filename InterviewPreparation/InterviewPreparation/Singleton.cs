using System;

namespace InterviewPreparation
{
    public class Singleton : IEquatable<Singleton>
    {
        private static Singleton instance;
        public int Value { get; set; }

        private Singleton()
        {
            this.Value = 100;
        }

        public static Singleton CreateSingleton()
        {
            if (Singleton.instance == null)
                instance = new Singleton();

            return instance;
        }

        public bool Equals(Singleton other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;

            return this.Value == other.Value;
        }
    }
}
