using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schemes.Models;
using System.Data.Entity;
using Schemes.Models.DbModels;
using Schemes.Filters;

namespace Schemes.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        //List<ViewPost> posts = Repository.GetaLLPosts();
        const int pageSize = 4;

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public JsonResult SendRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            Int16 thisVote = 0;
            Int16 sectionId = 0;
            Int16.TryParse(s, out sectionId);
            Int16.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);

            if (!User.Identity.IsAuthenticated)
            {
                return Json("Not authenticated!");
            }

            if (autoId.Equals(0))
            {
                return Json("Sorry, record to vote doesn't exists");
            }

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                bool b = false;

                switch (s)
                {
                    case "5": // school voting
                              // check if he has already voted
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                        b = Repository.SetRaiting(r, s, id, url, User.Identity.Name);
                        if(!b)
                            return Json("<br />You have already rated this post, thanks !");
                        break;
                    default:
                        break;
                }
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }

        public ActionResult Index(int? id, string s = "", int tagId = 0, string sort = "")
        {
            if(Session["Sort"]==null)
            {
                Session["Sort"] = sort;
            }
            if(sort != "")
            {
                Session["Sort"] = sort;
            }
            List<ViewPost> tagposts = new List<ViewPost>();
            if (tagId != 0)
            {
                tagposts = Repository.GetPostsOfTag(tagId);
                Session["ByTags"] = true;
                return View(tagposts);
            }
            
            int page;
            if (id == null)
            {
                page = 0;
                Session["s"] = s;
                Session["ByTags"] = null;
            }
            else page = int.Parse(Session["Page"].ToString());
                
            List<ViewPost> posts = new List<ViewPost>();
            int maxPages = Repository.GetNumberOfPosts()/pageSize + 1;
            while ((posts.Count < pageSize)&&(page<=maxPages))
            {
                if (Session["ByTags"] != null)
                {
                    posts = tagposts;
                    page = maxPages + 1;
                }
                else
                {
                    List<ViewPost> subposts = Repository.GetPostsSorted(pageSize, page, Session["Sort"].ToString(), Session["s"].ToString());
                    posts.AddRange(subposts);
                    page++;
                }
            }
            Session["Page"] = page;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Tape", posts);
            }
            return View(posts);
        }
    }
}