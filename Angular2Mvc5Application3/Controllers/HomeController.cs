﻿using System;
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


                switch (s)
                {
                    case "5": // school voting
                              // check if he has already voted
                        var isIt = db.VoteLog.Where(v => v.SectionId == sectionId &&
                            v.UserName.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) && v.VoteForId == autoId).FirstOrDefault();
                        if (isIt != null)
                        {
                            // keep the school voting flag to stop voting by this member
                            HttpCookie cookie = new HttpCookie(url, "true");
                            Response.Cookies.Add(cookie);
                            return Json("<br />You have already rated this post, thanks !");
                        }

                        var sch = db.Posts.Where(sc => sc.id == autoId).FirstOrDefault();
                        if (sch != null)
                        {
                            object obj = sch.Votes;

                            string updatedVotes = string.Empty;
                            string[] votes = null;
                            if (obj != null && obj.ToString().Length > 0)
                            {
                                string currentVotes = obj.ToString(); // votes pattern will be 0,0,0,0,0
                                votes = currentVotes.Split(',');
                                // if proper vote data is there in the database
                                if (votes.Length.Equals(5))
                                {
                                    // get the current number of vote count of the selected vote, always say -1 than the current vote in the array 
                                    int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                                    // increase 1 for this vote
                                    currentNumberOfVote++;
                                    // set the updated value into the selected votes
                                    votes[thisVote - 1] = currentNumberOfVote.ToString();
                                }
                                else
                                {
                                    votes = new string[] { "0", "0", "0", "0", "0" };
                                    votes[thisVote - 1] = "1";
                                }
                            }
                            else
                            {
                                votes = new string[] { "0", "0", "0", "0", "0" };
                                votes[thisVote - 1] = "1";
                            }

                            // concatenate all arrays now
                            foreach (string ss in votes)
                            {
                                updatedVotes += ss + ",";
                            }
                            updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                            db.Entry(sch).State = EntityState.Modified;
                            sch.Votes = updatedVotes;
                            db.SaveChanges();

                            VoteLog vm = new VoteLog()
                            {
                                Active = true,
                                SectionId = Int16.Parse(s),
                                UserName = User.Identity.Name,
                                Vote = thisVote,
                                VoteForId = autoId
                            };

                            db.VoteLog.Add(vm);

                            db.SaveChanges();

                            // keep the school voting flag to stop voting by this member
                            HttpCookie cookie = new HttpCookie(url, "true");
                            Response.Cookies.Add(cookie);
                        }
                        break;
                    default:
                        break;
                }
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }

        public ActionResult Index(int? id, string s = "", int tagId = 0)
        {
            if(tagId != 0)
            {
                List<ViewPost> tagposts = Repository.GetPostsOfTag(tagId);
                return View(tagposts);
            }
            int page;
            if (id == null)
            {
                page = 0;
                Session["s"] = s;
            }
            else page = int.Parse(Session["Page"].ToString());
                
            List<ViewPost> posts = new List<ViewPost>();
            int maxPages = Repository.GetNumberOfPosts()/pageSize + 1;
            while ((posts.Count < pageSize)&&(page<=maxPages))
            {
                List<ViewPost> subposts = Repository.GetPostsSortedByDate(pageSize, page, Session["s"].ToString());
                posts.AddRange(subposts);
                page++;
            }
            Session["Page"] = page;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Tape", posts);
            }
            return View(posts);
        }
        private List<ViewPost> GetItemsPage(List<ViewPost> posts, int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return posts.OrderByDescending(t => t.post.time).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }



    }
}