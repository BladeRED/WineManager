using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoStock
    {
        public int BottleId { get; set; }
        public int? DrawerId { get; set; }
        public string? DrawerPosition { get; set; }
    }
}
