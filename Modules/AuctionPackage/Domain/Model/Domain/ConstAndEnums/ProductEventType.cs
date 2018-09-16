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

        public const string PRODUCT_OPTION_ADDED = "product_option_added";
        public const string PRODUCT_OPTION_UPDATED = "product_option_updated";
        public const string PRODUCT_OPTION_REMOVED = "product_option_removed";
        public const string PRODUCT_OPTION_INDEX_CHANGED = "product_option_index_changed";

        public const string VARIANT_ADDED = "variant_added";
        public const string VARIANT_UPDATED = "variant_updated";
        public const string VARIANT_REMOVED = "variant_removed";
        public const string VARIANT_INDEX_CHANGED = "variant_index_changed";

    }
}
