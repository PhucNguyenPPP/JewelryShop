using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMaterialProductRepository _materialProductRepository;
        public ProductService(IProductRepository productRepository, IMaterialProductRepository materialProductRepository)
        {
            _productRepository = productRepository;
            _materialProductRepository = materialProductRepository;
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
            
            if(product == null)
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
            foreach(var e in productDTO.MaterialDTOs)
            {
                MaterialProduct materialProduct = new MaterialProduct()
                {
                    MaterialProductId = Guid.NewGuid(),
                    ProductId = productId,
                    MaterialId = Guid.Parse(e.MaterialId),
                    MaterialSize = e.MaterialSize
                };
                materialProducts.Add(materialProduct);
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
            if(product == null) return false;
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
            foreach (var e in productDTO.MaterialDTOs)
            {
                MaterialProduct materialProduct = new MaterialProduct()
                {
                    MaterialProductId = Guid.NewGuid(),
                    ProductId = productId,
                    MaterialId = Guid.Parse(e.MaterialId),
                    MaterialSize = e.MaterialSize
                };
                updatedMaterialProducts.Add(materialProduct);
            }
            _materialProductRepository.AddRange(updatedMaterialProducts);

            bool result = _productRepository.SaveChange();
            return result;

        }

        public bool CheckNameExisted(string productName)
        {
            bool result = _productRepository.GetProductList().Any(c => c.ProductName == productName);
            if(result) return true;
            return false;
        }
        
        public bool CheckDuplicateMaterialId(ProductRequestDTO productDTO)
        {
            HashSet<string> seenMaterialIds = new HashSet<string>();
            foreach(var material in productDTO.MaterialDTOs)
            {
                if(material.MaterialId != null)
                {
                    if(seenMaterialIds.Equals(material.MaterialId.ToString())) return true;
                }
                seenMaterialIds.Add(material.MaterialId);
            }
            return false;
        }

    }
}
