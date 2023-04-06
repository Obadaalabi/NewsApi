﻿using NewsApiDomin.Models;
using Services.CRUD;
using NewsApiServies.CRUD.Interfaces;
using Repositories.Interfaces;
using Services.Transactions.Interfaces;

namespace NewsApiServies.CRUD
{
   
    public class FavoriteCategoryService : BaseCRUDService<FavoriteCategorie>, IFavoriteCategoryService
    {
        private readonly IUnitOfWorkRepo unitOfWorkRepo;

        public FavoriteCategoryService(IUnitOfWorkRepo  unitOfWorkRepo) : base(unitOfWorkRepo.FavoriteCategorieRepository)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }


    }
}
