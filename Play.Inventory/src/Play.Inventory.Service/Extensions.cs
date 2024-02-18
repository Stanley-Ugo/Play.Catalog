using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemsDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemsDto(item.Id, name, description, item.Quantity, item.AcquiredDate);
        }
    }
}