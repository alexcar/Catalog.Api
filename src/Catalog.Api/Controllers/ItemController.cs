﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Requests.Item;
using Catalog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Catalog.Api.Filters;
using Catalog.Api.Responses;
using Catalog.Domain.Responses.Item;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _itemService.GetItemsAsync();

            var totalItems = result.Count();

            var itemsOnPage = result
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            var model = new PaginatedItemResponseModel<ItemResponse>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [HttpGet("{id:guid}")]
        [ItemExists]
        [ResponseCache(Duration = 100, VaryByQueryKeys = new [] { "*" })]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _itemService.GetItemAsync(
                new GetItemRequest
                {
                    Id = id
                });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddItemRequest request)
        {
            var result = await _itemService.AddItemAsync(request);

            return CreatedAtAction(nameof(GetById), new
            {
                id = result.Id
            }, null);
        }

        [HttpPut("id:guid")]
        [ItemExists]
        public async Task<IActionResult> Put(Guid id, EditItemRequest request)
        {
            request.Id = id;
            var result = await _itemService.EditItemAsync(request);

            return Ok(result);
        }

        [HttpDelete("id:guid")]
        [ItemExists]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteItemRequest { Id = id };

            await _itemService.DeleteItemAsync(request);

            return NoContent();
        }
    }
}