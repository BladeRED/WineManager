using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoPut
    {
        public int BottleId { get; set; }   
        public string Name { get; set; }
        public int Vintage { get; set; }
        public int StartKeepingYear { get; set; }
        public int EndKeepingYear { get; set; }
        public string Color { get; set; }
        public string Designation { get; set; }
    }
}
