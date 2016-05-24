using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using miniBlog.Models;
using Microsoft.Ajax.Utilities;

namespace miniBlog.Controllers
{
 
    public class PostController : BaseController
    {
        //
        // GET: /Post/
        private blogEntities _modelEntities = new blogEntities();
        private const int postPerPage = 4;

        public ActionResult Index(int? id)
        {
            int p = id ?? 0;
            IEnumerable<Post> posts = (from post in _modelEntities.Posts
                where post.Time < DateTime.Now
                orderby post.Time descending
                select post).Skip(p*postPerPage).Take(postPerPage);
            ViewBag.isPreviousLinkAvailable = p > 0;
            ViewBag.isNextLinkAvailable = posts.Count() > postPerPage - 1;
            ViewBag.pageNumber = p;
            ViewBag.isAdmin = IsAdmin;
            var indexModelObject = new IndexViewModel {Posts = posts};

            return View(indexModelObject);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdatePost(int? id, string title, DateTime? time, string body)
        {
            if (!IsAdmin)
            {
                return RedirectToAction("Index");
            }

            Post post = GetPost(id);
            post.Title = title;
            post.Time = DateTime.Now;
            post.Body = body;
            ViewBag.isAdmin = IsAdmin;

            if (!id.HasValue)
            {
                _modelEntities.Posts.Add(post);

            }
            _modelEntities.SaveChanges();
            return View("details", post);
        }

        [ValidateInput(false)]
        public ActionResult PostComment(int id, string name, string email, string commentBody)
        {

            Post post = GetPost(id);

            Comment comment = new Comment {Name = name, Email = email, Body = commentBody, PostId = id, Time = DateTime.Now};
            _modelEntities.Comments.Add(comment);
            _modelEntities.SaveChanges();
            return RedirectToAction("Details", new {id = id});




        }


        public ActionResult Delete(int id)
        {
            if (IsAdmin)
            {
                Post post = GetPost(id);
                _modelEntities.Posts.Remove(post);
                _modelEntities.SaveChanges();



            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteComment(int id)
        {
            
            if (IsAdmin)
            {
                
                Comment comment = _modelEntities.Comments.First(x => x.Id== id);
                int postId = comment.PostId;
                _modelEntities.Comments.Remove(comment);
                _modelEntities.SaveChanges();
                return RedirectToAction("Details", new { id = postId });
               

            }
            else
            {
                return RedirectToAction("Index");
                

            }
            
        }

        public ActionResult Details(int id)
        {

            Post post = GetPost(id);
            ViewBag.isAdmin = IsAdmin;
            return View(post);


        }

        public ActionResult EditPost(int? id)
        {
            if (!IsAdmin)
            {
                return RedirectToAction("Index");
            }
            Post post = GetPost(id);

            return View("EditPost", post);
        }

        //public bool IsAdmin
        //{
        //    get { return Session["IsAdmin"] != null && (bool) Session["IsAdmin"] == true; }

        //}

        private Post GetPost(int? id)
        {
            //return id.HasValue ? _modelEntities.Posts.First(x => x.Id == id) : new Post() { Id = (int) id};
            if (id.HasValue)
            
                return _modelEntities.Posts.First(x => x.Id == id);
            else
                return new Post() {Id = -1};
            
        }



    }

}
