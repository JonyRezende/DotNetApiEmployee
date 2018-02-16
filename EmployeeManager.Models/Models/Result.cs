using System.Collections.Generic;
using EmployeeManager.Models.Context;

namespace EmployeeManager.Models
{
    /// <summary>
    /// class to return a result for list Employee
    /// </summary>
    public class ResultEmployee
    {
        public List<Employee> Data { get; set; }
        public Paging Paging { get; set; }
    }
}
