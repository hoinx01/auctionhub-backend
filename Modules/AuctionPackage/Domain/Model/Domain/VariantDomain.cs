using Domain.Model.Dto;
using Modules.Product.Domain.Model.Domain.ConstAndEnums;
using Modules.Product.Domain.Model.Dto;
using Modules.Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AuctionHub.Common.Messages;
using SrvCornet.Http.Exceptions;
using SrvCornet.Utils;

namespace Modules.Product.Domain.Model.Domain
{
    public class VariantDomain : Subdomain<ProductDomain>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public List<VariantOptionDomain> Options { get; set; }
        public List<ImageDomain> Images { get; set; }
        public string Status { get; set; }

        public VariantDomain(ProductDomain root, VariantDto dto)
        {
            Root = root;
            Code = dto.Code;
            Name = dto.Name;
            Description = dto.Description;
            BasePrice = dto.BasePrice;
            Status = VariantStatus.ACTIVE;
            SetOptions(dto.Options);
            SetImages(dto.Images);
        }

        public void Update(VariantDto dto)
        {
            SetCode(dto.Code);
            SetName(dto.Name);
            SetDescription(dto.Description);
            SetBasePrice(dto.BasePrice);
            SetOptions(dto.Options);
            SetImages(dto.Images);
        }
        private void SetCode(string code)
        {
            if (StringUtils.Equals(code, Code))
                return;
            Code = code;
            Modify();       
        }
        private void SetName(string name)
        {
            if (StringUtils.Equals(name, Name))
                return;
            Name = name;
            Modify();
        }
        private void SetDescription(string description)
        {
            if (StringUtils.Equals(description, Description))
                return;
            Description = description;
            Modify();
        }
        private void SetBasePrice(decimal basePrice)
        {
            if (basePrice == BasePrice)
                return;
            BasePrice = basePrice;
            Modify();
        }
        private void SetOptions(List<VariantOptionDto> options)
        {
            Options = Options ?? new List<VariantOptionDomain>();
            var currentOptionNames = Options.Select(s => s.OptionName).ToList();
            var targetOptionNames = options.Select(s => s.OptionName).ToList();

            bool optionChange = false;

            var removedOptionNames = currentOptionNames.Where(w => !targetOptionNames.Contains(w)).ToList();
            foreach (var removedOptionName in removedOptionNames)
            {
                optionChange = true;
                Options.RemoveAll(r => r.OptionName.Equals(removedOptionName));
            }
                

            var targetOptions = new List<VariantOptionDomain>();

            foreach(var targetOption in options)
            {
                var productOption = Root.Options.Where(o => o.Name.Equals(targetOption.OptionName)).SingleOrDefault();
                if (productOption == null)
                    throw new NotFoundException(string.Format(ProductErrorMessages.PRODUCT_OPTION_NOT_FOUND, targetOption.OptionName));
                if (currentOptionNames.Contains(targetOption.OptionName))
                {
                    var currentOption = Options.Where(w => w.OptionName.Equals(targetOption.OptionName)).SingleOrDefault();
                    if (!StringUtils.Equals(currentOption.Value, targetOption.Value))
                    {
                        optionChange = true;
                        currentOption.Value = targetOption.Value;
                        targetOptions.Add(currentOption);
                    }
                    else
                        continue;
                }
                else
                {
                    optionChange = true;
                    var option = new VariantOptionDomain(targetOption);
                    targetOptions.Add(option);
                }
            }
            Options = targetOptions;

            if (optionChange)
                Modify();
        }
        private void SetImages(List<ImageDto> imageDtos)
        {
            Images = Images ?? new List<ImageDomain>();

            List<string> targetImageIds = imageDtos.Select(s => s.Id).ToList();
            List<string> currentImageIds = Images.Select(s => s.Id).ToList();

            var removedImageIds = currentImageIds.Where(s => !targetImageIds.Contains(s)).ToList();
            foreach(var imageId in removedImageIds)
            {
                RemoveImage(imageId);
            }

            foreach(var imageDomain in Images)
            {
                var targetDto = imageDtos.Where(w => w.Id.Equals(imageDomain.Id)).SingleOrDefault();
                if (targetDto == null)
                    continue;
                UpdateImage(targetDto);
            }

            var newImageIds = targetImageIds.Where(w => !currentImageIds.Contains(w)).ToList();
            foreach(var imageId in newImageIds)
            {
                var imageDto = imageDtos.Where(w => w.Id.Equals(imageId)).SingleOrDefault();
                AddImage(imageDto);
            }


        }
        private void AddImage(ImageDto dto)
        {
            
            var imageDomain = new ImageDomain(dto);
            Images.Add(imageDomain);
            Root.AddIntegratedEvent(ProductEventType.IMAGE_ADDED, dto.Id);
            Modify();
        }
        private void RemoveImage(string imageId)
        {
            Images.RemoveAll(r => r.Id.Equals(imageId));
            Root.AddIntegratedEvent(ProductEventType.IMAGE_REMOVED, imageId);
            Modify();
        }
        private void UpdateImage(ImageDto dto)
        {
            var imageDomain = Images.Where(s => s.Id.Equals(dto.Id)).SingleOrDefault();
            if (imageDomain == null)
                throw new NotFoundException(string.Format(ProductErrorMessages.PRODUCT_IMAGE_NOT_FOUND, dto.Id));
            imageDomain.Update(dto);
            if (imageDomain.Modified)
                Modify();
        }
    }
}
