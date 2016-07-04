using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;
using Schemes.Models.DbModels;
using Schemes.Models;
using Schemes.Models.ViewModels;
using Schemes.Filters;

namespace Schemes.Controllers
{
    [Culture]
    public class PostController : Controller
    {
        // GET: Post
        [HttpGet]
        public ActionResult CreatePost(int id = 0, string UserId = null)
        {
            Post post = new Post();
            if ((id == 0) && (UserId == null))
            {
                if (Session["id"] != null)
                    id = int.Parse(Session["id"].ToString());
                if (Session["UserId"] != null)
                    UserId = Session["UserId"].ToString();
            }
            if (id == 0)
            {
                post.UserId = UserId;
                Temp temp = Repository.GetTemp(UserId);
                if (temp != null)
                {
                    post.image = temp.Image;
                    post.json = temp.json;
                    
                }
                Session["UserId"] = UserId;
                Session["id"] = null;
            }
            if (UserId == null)
            {
                post = Repository.GetPostById(id);
                Session["id"] = id;
                Session["UserId"] = null;
            }

            post = GutFromSession(post);
            Picture p = new Picture(post.image);
            ViewBag.HtmlRaw = p.HtmlRaw;
            return View(post);
        }

        private Post GutFromSession(Post post)
        {
            if (Session["Title"] != null)
            {
                post.title = Session["Title"].ToString();
                Session["Title"] = null;
            }
            if (Session["Teme"] != null)
            {
                post.teme = Session["Teme"].ToString();
                Session["Teme"] = null;
            }
            if (Session["Tags"] != null)
            {
                post.tags = Session["Tags"].ToString();
                Session["Tags"] = null;
            }
            if (Session["Description"] != null)
            {
                post.description = Session["Description"].ToString();
                Session["Description"] = null;
            }
            return post;
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CreatePost")]
        public ActionResult CreatePost(Post model)
        {
            model = Parse(model);
            int id = 0;
            if (model.id == 0)
            {
                Temp temp = Repository.GetTemp(model.UserId);
                if (temp != null)
                {
                    model.image = temp.Image;
                    model.json = temp.json;
                   
                }
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

        private Post Parse(Post post)
        {
            if (post.title == null)
                post.title = String.Format("{0}",Resources.Resource.Post);
            if (post.teme == null)
                post.teme = "";
            if (post.tags == null)
                post.tags = "";
            else if (post.tags[0] == '#')
                post.tags = post.tags.Remove(0, 1);
            if (post.description == null)
                post.description = "";
            return post;
        }

        public ActionResult PostPage(int id)
        {
            Post post = new Post();
            post = Repository.GetPostById(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (post.Votes == null)
                post.Votes = "";
            ViewPost model = new ViewPost(post);
            return View(model);
        }

        public ActionResult DeletePost(int id)
        {
            Repository.DeletePost(id);
            return RedirectToAction("Index", "Home");
        }


        [MultipleButton(Name = "action", Argument = "EditorApp")]
        public ActionResult EditorApp(Post post)
        {
            Session["PostId"] = post.id;
            Session["Title"] = post.title;
            Session["Teme"] = post.teme;
            Session["tags"] = post.tags;
            Session["Description"] = post.description;
            return View(post.id);
        }
        [HttpGet]
        public JsonResult MyApp()
        {
            string json = "";
            string s = "";
            int id = 0;
            if (Session["PostId"] != null)
            {
                s = Session["PostId"].ToString();
                id = int.Parse(s);
            }
            string UserId = "";
            Temp temp = null;
            if (Session["UserId"] != null)
            {
                UserId = Session["UserId"].ToString();
                temp = Repository.GetTemp(UserId);
            }
            if (id != 0)
                json = Repository.GetPostById(id).json;
            else if(temp != null) json = temp.json;
                
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        
       
        public JsonResult EditorApp(string json, string picture, string url)
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                }
            }
            int id = GetIdFromUrl(url);
            byte[] image = Convert.FromBase64String(GetBase64(picture));
            Repository.AddScheme(id,User.Identity.GetUserId(),image,json);
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddComment(string text, string url)
        {
            Comment comment = new Comment();
            comment.EmailAutor = Repository.GetUser(User.Identity.GetUserId()).Email;
            comment.CommentText = text;
            comment.time = DateTime.Now;
            comment.PostId = GetPostIdFromUrl(url);
            comment = Repository.AddComment(comment);
            return Json(Repository.CommentToViewComment(comment), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddLike(int id)
        {
            int likes = Repository.AddLike(id, User.Identity.GetUserId());
            return Json(likes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetComments(string url)
        {
            int PostId = GetPostIdFromUrl(url);
            List<ViewComment> comments = Repository.GetComments(PostId);
            comments.Reverse();
            return Json(comments, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteComment(int id)
        {
            Repository.DeleteComment(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        private int GetPostIdFromUrl(string url)
        {
            string[] a = url.Split('/');
            int id = int.Parse(a[a.Length - 1]);
            return id;
        }

        

        private string GetBase64(string picture)
        {
            string[] s = picture.Split(',');
            string newpicture = s[s.Length - 1];
            return newpicture;
        }

        private int GetIdFromUrl(string url)
        {
            string[] s = url.Split('=');
            string id = s[s.Length-1];
            if(id[id.Length-1]=='#')
            {
                id = id.Substring(0, id.Length - 1);
            }
            int result;
            if(int.TryParse(id, out result))
                return result;
            return 0;
        }
    }
}