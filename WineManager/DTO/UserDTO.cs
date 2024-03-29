﻿using System.ComponentModel.DataAnnotations;
using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public List<Bottle>? Bottles { get; set; }
        public List<Drawer>? Drawers { get; set; }
        public List<Cave>? Caves { get; set; }

        public UserDto() { }

        public UserDto(UserPostDto userPostDto)
        {
            Name = userPostDto.Name;
            Email = userPostDto.Email;
            BirthDate = userPostDto.BirthDate;
        }

        public UserDto(User user)
        {
            Name = user.Name;
            BirthDate = user.BirthDate;
            Email = user.Email;
            Drawers = user.Drawers;
        }
        public UserDto(UserPutDto userPutDto)
        {

            if (userPutDto.NewName != null)
                Name = userPutDto.NewName;
            if (userPutDto.NewEmail != null)
                Email = userPutDto.NewEmail;
            if (userPutDto.NewBirthDate != null)
                BirthDate = (DateTime)userPutDto.NewBirthDate;
        }
    }
}

