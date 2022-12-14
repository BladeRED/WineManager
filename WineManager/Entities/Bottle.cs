using System.Drawing;
using WineManager.DTO;

namespace WineManager.Entities
{
    public class Bottle
    {

        public int BottleId { get; set; }
        public string Name { get; set; }
        public int Vintage { get; set; }
        public int StartKeepingYear { get; set; }
        public int EndKeepingYear { get; set; }
        public string Color { get; set; }
        public string Designation { get; set; }
        public string? DrawerPosition { get; set; }

        //Navigation properties //
        public int? DrawerId { get; set; }
        public Drawer? Drawer { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public Bottle(BottleDto bottleDto, int userId)
        {
            Name = bottleDto.Name;
            Vintage = bottleDto.Vintage;
            if (bottleDto.StartKeepingYear != null)
                StartKeepingYear = (int)bottleDto.StartKeepingYear;
            else 
                bottleDto.StartKeepingYear = 5;
            if (bottleDto.EndKeepingYear != null)
                EndKeepingYear = (int)bottleDto.EndKeepingYear;
            else
                bottleDto.EndKeepingYear = 8;
            Color = bottleDto.Color;
            Designation = bottleDto.Designation;
            DrawerId = bottleDto.DrawerId;
            UserId = userId;
            DrawerPosition = bottleDto.DrawerPosition;
        }
        public Bottle()
        {

        }
    }
}
