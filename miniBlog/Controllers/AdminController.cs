using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miniBlog.Models;

namespace miniBlog.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        private blogEntities _modelEntities = new blogEntities();

        public ActionResult LoginVerify(string name, String password)
        {
            if (_modelEntities.Admins.Any(x => x.Name == name))
            {
                var admin = from user in _modelEntities.Admins.Where(x => x.Name == name)
                    select user;

                var selectedUser = admin.FirstOrDefault();
                if (selectedUser.Password == password)
                {
                     Session["IsAdmin"] = true;
                }
                




            }
            return RedirectToAction("Index", "Post");

        }

        public ActionResult Login()
        {
            if (Session["IsAdmin"] != null && (bool) Session["IsAdmin"] == true)
            {
                return RedirectToAction("Logout");
            }
            else
            {
                Session["IsAdmin"] = null;
                return View();
                
            }
            
        }


        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            return RedirectToAction("Index", "Post");

        }

    }
}
