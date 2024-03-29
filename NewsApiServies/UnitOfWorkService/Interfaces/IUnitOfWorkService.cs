﻿using NewsApiDomin.Models;
using NewsApiDomin.ViewModels.ArticleViewModel;
using NewsApiDomin.ViewModels.CategoryViewModel;
using NewsApiDomin.ViewModels.CommentViewModel;
using NewsApiDomin.ViewModels.LikeViewModel;
using NewsApiServies.CRUD.Interfaces;
using NewsApiServies.Pagination.Interface;
using Services.CRUD.Interfaces;
using System.Threading.Tasks;

namespace Services.Transactions.Interfaces
{
    public interface IUnitOfWorkService
    {
       // public IBaseCRUDService<Category> CategoryService { get; }
        public ICategoryService CategoryService { get; }
        public IArticleService ArticleService { get; }
        public IAuthorService AuthorService { get; }
        public ICommentsService CommentsService { get; }
        public IImageService ImageService { get; }
        public ILikeService LikeService { get; }
        public ILogService LogService { get; }
        public IUsersService UsersService{ get; }
        Task<bool> CommitAsync();

       // public IPaginationService<Category> CategoryPagination { get; }
       public IPaginationService<ListCommentView> ListCommentViewPagination { get; }
        public IPaginationService<ListLikeView> ListLikeViewPagination { get; }
        public IPaginationService<Log> LogPagination { get; }
        public IPaginationService<User> UserPagination { get; }
        public IPaginationService<Comment> CommentPagination { get; }
        public IPaginationService<Image> ImagePagination { get; }
        public IPaginationService<Like> LikePagination { get; }
        public IPaginationService<ArticleWithAuthorView> ArticleWithAuthorViewPagination { get; }
    }
}
