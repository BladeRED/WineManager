using BottleManager.Entities;

namespace BottleManager.DTO
{
    public class BottleDto
    {
        public string Name { get; set; }
        public int Vintage { get; set; }
        public int StartKeepingYear { get; set; }
        public int EndKeepingYear { get; set; }
        public string Color { get; set; }
        public string Designation { get; set; }
        public string DrawerPosition { get; set; }
    }
}
