using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDto
    {
        public string Name { get; set; }
        public int Vintage { get; set; }
        public int StartKeepingYear { get; set; }
        public int EndKeepingYear { get; set; }
        public string Color { get; set; }
        public string Designation { get; set; }
        public int? DrawerId { get; set; }

        public BottleDto(Bottle bottle)
        {
            Name = bottle.Name;
            Vintage = bottle.Vintage;
            StartKeepingYear = bottle.StartKeepingYear;
            EndKeepingYear = bottle.EndKeepingYear;
            Color = bottle.Color;
            Designation = bottle.Designation;
            DrawerId = bottle.DrawerId;
        }
        public BottleDto()
        {

        }
    }
}
