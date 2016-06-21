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
        List<ViewPost> posts = Repository.GetaLLPosts();
        const int pageSize = 4;

        public ActionResult Index(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Tape", GetItemsPage(page));
            }
            return View(GetItemsPage(page));
        }
        private List<ViewPost> GetItemsPage(int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return posts.OrderBy(t => t.UserEmail).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }
    }
}