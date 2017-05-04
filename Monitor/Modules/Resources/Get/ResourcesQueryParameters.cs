namespace Monitor.Modules.Resources.Get
{
    class ResourcesQueryParameters
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
