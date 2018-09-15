using Modules.Product.Domain.Model.Dto;
using Modules.Product.Domain.SeedWork;
using SrvCornet.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Model.Domain
{
    public class ImageDomain : Subdomain
    {
        public string ImageId { get; set; }
        public string Source { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageDomain(ImageDto dto)
        {
            Id = dto.Id;
            ImageId = dto.ImageId;
            Source = dto.Source;
            Width = dto.Width;
            Height = dto.Height;
        }
        public void Update(ImageDto dto)
        {
            SetImageId(dto.ImageId);
            SetSource(dto.Source);
            SetWidth(dto.Width);
            SetHeight(dto.Height);
        }
        private void SetImageId(string imageId)
        {
            if (StringUtils.Equals(imageId, ImageId))
                return;
            ImageId = imageId;
            Modify();
        }
        private void SetSource(string source)
        {
            if (StringUtils.Equals(Source, source))
                return;
            Source = source;
            Modify();
        }
        private void SetWidth(int width)
        {
            if (width == Width)
                return;
            Width = width;
            Modify();
        }
        private void SetHeight(int height)
        {
            if(height == Height)
            {
                return;
            }
            Height = height;
            Modify();
        }
    }
}
