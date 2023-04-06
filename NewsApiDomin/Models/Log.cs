﻿using DataAccess.Entities.Abstractions.Classes;
using NewsApiDomin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiDomin.Models
{
    public class Log : BaseNormalEntity
    {
        public  LogLevel logLevel { get; set; }
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int UserId { get; set; }
        public Author Author { get; set; } = new Author();
        public User User { get; set; }=new User();
      
    }
}
