using Modules.Product.Domain.Model.Dto;
using Modules.Product.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SrvCornet.Http.Exceptions;
using AuctionHub.Common.Messages;
using Modules.Product.Domain.Model.Domain.ConstAndEnums;
using SrvCornet.Utils;

namespace Modules.Product.Domain.Model.Domain
{
    public class ProductDomain : AggregateRoot
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public List<ProductOptionDomain> Options { get; set; }
        public List<VariantDomain> Variants { get; set; }
        public string Status { get; set; }

        public ProductDomain(ProductDto dto)
        {
            Id = dto.Id;
            Code = dto.Code;
            Name = dto.Name;
            Description = dto.Description;
            BrandId = dto.BrandId;
            CategoryId = dto.CategoryId;
            SetOptions(dto.Options);
            SetVariants(dto.Variants);
            Status = dto.Status;
        }
        public void Update(ProductDto dto)
        {
            SetCode(dto.Code);
            SetName(dto.Name);
            SetDescription(dto.Description);
            SetBrandId(dto.BrandId);
            SetCategoryId(dto.CategoryId);
            SetOptions(dto.Options);
            SetVariants(dto.Variants);
        }
        private void SetCode(string code)
        {
            if (StringUtils.Equals(Code, code))
                return;
            Code = code;
            Modify();
        }
        private void SetName(string name)
        {
            if (StringUtils.Equals(Name, name))
                return;
            Name = name;
            Modify();
        }
        private void SetBrandId(string brandId)
        {
            if (StringUtils.Equals(BrandId, brandId))
                return;
            BrandId = brandId;
            Modify();
        }
        private void SetCategoryId(string categoryId)
        {
            if (StringUtils.Equals(CategoryId, categoryId))
                return;
            CategoryId = categoryId;
            Modify();
        }
        private void SetDescription(string description)
        {
            if (StringUtils.Equals(Description, description))
                return;
            Description = description;
            Modify();
        }
        private void SetOptions(List<ProductOptionDto> optionDtos)
        {
            Options = Options ?? new List<ProductOptionDomain>();

            var currentOptionIds = Options.Select(s => s.Id).ToList();
            var targetOptionIds = optionDtos.Select(s => s.Id).ToList();

            var removedOptionIds = currentOptionIds.Where(w => !targetOptionIds.Contains(w)).ToList();
            foreach (var removeOptionId in removedOptionIds)
                RemoveOption(removeOptionId);

            foreach(var optionDto in optionDtos)
            {
                var optionDomain = Options.Where(w => w.Id.Equals(optionDto.Id)).SingleOrDefault();
                if (optionDomain == null)
                    AddOption(optionDto);
                else
                    UpdateOption(optionDomain, optionDto);
            }

            var newListOption = new List<ProductOptionDomain>();
            for(int i = 0; i < optionDtos.Count; i++)
            {
                ProductOptionDomain optionDomain = null;
                for(int j = 0; j < Options.Count; j++)
                {
                    if (!optionDtos[i].Id.Equals(Options[j].Id))
                        continue;
                    optionDomain = Options[j];
                    if (i != j && currentOptionIds.Contains(optionDtos[i].Id))
                    {
                        AddIntegratedEvent(ProductEventType.PRODUCT_OPTION_INDEX_CHANGED, optionDtos[i].Id);
                        Modify();
                    }
                }
                newListOption.Add(optionDomain);
            }
            Options = newListOption;

        }
        private void RemoveOption(string optionId)
        {
            var option = Options.Where(w => w.Id.Equals(optionId)).SingleOrDefault();
            if (option == null)
                throw new NotFoundException(string.Format(ProductErrorMessages.PRODUCT_OPTION_NOT_FOUND, optionId));
            Options.RemoveAll(r => r.Id.Equals(optionId));
            AddIntegratedEvent(ProductEventType.PRODUCT_OPTION_REMOVED, optionId);
            Modify();
        }
        private void AddOption(ProductOptionDto dto)
        {
            var optionDomain = new ProductOptionDomain(this, dto);
            Options.Add(optionDomain);
            AddIntegratedEvent(ProductEventType.PRODUCT_OPTION_ADDED, dto.Id);
            Modify();
        }
        private void UpdateOption(ProductOptionDomain optionDomain, ProductOptionDto dto)
        {
            optionDomain.Update(dto);
            if (optionDomain.Modified)
            {
                AddIntegratedEvent(ProductEventType.PRODUCT_OPTION_UPDATED, dto.Id);
                Modify();
            }
                
        }
        private void SetVariants(List<VariantDto> variantDtos)
        {
            Variants = Variants ?? new List<VariantDomain>();

            List<string> currentVariantIds = Variants.Select(s => s.Id).ToList();
            List<string> targetVariantIds = variantDtos.Select(s => s.Id).ToList();

            var removedVariantIds = currentVariantIds.Where(w => !targetVariantIds.Contains(w)).ToList();
            foreach (var removedVariantId in removedVariantIds)
                RemoveVariant(removedVariantId);

            foreach(var variantDto in variantDtos)
            {
                var variantDomain = Variants.Where(w => w.Id.Equals(variantDto.Id)).SingleOrDefault();
                if (variantDomain == null)
                    AddVariant(variantDto);
                else
                    UpdateVariant(variantDomain, variantDto);
            }

            var newListVariant = new List<VariantDomain>();
            for (int i = 0; i < variantDtos.Count; i++)
            {
                VariantDomain variantDomain = null;
                for (int j = 0; j < Variants.Count; j++)
                {
                    if (!variantDtos[i].Id.Equals(Variants[j].Id))
                        continue;
                    variantDomain = Variants[j];
                    if (i != j && currentVariantIds.Contains(variantDtos[i].Id))
                    {
                        AddIntegratedEvent(ProductEventType.VARIANT_INDEX_CHANGED, variantDtos[i].Id);
                        Modify();
                    }
                }
                newListVariant.Add(variantDomain);
            }
            Variants = newListVariant;
        }
        public void RemoveVariant(string variantId)
        {
            int count = Variants.RemoveAll(r => r.Id.Equals(variantId));
            if(count == 0)
                throw new NotFoundException(string.Format(ProductErrorMessages.PRODUCT_VARIANT_NOT_FOUND, variantId));
            AddIntegratedEvent(ProductEventType.VARIANT_REMOVED, variantId);
            Modify();
        }
        public void AddVariant(VariantDto dto)
        {
            var variantDomain = new VariantDomain(this, dto);
            Variants.Add(variantDomain);
            AddIntegratedEvent(ProductEventType.VARIANT_ADDED, dto.Id);
            Modify();
        }
        public void UpdateVariant(VariantDomain variantDomain, VariantDto dto)
        {
            variantDomain.Update(dto);
            if(variantDomain.Modified)
            {
                AddIntegratedEvent(ProductEventType.VARIANT_UPDATED, variantDomain.Id);
                Modify();
            }
        }
        
    }
}
