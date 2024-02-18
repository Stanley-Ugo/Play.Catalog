using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemsDto AsDto(this InventoryItem item)
        {
            return new InventoryItemsDto(item.Id, item.Quantity, item.AcquiredDate);
        }
    }
}