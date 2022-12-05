namespace BottleManager.Entities
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
        public int DrawerId { get; set; }
        public Drawer? Drawer { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
