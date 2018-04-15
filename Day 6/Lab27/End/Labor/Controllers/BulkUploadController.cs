using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Labor.Filter;
using Labor.ViewModels;
using Labor.Models;
using System.IO;
using Labor.DataAccessLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.FileProviders;

namespace Labor.Controllers
{
    public class BulkUploadController : Controller
    {
        private readonly Sales db;
        public BulkUploadController(Sales database) { db = database; }

        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileUploadViewModel model)
        {
            if (model == null ||
                model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Content("file not selected");
            var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "wwwroot",
                   model.FileToUpload.GetFilename());
            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    model.FileToUpload.CopyToAsync(stream);
            //}


            List<Employee> employees = GetEmployees(model);
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees, db);

            return RedirectToAction("Index", "Employee");

            //var path = Path.Combine(
            //            Directory.GetCurrentDirectory(), "wwwroot",
            //            model.FileToUpload.GetFilename());
            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    await model.FileToUpload.CopyToAsync(stream);
            //    StreamReader reader = new StreamReader(stream);
            //    List<Employee> employees = new List<Employee>();

            //    reader.ReadLine();

            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');
            //        Employee e = new Employee();
            //        e.FirstName = values[0];
            //        e.LastName = values[1];
            //        e.Salary = int.Parse(values[2]);
            //        employees.Add(e);

            //    }
            //    return RedirectToAction("Index", "Employee");
        }

        //[AdminFilter]
        //public ActionResult Upload(FileUploadViewModel model, string upload)
        //{
        //    List<Employee> employees = GetEmployees(model);
        //    EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
        //    bal.UploadEmployees(employees, db);

        //    return RedirectToAction("Index", "Employee");
        //}

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    model.FileToUpload.GetFilename());
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.FileToUpload.CopyToAsync(stream);
                StreamReader reader = new StreamReader(stream);
                List<Employee> employees = new List<Employee>();

                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Employee e = new Employee();
                    e.FirstName = values[0];
                    e.LastName = values[1];
                    e.Salary = int.Parse(values[2]);
                    employees.Add(e);
                }
                return employees;
            }
        }
    }
}
