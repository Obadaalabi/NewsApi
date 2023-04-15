﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApiDomin.Models;
using NewsApiDomin.ViewModels.CategoryViewModel;
using NewsApiDomin.ViewModels;
using NewsApiDomin.ViewModels.ImageViewModel;
using NewsApiDomin.ViewModels.LikeViewModel;
using NewsApiServies.Auth.ClassStatic;
using Services.MyLogger;
using Services.Transactions.Interfaces;
using System.Text.Json;
using AutoMapper;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Author")]

   
    public class ImageController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWorkService;
        private readonly IMyLogger logger;
        private readonly IMapper mapper;

        public ImageController(IUnitOfWorkService unitOfWorkService, IMyLogger logger,IMapper mapper)
        {
            this.unitOfWorkService = unitOfWorkService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<(PaginationMetaData, List<ImageView>)>> GetAll(int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                var image = await unitOfWorkService.ImageService.GetAllAsync();
                if (image.Count() > 0)
                {
                    (image, var paginationData) = await unitOfWorkService.ImagePagination.GetPaginationAsync(pageNumber, pageSize, image);
                    //var images = image.Select(i => new ImageView { Id = i.Id, ArticleId = i.ArticleId, ImageUrl = i.ImageUrl, ImageDescription = i.ImageDescription });
                    if (image.Count() > 0)
                    {
                        List<ImageView> images = mapper.Map<List<ImageView>>(image);
                        Response.Headers.Add("X-Pagination",
                   JsonSerializer.Serialize(paginationData));
                        await logger.LogInformation("All Image table records fetched", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                        return Ok(new { paginationData, images });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    await logger.LogWarning("An Warning occurred while fetching all logs Image ", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return BadRequest();
                }

            }
            catch (Exception)
            {
                await logger.LogWarning("An Warning occurred while fetching all logs Image ", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                return BadRequest();
            }

        }

        [Authorize]
        [HttpGet("{id}",Name = "GetImage")]
        public async Task<ActionResult<ImageView>> GetById(int id)
        {


            try
            {
                var image = await unitOfWorkService.ImageService.GetByIdAsync(id);
                //var imageview=new ImageView { Id = image.Id,ArticleId=image.ArticleId, ImageDescription = image.ImageDescription,ImageUrl=image.ImageUrl };
                if (image == null)
                {
                    await logger.LogWarning("Failed to fetch Image with ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return BadRequest();
                }
                else
                {
                    ImageView imageView = mapper.Map<ImageView>(image);
                    await logger.LogInformation("Image with ID " + id + " fetched ", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return Ok(imageView);
                }

            }
            catch (Exception)
            {
                await logger.LogErorr("Erorr to fetch Image with ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<ActionResult<ImageView>> Post(CreateImage createImage)
        {

            try
            {
                //var image = new Image { ImageDescription=createImage.ImageDescription,ImageUrl= createImage.ImageUrl,ArticleId=createImage.ArticleId};
                Image image=mapper.Map<Image>(createImage);
                await unitOfWorkService.ImageService.AddAsync(image);

                if (await unitOfWorkService.CommitAsync())
                {
                    var lastID = await unitOfWorkService.ImageService.GetAllAsync();
                    var imageId = lastID.Max(b => b.Id);
                    image = await unitOfWorkService.ImageService.GetByIdAsync(imageId);
                    await logger.LogInformation("Image with ID " + imageId + " added", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    //var imageView = new ImageView { Id = image.Id, ArticleId = image.ArticleId, ImageDescription = image.ImageDescription, ImageUrl = image.ImageUrl };
                    ImageView imageView = mapper.Map<ImageView>(image);
                    return CreatedAtRoute("GetImage", new
                    {
                        id = imageId,
                    }, imageView);


                }
                else
                {
                    await logger.LogWarning("An warning occurred when adding the Image", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return BadRequest();
                }

            }
            catch (Exception)
            {
                await logger.LogErorr("An Erorr occurred when adding the Image", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateImage updateImage)
        {
            try
            {
              Image image=  await unitOfWorkService.ImageService.GetByIdAsync(id);
                //image.ImageDescription = updateImage.ImageDescription;
                //image.ImageUrl = updateImage.ImageUrl;
                mapper.Map(updateImage, image);
                await unitOfWorkService.ImageService.UpdateAsync(image);
                if (await unitOfWorkService.CommitAsync())
                {
                    await logger.LogInformation("Image with ID " + id + " updated", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return NoContent();
                }
                else
                {
                    await logger.LogWarning("An warning occurred when updateing the Image ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return BadRequest();
                }

            }
            catch (Exception)
            {
                await logger.LogErorr("An Erorr occurred when updateing the Image ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                return BadRequest();
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await unitOfWorkService.ImageService.DeleteAsync(id);
                if (await unitOfWorkService.CommitAsync())
                {
                    await logger.LogInformation("Image with ID " + id + " deleted", CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return NoContent();
                }
                else
                {
                    await logger.LogWarning("An warning occurred when deleteing the Image ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                    return BadRequest();
                }

            }
            catch (Exception)
            {
                await logger.LogErorr("An Erorr occurred when deleteing the Image ID " + id, CurrentUser.Id(HttpContext), CurrentUser.Role(HttpContext));
                return BadRequest();
            }
        }
    }
}
