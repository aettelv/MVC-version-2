using System.Collections.Generic;
using Labor.DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Labor.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees(Sales db)
        {
            return db.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e, Sales db)
        {
            db.Employees.Add(e);
            db.SaveChanges();
            return e;
        }

        public bool IsValidUser(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return true;
            }
            if (u.UserName == "Mari" && u.Password == "Mets")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
