using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMaterialProductRepository _materialProductRepository;
        private readonly IMaterialService _materialService;
        public ProductService(IProductRepository productRepository, IMaterialProductRepository materialProductRepository,
            IMaterialService materialService)
        {
            _productRepository = productRepository;
            _materialProductRepository = materialProductRepository;
            _materialService = materialService;
        }

        public List<Product> GetProductList()
        {
            return _productRepository.GetProductList().ToList();
        }

        public List<Product> SearchProduct(string searchValue)
        {
            List<Product> productList = _productRepository.GetProductList().
                Where(c => c.ProductName.ToLower().Contains(searchValue)).ToList();
            return productList;
        }

        public bool DeleteProduct(string productId)
        {
            Product? product = _productRepository.GetProductList().
                Where(c => c.ProductId == Guid.Parse(productId)).FirstOrDefault();

            if (product == null)
            {
                return false;
            }
            product.Status = false;
            _productRepository.UpdateProduct(product);
            bool result = _productRepository.SaveChange();
            return result;
        }

        public bool AddProduct(ProductRequestDTO productDTO)
        {
            Guid.TryParse(productDTO.CounterId, out Guid counterId);
            var productId = Guid.NewGuid();
            Product product = new Product()
            {
                ProductId = productId,
                ProductName = productDTO.ProductName,
                Price = productDTO.Price,
                AmountInStock = productDTO.AmountInStock,
                AvatarImg = productDTO.AvatarImg,
                CounterId = counterId,
                Status = true
            };
            _productRepository.AddProduct(product);
            List<MaterialProduct> materialProducts = new List<MaterialProduct>();
            for (int i = 0; i < productDTO.MaterialDTOs?.Count; i++)
            {
                if (productDTO.MaterialDTOs[i].MaterialId != null)
                {
                    MaterialProduct materialProduct = new MaterialProduct()
                    {
                        MaterialProductId = Guid.NewGuid(),
                        ProductId = productId,
                        MaterialId = Guid.Parse(productDTO.MaterialDTOs[i].MaterialId),
                        MaterialSize = decimal.Parse(productDTO.MaterialDTOs[i].MaterialSize)
                    };
                    materialProducts.Add(materialProduct);
                }
            }
            _materialProductRepository.AddRange(materialProducts);
            bool result = _productRepository.SaveChange();
            return result;

        }

        public bool UpdateProduct(ProductRequestDTO productDTO)
        {
            Guid.TryParse(productDTO.ProductId, out Guid productId);
            Guid.TryParse(productDTO.CounterId, out Guid counterId);
            Product? product = _productRepository.GetById(productId);
            if (product == null) return false;
            if (!productDTO.AvatarImg.IsNullOrEmpty())
            {
                product.AvatarImg = productDTO.AvatarImg;
            }
            product.ProductName = productDTO.ProductName;
            product.Price = productDTO.Price;
            product.AmountInStock = productDTO.AmountInStock;
            product.CounterId = counterId;
            _productRepository.UpdateProduct(product);

            var existingMaterial = product.MaterialProducts.ToList();
            _materialProductRepository.DeleteRange(existingMaterial);

            List<MaterialProduct> updatedMaterialProducts = new List<MaterialProduct>();
            for (int i = 0; i < productDTO.MaterialDTOs?.Count; i++)
            {
                if (productDTO.MaterialDTOs[i].MaterialId != null)
                {
                    MaterialProduct materialProduct = new MaterialProduct()
                    {
                        MaterialProductId = Guid.NewGuid(),
                        ProductId = productId,
                        MaterialId = Guid.Parse(productDTO.MaterialDTOs[i].MaterialId),
                        MaterialSize = decimal.Parse(productDTO.MaterialDTOs[i].MaterialSize)
                    };
                    updatedMaterialProducts.Add(materialProduct);
                }
            }
            _materialProductRepository.AddRange(updatedMaterialProducts);

            bool result = _productRepository.SaveChange();
            return result;

        }

        public bool CheckNameExisted(string productName)
        {
            bool result = _productRepository.GetProductList().Any(c => c.ProductName == productName);
            if (result) return true;
            return false;
        }

        public bool CheckDuplicateMaterialId(ProductRequestDTO productDTO)
        {
            HashSet<string> seenMaterialIds = new HashSet<string>();
            foreach (var material in productDTO.MaterialDTOs)
            {
                if (material.MaterialId != null)
                {
                    if (seenMaterialIds.Equals(material.MaterialId.ToString())) return true;
                }
                seenMaterialIds.Add(material.MaterialId);
            }
            return false;
        }

        public Product GetProductById(string productId)
        {
            Guid.TryParse(productId, out Guid parseProductId);
            var product = _productRepository.GetById(parseProductId);
            if (product != null)
            {
                return product;
            }
            return new Product();
        }

        public ResponseDTO CheckValidationMaterialDTOList(List<MaterialDTO> list)
        {
            int count = 0;

            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].MaterialId == null)
                {
                    count++;
                }

                if (count == 4)
                {
                    return new ResponseDTO("Please choose at least 1 Material", false);
                }
                if (list[i].MaterialId != null && list[i].MaterialSize == null)
                {
                    return new ResponseDTO("Please input the size for Material " + ++i, false);
                }

                if (list[i].MaterialId == null && list[i].MaterialSize != null)
                {
                    return new ResponseDTO("Please choose the Material Name for Material " + ++i, false);
                }

                Regex regexSizeGold = new Regex("^[0-9]+(\\.[0-9]+)?$");
                Regex regexSizeDiamond = new Regex(@"^\d+$");
                if(list[i].MaterialId != null)
                {
                    var materialName = _materialService.GetMaterial(list[i].MaterialId).MaterialName;
                    if (list[i].MaterialId != null
                    && !regexSizeDiamond.IsMatch(list[i]?.MaterialSize)
                    && materialName.ToLower().Contains("diamond"))
                    {
                        return new ResponseDTO("The price " + ++i + " is invalid", false);
                    }

                    if (list[i].MaterialId != null
                        && !regexSizeGold.IsMatch(list[i]?.MaterialSize)
                        && !materialName.ToLower().Contains("diamond"))
                    {
                        return new ResponseDTO("The price " + ++i + " is invalid", false);
                    }
                }
                
            }
            var ids = list.Where(c => c.MaterialId != null).Select(c => c.MaterialId).ToList();
            var hasDuplicates = ids.Count() != ids.Distinct().Count();
            if (hasDuplicates)
            {
                return new ResponseDTO("Material is duplicated", false);
            }
            return new ResponseDTO("Check Validation Successfully", true);

        }
    }
}
