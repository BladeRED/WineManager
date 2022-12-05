namespace BottleManager.Entities
{
    public class User
    {


        int UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        DateTime BirthDate { get; set; }
        string Password { get; set; }   

        // Navigation properties //

        List<Bottle>? Bottles { get; set; }
        List<Cave>? Caves { get; set; }
        List<Drawer>? Drawers { get; set; }

        public User()
        {
        }

        public User(int userId, string name, string email, DateTime birthDate, string password, List<Bottle>? bottles, List<Cave>? caves, List<Drawer>? drawers)
        {
            UserId = userId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Password = password;
            Bottles = bottles;
            Caves = caves;
            Drawers = drawers;
        }
    }
}

