using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        // Get : /api/Employees/
        public IEnumerable<empTable> Get()
        {
            using(EmployeeDBEntities dbcontext = new EmployeeDBEntities())
            {
                return dbcontext.empTables.ToList();
            }
        }
        //Get : /api/Employees/id
        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities dbcontext = new EmployeeDBEntities())
            {
                var emp= dbcontext.empTables.FirstOrDefault(e => e.Id == id);
                if(emp == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id " + id + " not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
            }
        }
        //Post : /api/Employee/id
        public HttpResponseMessage Post(empTable emp)
        {
            using (EmployeeDBEntities dbconext = new EmployeeDBEntities())
            {
                if (emp != null)
                {
                    dbconext.empTables.Add(emp);
                    dbconext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please provide the required information");
                }
            }
        }
        //Put : /api/Employee/id
        public HttpResponseMessage Put(int id, empTable emp_UI)
        {
            using(EmployeeDBEntities dbconext = new EmployeeDBEntities())
            {
                var emp = dbconext.empTables.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = emp_UI.FirstName;
                    emp.LastName = emp_UI.LastName;
                    emp.Email = emp_UI.Email;
                    emp.Gender = emp_UI.Gender;
                    emp.City = emp_UI.City;
                    dbconext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id " + id +" not found in the database");
                }

            }
            
        }

        // Delete : /api/Employee/id
        public HttpResponseMessage Delete(int id)
        {
            using (EmployeeDBEntities dbconext = new EmployeeDBEntities())
            {
                var emp = dbconext.empTables.FirstOrDefault(e => e.Id == id);
                if(emp != null)
                {
                    dbconext.empTables.Remove(emp);
                    dbconext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id " + id + " not found in the database");
                }
            }
        }
    }
}
