namespace BottleManager.Entities
{
    public class Cave
    {


        public int CaveId { get; set; } 
        public string CaveType { get; set;}
        public string Family { get; set;}
        public string Brand { get; set;}
        public int Temperature { get; set;} 

        // Navigation Properties //
        List<Drawer> Drawers { get; set; }
        User User { get; set; }

        public Cave()
        {
        }

        public Cave(int caveId, string caveType, string family, string brand, int temperature, List<Drawer> drawers, User user)
        {
            CaveId = caveId;
            CaveType = caveType;
            Family = family;
            Brand = brand;
            Temperature = temperature;
            Drawers = drawers;
            User = user;
        }
    }
}
