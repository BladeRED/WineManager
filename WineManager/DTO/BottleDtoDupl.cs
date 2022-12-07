using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoDupl
    {
        public string Name { get; set; }
        public int Vintage { get; set; }
        public int StartKeepingYear { get; set; }
        public int EndKeepingYear { get; set; }
        public string Color { get; set; }
        public string Designation { get; set; }

        //public int Quantity { get; set; }
    }
}
