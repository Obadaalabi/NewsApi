﻿using NewsApiDomin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiDomin.ViewModels.ArticleViewModel
{
    public class ArticlView
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.MinValue;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        
    }
}
