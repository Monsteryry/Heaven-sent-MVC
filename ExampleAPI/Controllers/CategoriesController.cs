using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using ExampleApi.Models.InputModels.Categories;
using ExampleApi.Models.OutputModels.Categories;
using Swashbuckle.Swagger.Annotations;

namespace ExampleApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Category details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category details", typeof(CategoryOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var outputCategory = _mapper.Map<CategoryOutputModel>(category);
            return Ok(outputCategory);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [SwaggerResponse(HttpStatusCode.OK, "List of Categories details", typeof(List<CategoryOutputModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound();
            }

            var outputCategories = categories.Select(category => _mapper.Map<CategoryOutputModel>(category)).ToList();
            return Ok(outputCategories);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="model">Category details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Model added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model cannot be null")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model did not pass validation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Server could not process request")]
        public async Task<IHttpActionResult> Post(CategoryInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = _mapper.Map<Category>(model);
            var result = await _categoryRepository.SaveCategoryAsync(newCategory);
            if (!result)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <param name="model">Category details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Model updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model cannot be null")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Input model did not pass validation")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category was not found/Empty object")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Server could not process request")]
        public async Task<IHttpActionResult> Put(int id, CategoryInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(model, category);
            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Delete selected category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Model deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category was not found/Empty object")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Server could not process request")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var result = await _categoryRepository.DeleteCategoryAsync(category);
            if (!result)
            {
                return InternalServerError();
            }

            return Ok();
        }

    }
}
