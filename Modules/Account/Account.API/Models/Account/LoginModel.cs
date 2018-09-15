using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Account.API.Models.Users
{
    public class LoginModel
    {
        [MaxLength(255)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
