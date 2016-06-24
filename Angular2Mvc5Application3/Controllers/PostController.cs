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
                    Repository.Delete(temp.id);
                    post.image = temp.Image;
                    post.json = temp.json;
                }
            }
            if (UserId == null)
            {
                post = Repository.GetPostById(id);
            }
            Picture p = new Picture(post.image);
            ViewBag.HtmlRaw = p.HtmlRaw;
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

        [HttpGet]
        public ActionResult EditorApp(Post post)
        {
            return View();
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditorApp(string picture, string url)
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

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }
            int id = GetIdFromUrl(url);
            byte[] image = Convert.FromBase64String(GetBase64(picture));
            Repository.AddScheme(id,User.Identity.GetUserId(),image);
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
            string[] s = url.Split('/');
            string id = s[s.Length-1];
            if(id[id.Length-1]=='#')
            {
                id = id.Substring(0, id.Length - 1);
            }
            return int.Parse(id);
        }
    }
}