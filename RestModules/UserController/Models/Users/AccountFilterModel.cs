using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace UserController.Models.Users
{
    public class AccountFilterModel
    {
        [FromQuery(Name = "user_name")]
        public string UserName { get; set; }
        [FromQuery(Name = "email")]
        public string Email { get; set; }
        [FromQuery(Name = "phone_number")]
        public string PhoneNumber { get; set; }
    }
}
