using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> itemsDtos = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronz sword", "deals a small amount of damage", 20, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Hi-Potion", "Restores a massive amount of HP", 15, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return itemsDtos;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = itemsDtos.Where(item => item.Id == id).SingleOrDefault();
            if (item == null)
                return NotFound();
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            itemsDtos.Add(item);

            return CreatedAtAction(nameof(GetById), new { Id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = itemsDtos.Where(item => item.Id == id).SingleOrDefault();

            if (existingItem == null)
                return NotFound();

            var updateItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = itemsDtos.FindIndex(existingItem => existingItem.Id == existingItem.Id);
            itemsDtos[index] = updateItem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            var index = itemsDtos.FindIndex(existingItem => existingItem.Id == id);

            if (index < 0)
                return NotFound();

            itemsDtos.RemoveAt(index);

            return NoContent();
        }
    }
}