using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Account.API.Models.Users
{
    public class AddAccountModel
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
