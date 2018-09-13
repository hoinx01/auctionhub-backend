using SrvCornet.Dal.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHub.Data.Mongo.Dto
{
    public class MdAccount : BaseMongoDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
    public class AccountFilter : BaseFilter
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
