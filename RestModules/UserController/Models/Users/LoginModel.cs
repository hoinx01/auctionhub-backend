using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserController.Models.Users
{
    public class LoginModel
    {
        [MaxLength(255)]
        [Required]
        public string LoginName { get; set; }
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
    }
}
