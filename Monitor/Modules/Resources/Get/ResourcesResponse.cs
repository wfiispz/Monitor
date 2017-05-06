namespace Monitor.Modules.Resources.Get
{
    public class ResourcesResponse
    {
        public Resource[] Resources { get; set; }
        public PageDetails Page { get; set; }

        protected bool Equals(ResourcesResponse other)
        {
            return Equals(Resources, other.Resources) && Equals(Page, other.Page);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ResourcesResponse) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Resources != null ? Resources.GetHashCode() : 0) * 397) ^ (Page != null ? Page.GetHashCode() : 0);
            }
        }
    }
}