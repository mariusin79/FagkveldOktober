namespace SharedContracts
{
    public class BookKey
    {
        protected bool Equals(BookKey other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BookKey)obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public int Value { get; set; }

        public static bool operator ==(BookKey b1, BookKey b2)
        {
            return Equals(b1, b2);
        }

        public static bool operator !=(BookKey b1, BookKey b2)
        {
            return !(b1 == b2);
        }
    }
}