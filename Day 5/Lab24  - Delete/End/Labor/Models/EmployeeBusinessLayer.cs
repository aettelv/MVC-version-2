using System.Collections.Generic;
using System.Linq;
using Labor.DataAccessLayer;

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
            if (u.UserName == "Admin" && u.Password == "Admin") return true;
            if (u.UserName == "Mari" && u.Password == "Mets")
                return true;
            return false;
        }
    }
}