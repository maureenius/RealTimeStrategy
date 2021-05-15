using System;

# nullable enable

namespace Model.Util
{
    // Value Object
    public class Buff : IEquatable<Buff>
    {
        public readonly float rate;
        public readonly string reason;

        public Buff(float rate, string reason)
        {
            this.rate = rate;
            this.reason = reason;
        }

        public bool Equals(Buff other)
        {
            return reason == other.reason;
        }
    }
}