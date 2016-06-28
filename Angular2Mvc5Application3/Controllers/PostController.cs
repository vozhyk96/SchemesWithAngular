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
                Temp temp = Repository.GetTemp(UserId);
                if (temp != null)
                {
                    post.image = temp.Image;
                }
            }
            if (UserId == null)
            {
                post = Repository.GetPostById(id);
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
            if (id != 0)
            {
                json = Repository.GetPostById(id).json;
            }
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

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Upload/MVC_") + _imgname + _ext;
                    _imgname = "MVC_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                }
            }
            int id = GetIdFromUrl(url);
            byte[] image = Convert.FromBase64String(GetBase64(picture));
            Repository.AddScheme(id,User.Identity.GetUserId(),image,json);
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
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