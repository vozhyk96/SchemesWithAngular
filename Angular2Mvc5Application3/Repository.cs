using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schemes.Models;
using Schemes.Models.DbModels;
using Schemes.Models.ViewModels;

namespace Schemes
{
    static public class Repository
    {


        static public ApplicationUser GetUser(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser appuser = new ApplicationUser();
                appuser = db.Users.Find(id);
                return (appuser);
            }
        }

        static public void ChangeUser(ViewUser user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser appuser = db.Users.Find(user.id);
                if(user.surname != null)
                {
                    appuser.surname = user.surname;
                }
                if(user.name != null)
                {
                    appuser.name = user.name;
                }
                if(user.patronymic != null)
                {
                    appuser.patronymic = user.patronymic;
                }
                db.SaveChanges();
            }

        }

        static public void AddPicture(string id, byte[] imageData)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser appuser = db.Users.Find(id);
                appuser.Image = imageData;
                db.SaveChanges();
            }
        }

        static public int AddPost(Post post)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Posts.Add(post);
                db.SaveChanges();
                int id = post.id;
                return id;
            }
        }

        

        static public void UpdatePost(Post post)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Post newPost = db.Posts.Find(post.id);
                newPost.title = post.title;
                newPost.teme = post.teme;
                newPost.tags = post.tags;
                newPost.description = post.description;
                db.SaveChanges();
            }
        }

        static public List<ViewPost> GetPostsOfUser(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ViewPost> posts = new List<ViewPost>();
                foreach(Post post in db.Posts)
                {
                    if(post.UserId == id)
                    {
                        Post p = post;
                        ViewPost vp = new ViewPost(p);
                        posts.Add(vp);
                    }
                }
                return posts;
            }
        }

        static public Post GetPostById(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Post post = db.Posts.Find(id);
                return post;
            }
            
        }
        static public List<ViewPost> GetaLLPosts()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ViewPost> posts = new List<ViewPost>();
                foreach (Post post in db.Posts)
                {
                    Post p = post;
                    ViewPost vp = new ViewPost(p);
                    posts.Add(vp);
                    
                }
                return posts;
            }
        }

        static public void DeletePassword(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(id);
                user.PasswordHash = null;
                db.SaveChanges();
            }

        }

        static public void DeleteImage(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(id);
                user.Image = null;
                db.SaveChanges();
            }
        }

        static public void DeletePost(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Post post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
            }
        }

        static public List<ViewPost> FindPostsWithString(string s)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ViewPost> posts = new List<ViewPost>();
                foreach (Post post in db.Posts)
                {
                    ViewPost vpost = new ViewPost(post);
                    if((post.title.Contains(s))||(post.teme.Contains(s))||(post.tags.Contains(s)||(post.description.Contains(s)||(post.time.ToString().Contains(s))||(vpost.UserEmail.Contains(s)))))
                    {
                        posts.Add(new ViewPost(post));
                    }
                }
                return posts;
            }
        }

        static public void AddScheme(int id, string UserId, byte[] image, string json)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if(id == 0)
                {
                    Temp temp = GetTemp(UserId);
                    if (temp != null)
                    {
                        temp = db.Temp.Find(temp.id);
                        temp.Image = image;
                        temp.UserId = UserId;
                        temp.json = json;
                        db.SaveChanges();
                    }
                    temp = new Temp();
                    temp.Image = image;
                    temp.UserId = UserId;
                    temp.json = json;
                    db.Temp.Add(temp);
                }
                else
                {
                    Post post = db.Posts.Find(id);
                    post.image = image;
                    post.json = json;
                }
                db.SaveChanges();
            }

        }

        static public Temp GetTemp(string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var temp in db.Temp)
                {
                    if (temp.UserId == UserId)
                    {
                        return temp;
                    }
                }
            }
            return null;
        }
        static public void DeleteTemp(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Temp temp = db.Temp.Find(id);
                db.Temp.Remove(temp);
                db.SaveChanges();
            }
        }

        static public Comment AddComment(Comment comment)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return comment;
            }
        }

        static public Comment GetComment(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment comment = db.Comment.Find(id);
                return comment;
            }
        }

        static public ViewComment CommentToViewComment(Comment comment)
        {
            ViewComment vcomment = new ViewComment();
            vcomment.id = comment.id;
            vcomment.PostId = comment.PostId;
            vcomment.EmailAutor = comment.EmailAutor;
            vcomment.CommentText = comment.CommentText;
            vcomment.time = comment.time.ToString();
            return vcomment;
        }

        static public List<ViewComment> GetComments(int PostId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ViewComment> result = new List<ViewComment>();
                foreach (var comment in db.Comment)
                {
                    if (comment.PostId == PostId)
                        result.Add(CommentToViewComment(comment));
                        
                }
                return result;
            }

        }

    }
}