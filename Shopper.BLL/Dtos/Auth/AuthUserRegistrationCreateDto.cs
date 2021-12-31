using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.BLL.Dtos.Auth
{
    public class AuthUserRegistrationCreateDto
    {
        [Required]
        public string UserName { get; set; }

        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
