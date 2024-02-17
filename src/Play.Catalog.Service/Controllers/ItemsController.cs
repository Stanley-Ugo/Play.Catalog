using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Get()
        {
            var items = (await itemsRepository.GetAllAsync())
                            .Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);
            if (item == null)
                return NotFound();
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {

            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
                return NotFound();

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
                return NotFound();

            await itemsRepository.DeleteAsync(existingItem.Id);

            return NoContent();
        }
    }
}