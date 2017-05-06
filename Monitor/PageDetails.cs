namespace Monitor
{
    /// <summary>
    /// DTO containing details of paginated data
    /// </summary>
    public class PageDetails
    {
        public int Size { get; set; }
        public int Number { get; set; }
        public int TotalCount { get; set; }

        protected bool Equals(PageDetails other)
        {
            return Size == other.Size && Number == other.Number && TotalCount == other.TotalCount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PageDetails) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Size;
                hashCode = (hashCode * 397) ^ Number;
                hashCode = (hashCode * 397) ^ TotalCount;
                return hashCode;
            }
        }
    }
}