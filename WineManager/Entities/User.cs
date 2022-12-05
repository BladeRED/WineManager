using System.ComponentModel.DataAnnotations;

namespace WineManager.Entities
{
    public class User
    { 
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Password { get; set; }   

        // Navigation properties //

        public List<Bottle>? Bottles { get; set; }
        public List<Cave>? Caves { get; set; }
        public List<Drawer>? Drawers { get; set; }

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

