﻿using Microsoft.EntityFrameworkCore;
using NewsApiData;
using NewsApiDomin.Models;
using NewsApiRepositories.Interfaces;
using Repositories;
using System.Collections.Generic;


namespace NewsApiRepositories
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        private readonly NewsApiDbContext dbContext;

        public LogRepository(NewsApiDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


    }
        
}
