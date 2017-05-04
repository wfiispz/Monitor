namespace Monitor.Sensors
{
    /// <summary>
    /// DTO containing details of paginated data
    /// </summary>
    public class PageDetails
    {
        public int Size { get; set; }
        public int Number { get; set; }
        public int TotalCount { get; set; }
    }
}