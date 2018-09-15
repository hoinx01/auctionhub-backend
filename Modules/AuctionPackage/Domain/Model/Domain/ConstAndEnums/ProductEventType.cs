using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain.ConstAndEnums
{
    public class ProductEventType
    {
        public const string IMAGE_ADDED = "image_added";
        public const string IMAGE_UPDATED = "image_updated";
        public const string IMAGE_REMOVED = "image_removed";

        public const string OPTION_ADDED = "option_added";
        public const string OPTION_UPDATED = "option_updated";
        public const string OPTION_REMOVED = "option_removed";

        public const string VARIANT_ADDED = "variant_added";
        public const string VARIANT_UPDATED = "variant_updated";
        public const string VARIANT_REMOVED = "variant_removed";
    }
}
