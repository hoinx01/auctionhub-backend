using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Account.API.Models.Users
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
