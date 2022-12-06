using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserPutDto
    {
        public string NewName { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public DateTime NewBirthDate { get; set; }

        public static User ConvertUserPutDtoToUser(UserPutDto userPutDto)
        {
            var user = new User
            {
                Name = userPutDto.NewName,
                Email = userPutDto.NewEmail,
                Password = userPutDto.NewPassword,
                BirthDate = userPutDto.NewBirthDate
            };

            return user;
        }
        public static UserDto ConvertUserPutDtoToUserDto(UserPutDto userPutDto)
        {
            var user = new UserDto
            {
                Name = userPutDto.NewName,
                Email = userPutDto.NewEmail,
                BirthDate = userPutDto.NewBirthDate
            };

            return user;
        }


    }
}
