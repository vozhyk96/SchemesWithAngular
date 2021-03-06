﻿using Schemes.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes
{
    public class ViewPost
    {
        public Post post { get; set; }
        public string UserEmail { get; set; }
        public Picture picture { get; set; }
        public List<Tag> tags { get; set; }

        public ViewPost(Post post)
        {
            List<string> list = new List<string>();
            this.post = post;
            if (post != null)
            {
                UserEmail = Repository.GetUser(post.UserId).Email;
            }
            picture = new Picture(post.image);
            tags = Repository.FindNeedTags(post.id);
        }   
    }
}