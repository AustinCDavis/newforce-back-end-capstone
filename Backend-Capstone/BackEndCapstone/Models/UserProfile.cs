﻿using System.ComponentModel.DataAnnotations;

namespace BackEndCapstone.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public DateTime CreateDateTime { get; set; }

       
        [MaxLength(255)]
        public string ImageLocation { get; set; }

        [Required]
        public int UserTypeId { get; set; }
        public UserType? UserType { get; set; }


        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
