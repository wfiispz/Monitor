namespace Monitor.Api.Resources.Query
{
    public class ResourcesQueryParameters
    {
        public ResourcesQueryParameters()
        {
            PageSize = 100;
            Page = 1;
        }

        public string Name { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
