using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Schemes.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        [HttpGet]
        public ActionResult CreatePost(int id = 0, string UserId = null)
        {
            Post post = new Post();
            if (id == 0)
            {
                post.UserId = UserId;
            }
            if (UserId == null)
            {
                post = Repository.GetPostById(id);
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult CreatePost(Post model)
        {
            int id = 0;
            if (model.id == 0)
            {
                model.time = DateTime.Now;
                id = Repository.AddPost(model);
            }
            else
            {
                Repository.UpdatePost(model);
                id = model.id;
            }
            return RedirectToAction("PostPage", "Post", new { id = id });
        }


        public ActionResult PostPage(int id)
        {
            Post post = new Post();
            post = Repository.GetPostById(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewPost model = new ViewPost(post);
            return View(model);
        }

        public ActionResult DeletePost(int id)
        {
            Repository.DeletePost(id);
            return RedirectToAction("Index", "Home");
        }
   
    }
}