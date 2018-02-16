namespace EmployeeManager.Models
{
    /// <summary>
    /// Object to paging results
    /// </summary>
    public class Paging
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Returned { get; set; }
    }
}
