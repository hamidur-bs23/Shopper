using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.DAL.Entities.Auth
{
    public class AuthUser : IdentityUser
    {/*
        [Required]
        [DataType(DataType.EmailAddress)]
        //public override string Email { get => base.Email; set => base.Email = value; }
        public string Email { get; set; }*/
    }
}
