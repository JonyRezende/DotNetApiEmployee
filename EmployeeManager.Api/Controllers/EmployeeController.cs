using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeManager.Models;
using EmployeeManager.Models.Context;

namespace EmployeeManager.Api
{
    public class EmployeeController : ApiController
    {
        private enum _action {
            GetAll = 1,
            Get = 2,
            Create = 3,
            Delete = 4 
        }

        /// Returns List of Employee
        /// <param name="page">Actual Page</param>
        /// <param name="limit">Limit per Page</param>
        /// <usage>curl -H "Content-Type: application/javascript" "http://localhost:8000/api/employee?page=1&limit=10"</usage>
        /// <returns>Object Data (list of employees), Object Paging (total, page, limit, number of objects)</returns>
        public IHttpActionResult Get(int page = 1, int limit = 10)
        {
            return ManageEmployee((int)_action.GetAll, null, page, limit, null);
        }

        /// Return Employee
        /// <param name="id">id of employee</param>
        /// <usage>curl -H "Content-Type: application/javascript" "http://localhost:8000/api/employee/2"</usage>
        /// <returns>Object Employee</returns>
        public IHttpActionResult Get(int id)
        {
            return ManageEmployee((int)_action.Get, id, null, null, null);
        }

        /// Create Employee
        /// <param name="employee">Object Employee</param>
        /// <usage>curl -i -H "Accept: application/javascript" -X POST -d "Name=Jonathas Rezende&Email=jonyrezende@live.com&Department=Development" "http://localhost:8000/api/employee"</usage>
        /// <returns>Response 201 Created</returns>
        public IHttpActionResult Post(Employee employee)
        {
            return ManageEmployee((int)_action.Create, null, null, null, employee);
        }

        /// Delete Employee
        /// <param name="id">id of Employee</param>
        /// <usage>curl -H "Content-Type: application/javascript" "http://localhost:8000/api/employee/15/" -X DELETE</usage>
        /// <returns>Response 200</returns>
        public IHttpActionResult Delete(int id)
        {
            return ManageEmployee((int)_action.Delete, id, null, null, null);
        }

        /// <summary>
        /// Manage Employee Action on Database
        /// </summary>
        /// <param name="action">action (GetAll, Get, Create, Delete)</param>
        /// <param name="id">int id of employee</param>
        /// <param name="page">int actual page</param>
        /// <param name="limit">int page limit</param>
        /// <param name="employee">object employee</param>
        /// <returns>Depending of action, returns list of employees, object employee or status messages</returns>
        private IHttpActionResult ManageEmployee(int action, int? id, int? page, int? limit, Employee employee)
        {
            try
            {
                using (EmployeeRepository repo = new EmployeeRepository())
                {
                    switch (action)
                    {
                        case (int)_action.GetAll:
                            return Ok(repo.GetAll((int)page, (int)limit));
                        case (int)_action.Get:
                            return Ok(repo.Get((int)id));
                        case (int)_action.Create:
                            repo.Add(employee);
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Created, "Employee Created"));
                        case (int)_action.Delete:
                            repo.Delete((int)id);
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "Employee Deleted"));
                        default:
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred, try again"));
                    }
                }
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
        }
    }
}
