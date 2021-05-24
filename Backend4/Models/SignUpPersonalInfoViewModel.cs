using System;
using System.ComponentModel.DataAnnotations;
using Backend4.Models.Controls;

namespace Backend4.Models
{
    public class SignUpPersonalInfoViewModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }

        [Required]
        public Int32 BirthdayDay { get; set; }
        [Required]
        public Int32 BirthdayMonth { get; set; }
        [Required]
        public Int32 BirthdayYear { get; set; }

        [Required]
        public String Gender { get; set; }

        public Boolean Existed { get; set; }

        public Month[] Months { get; set; }
    }
}
