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
    public class OrderItemController : Controller
    {
        private readonly OrderItemService _service;

        public OrderItemController(OrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItems()
        {
            var response = await _service.GetOrderItems();

            if (!response.All(item => item.IsValid()))
            {
                var orderItem = response.Where(item => !item.IsValid()).FirstOrDefault();
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, orderItem.Notifications));
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(int id)
        {
            var response = await _service.GetOrderItem(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }
    }
}
