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
                post.tags = post.tags.Insert(0, "#");
                post.rating = GetRaiting(post.Votes);
                db.Posts.Add(post);
                db.SaveChanges();
                int id = post.id;
                AddTags(post.tags, post.id);
                AddToPosts(post.UserId);
                return id;
            }
        }

        static public void AddToPosts(string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                ApplicationUser user = db.Users.Find(UserId);
                user.publicedPosts++;
                db.SaveChanges();
            }
        }

        static public int GetNumberOfPosts(string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(UserId);
                return user.publicedPosts;
            }
        }

        static public void UpdatePost(Post post)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                post.tags = post.tags.Insert(0, "#");
                Post newPost = db.Posts.Find(post.id);
                newPost.title = post.title;
                newPost.teme = post.teme;
                newPost.tags = post.tags;
                newPost.description = post.description;
                newPost.rating = GetRaiting(newPost.Votes);
                db.SaveChanges();
                AddTags(post.tags, post.id);
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
            vcomment.Likes = comment.Likes;
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

        static public int AddLike(int id, string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment comment = db.Comment.Find(id);
                if (Liked(comment.LikedUserIds, UserId))
                {
                    comment.LikedUserIds = comment.LikedUserIds.Replace(UserId+',', "");
                    comment.Likes--;
                }
                else
                {
                    comment.LikedUserIds = comment.LikedUserIds + UserId + ',';
                    comment.Likes++;
                }
                db.SaveChanges();
                return comment.Likes;
            }
        }

        static private bool Liked(string UserIds, string UserId)
        {
            if (UserIds == null)
                return false;
            string[] Ids = UserIds.Split(',');
            if (Ids.Contains(UserId))
                return true;
            else return false;
        }

        static public double GetRaiting(string Model)
        {
            if (Model == null)
                return 0;
            Single m_Average = 0;

            Single m_totalNumberOfVotes = 0;
            Single m_totalVoteCount = 0;
            Single m_currentVotesCount = 0;
            double m_inPercent = 0;
            var thisVote = string.Empty;

            if (Model.Length > 0)
            {
                // calculate total votes now
                string[] votes = Model.Split(',');
                for (int i = 0; i < votes.Length; i++)
                {
                    m_currentVotesCount = int.Parse(votes[i]);
                    m_totalNumberOfVotes = m_totalNumberOfVotes + m_currentVotesCount;
                    m_totalVoteCount = m_totalVoteCount + (m_currentVotesCount * (i + 1));
                }

                m_Average = m_totalVoteCount / m_totalNumberOfVotes;
                m_inPercent = (m_Average * 100) / 5;
                m_inPercent = Math.Round(m_inPercent);
                return m_inPercent;
            }
            return 0;
        }

        static public List<ViewPost> GetPostsSorted(int pageSize, int page, string sort, string s = "")
        {
            List<Post> posts = new List<Post>();
            switch (sort)
            {
                case "DateUp": posts = SortByDateUp(pageSize, page); break;
                case "DateDown": posts = SortByDateDown(pageSize, page); break;
                case "RateUp": posts = SortByRateUp(pageSize, page); break;
                case "RateDown": posts = SortByRateDown(pageSize, page); break;
                case "": posts = SortByDateDown(pageSize, page); break;
            }
            
            if (s != "")
            {
                List<Post> subposts = GetPostsWithSubstring(posts, s);
                posts = subposts;
            }


            List<ViewPost> result = new List<ViewPost>();
            foreach (var post in posts)
            {
                ViewPost vpost = new ViewPost(post);
                result.Add(vpost);
            }
            return result;
        }

        static private List<Post> SortByDateDown(int pageSize, int page)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var itemsToSkip = page * pageSize;
                return db.Posts.OrderByDescending(t => t.time).Skip(itemsToSkip).Take(pageSize).ToList();
            }
        }
        static private List<Post> SortByDateUp(int pageSize, int page)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var itemsToSkip = page * pageSize;
                return db.Posts.OrderBy(t => t.time).Skip(itemsToSkip).Take(pageSize).ToList();
            }
        }

        static private List<Post> SortByRateUp(int pageSize, int page)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var itemsToSkip = page * pageSize;
                return db.Posts.OrderBy(t => t.rating).Skip(itemsToSkip).Take(pageSize).ToList();
            }
        }

        static private List<Post> SortByRateDown(int pageSize, int page)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var itemsToSkip = page * pageSize;
                return db.Posts.OrderByDescending(t => t.rating).Skip(itemsToSkip).Take(pageSize).ToList();
            }
        }

        static private List<Post> GetPostsWithSubstring(List<Post> posts, string s)
        {
            List<Post> result = new List<Post>();
            foreach (Post post in posts)
            {
                ViewPost vpost = new ViewPost(post);
                if ((post.title.Contains(s)) || (post.teme.Contains(s)) || (post.tags.Contains(s) || (post.description.Contains(s) || (post.time.ToString().Contains(s)) || (vpost.UserEmail.Contains(s)))))
                {
                    result.Add(post);
                }
            }
            return result;
        }

        static public int GetNumberOfPosts()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Posts.Count();
            }
        }

        static private void AddTags(string strtags, int postid)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (strtags[0] == '#')
                    strtags = strtags.Remove(0, 1);
                DeleteTags(postid);
                string[] tags = strtags.Split('#');
                foreach (var tag in tags)
                {
                    string ptag = ParseTag(tag);
                    int id = Contains(ptag);
                    if(id > 0)
                    {
                        UpdateTag(id, postid);
                    }
                    else
                    {
                        AddTag(ptag, postid);
                    }
                }
            }
        }

        static private void DeleteTags(int postid)
        {
            List<Tag> tags = FindNeedTags(postid);
            foreach (var tag in tags)
            {
                string s = String.Format(",{0}", postid.ToString());
                if (tag.Count > 1)
                {
                    tag.idsOfPosts = tag.idsOfPosts.Replace(s, "");
                    tag.idsOfPosts = tag.idsOfPosts.Replace(postid.ToString() + ",", "");
                    if(tag.idsOfPosts[0]==',')
                        tag.idsOfPosts = tag.idsOfPosts.Remove(0, 1);
                    DeleteIdsFromTag(tag.id, tag.idsOfPosts);
                }
                else DeleteTag(tag.id);
            }
        }

        static private void DeleteTag(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Tag tag = db.Tags.Find(id);
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
        }

        static private void DeleteIdsFromTag(int id, string idsOfPosts)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Tag tag = db.Tags.Find(id);
                tag.idsOfPosts = idsOfPosts;
                tag.Count = idsOfPosts.Split(',').Length;
                db.SaveChanges();
            }
        }


        static public List<Tag> FindNeedTags(int postid)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Tag> needTags = new List<Tag>();
                foreach (var tag in db.Tags)
                {
                    if ((tag.idsOfPosts.Contains("," + postid.ToString() + ","))||(postid.ToString() == tag.idsOfPosts.Split(',')[0])|| (postid.ToString() == tag.idsOfPosts.Split(',')[tag.idsOfPosts.Split(',').Length-1]))
                        needTags.Add(tag);
                }
                return needTags;
            }
        }

        static private void UpdateTag(int id, int postid)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Tag mtag = db.Tags.Find(id);
                mtag.idsOfPosts += String.Format(",{0}", postid);
                mtag.Count++;
                db.SaveChanges();
            }
        }

        static private void AddTag(string ptag, int postid)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Tag mtag = new Tag();
                mtag.tagName = ptag;
                mtag.idsOfPosts = postid.ToString();
                mtag.Count = 1;
                db.Tags.Add(mtag);
                db.SaveChanges();
            }
        }

        static private string ParseTag(string tag)
        {
            if(tag[tag.Length-1] == ' ')
            {
                tag = tag.Substring(0, tag.Length - 1);
            }
            return tag;
        }

        static private int Contains(string stag)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var tag in db.Tags)
                {
                    if (tag.tagName == stag)
                        return tag.id;
                }
                return 0;
            }
        }

        static public List<ViewPost> GetPostsOfTag(int tagId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Tag tag = db.Tags.Find(tagId);
                List<ViewPost> result = new List<ViewPost>();
                foreach (var sid in tag.idsOfPosts.Split(','))
                {
                    Post post = db.Posts.Find(int.Parse(sid));
                    result.Add(new ViewPost(post));
                }
                return result;
            }
        }

        static public List<Tag> GetAllTags()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Tag> tags = db.Tags.OrderByDescending(t => t.Count).ToList();
                return tags;
            }
        }
    }
}