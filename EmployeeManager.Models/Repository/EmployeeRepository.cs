using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManager.Models.Context;
using EmployeeManager.Library;

namespace EmployeeManager.Models
{
    public class EmployeeRepository : IDisposable
    {
        private EmployeeManagerEntities _context;

        public EmployeeRepository()
        {
            _context = new EmployeeManagerEntities();
        }

        /// <summary>
        /// Return All Objects Paginated
        /// </summary>
        /// <param name="page">actual page</param>
        /// <param name="limit">page limit</param>
        /// <returns>Return object "Result" with list of employees and Paging info</returns>
        public ResultEmployee GetAll(int page, int limit)
        {
            int total = _context.Employee.Count();
            int offset = page == 0 ? page = 1 : (page - 1) * limit;

            List<Employee> employees = _context.Employee.OrderBy(c => c.Id).Skip(offset).Take(limit).ToList();

            if (employees.Count == 0)
                throw new Exception("No records found");

            return new ResultEmployee
            {
                Data = employees,
                Paging = new Paging
                {
                    Total = total,
                    Limit = limit,
                    Page = page,
                    Returned = employees.Count
                }
            };
        }

        /// <summary>
        /// Get object Employee
        /// </summary>
        /// <param name="id">id of Employee</param>
        /// <returns>Object Employee</returns>
        public Employee Get(int id)
        {
            if(_context.Employee.FirstOrDefault(e => e.Id == id) == null)
                throw new Exception("Employee not found.");

            return _context.Employee.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Add object Employee
        /// </summary>
        /// <param name="employee">Object Employee</param>
        public void Add(Employee employee)
        {
            CheckFields(employee);

            _context.Employee.Add(employee);
            SaveChanges();
        }

        /// <summary>
        /// Delete a Employee Object
        /// </summary>
        /// <param name="id">id of employee</param>
        public void Delete(int id)
        {
            Employee employee = Get(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            _context.Employee.Remove(employee);
            SaveChanges();
        }

        /// <summary>
        /// Dispose _context
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Check fields of object employee
        /// </summary>
        /// <param name="employee">Object Employee</param>
        private void CheckFields(Employee employee)
        {
            if (!StringLib.CheckNull(employee.Name))
                throw new Exception("Name required");

            if (!StringLib.CheckNull(employee.Email))
                throw new Exception("Email required");

            if (!StringLib.CheckNull(employee.Department))
                throw new Exception("Department required");

            if (!StringLib.CheckLenght(employee.Name, 100))
                throw new Exception("Maximum Name length: 100 characters");

            if (!StringLib.CheckLenght(employee.Email, 100))
                throw new Exception("Maximum Email length: 100 characters");

            if (!StringLib.CheckLenght(employee.Department, 100))
                throw new Exception("Maximum Department length: 100 characters");

            if (!EmailLib.IsValidEmail(employee.Email))
                throw new Exception("Invalid Email");

            if (_context.Employee.FirstOrDefault(e => e.Email == employee.Email) != null)
                throw new Exception("Email already exists");
        }

        /// <summary>
        /// Save _context Changes
        /// </summary>
        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}