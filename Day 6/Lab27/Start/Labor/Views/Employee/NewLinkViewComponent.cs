using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labor.Views.Employee
{
    public class NewLinkViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.Session.GetString("SessionKeyName") == "Admin")
            {
                return View("AddNewLink");
            }
            else
            {
                return Content(String.Empty);
            }
        }
    }
}

