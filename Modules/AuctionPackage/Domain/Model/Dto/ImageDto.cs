using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Dto
{
    public class ImageDto
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
        public string Source { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
