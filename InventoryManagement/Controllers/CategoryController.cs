﻿using Azure;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Service;
using InventoryManagement.Settings.HttpException;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReponse>>> GetCategories()
        {
            var response = await _service.GetCategories();

            if (!response.All(livro => livro.IsValid()))
            {
                var category = response.Where(item => !item.IsValid()).FirstOrDefault();
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, category.Notifications));
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReponse>> GetCategory(int id)
        {
            var response = await _service.GetCategory(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReponse>> CreateCategory(CategoryRequest categoryRequest)
        {
            var response = await _service.CreateCategory(categoryRequest);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryReponse>> UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            var response = await _service.UpdateCategory(id, categoryRequest);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryReponse>> DeleteCategory(int id)
        {
            var response = await _service.DeleteCategory(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }
    }
}
