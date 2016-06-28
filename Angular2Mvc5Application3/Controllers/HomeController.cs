using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schemes.Models;

namespace Schemes.Controllers
{
    public class HomeController : Controller
    {
        //List<ViewPost> posts = Repository.GetaLLPosts();
        const int pageSize = 4;

        public ActionResult Index(int? id, string s = "")
        {
            List<ViewPost> posts = new List<ViewPost>();
            if (s == "")
                posts = Repository.GetaLLPosts();
            else posts = Repository.FindPostsWithString(s);
            /*else
            {
                LuceneRepository.BuildIndex();
                int count;
                posts = LuceneRepository.Search(s, out count);
            }*/

            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Tape", GetItemsPage(posts, page));
            }
            return View(GetItemsPage(posts, page));
        }
        private List<ViewPost> GetItemsPage(List<ViewPost> posts, int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return posts.OrderByDescending(t => t.post.time).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }

        

    }
}