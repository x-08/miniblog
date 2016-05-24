using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace miniBlog.Controllers
{
    public class BaseController : Controller
    {

        public bool IsAdmin
        {
            get
            {
                if ( Session!=null && Session["IsAdmin"] != null && (bool) Session["IsAdmin"])
                    return true;
                return false;
            }
        }
        public BaseController()
        {
            // This code will run for all your controllers
            ViewBag.IsAdmin = IsAdmin;
        }


    }



}
