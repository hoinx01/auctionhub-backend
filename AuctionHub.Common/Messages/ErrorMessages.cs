using System;
using System.Collections.Generic;

namespace AuctionHub.Common.Messages
{
    public class AccountErrorMessages
    {
        public const string REGISTER_EXIST_USERNAME = "Tên đăng nhập đã tồn tại";
        public const string LOGIN_INVALID_USER = "Tài khoản không tồn tại";
        public const string LOGIN_FAILED = "Thông tin đăng nhập không chính xác";
    }
    public class ProductErrorMessages
    {
        public const string IMAGE_NOT_FOUND = "Không tồn tại ảnh có id = {0}";
    }
}
