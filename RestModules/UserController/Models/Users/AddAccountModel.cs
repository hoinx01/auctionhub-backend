﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserController.Models.Users
{
    public class AddAccountModel
    {
        public string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}