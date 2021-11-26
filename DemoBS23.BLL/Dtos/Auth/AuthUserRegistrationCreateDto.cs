using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos.Auth
{
    public class AuthUserRegistrationCreateDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
