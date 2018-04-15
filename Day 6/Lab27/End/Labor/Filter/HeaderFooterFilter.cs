using Labor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Labor.Filter
{
    public class HeaderFooterFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null)
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)
                {
                    bvm.UserName = filterContext.HttpContext.Session.GetString("SessionKeyName");
                    bvm.FooterData = new FooterViewModel();
                    bvm.FooterData.CompanyName = "TTÜ";
                    bvm.FooterData.Year = DateTime.Now.Year.ToString();
                }
            }
        }

    }
}

