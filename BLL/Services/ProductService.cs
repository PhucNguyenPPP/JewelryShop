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
        private readonly IGoldPriceService _goldPriceService;
        private readonly IEmployeeService _employeeService;
        private readonly IPriceRateRepository _priceRateRepository;
        private readonly IMaterialRepository _materialRepository;
        public ProductService(IProductRepository productRepository, IMaterialProductRepository materialProductRepository,
            IMaterialService materialService, IGoldPriceService goldPriceService, IEmployeeService employeeService,
            IPriceRateRepository priceRateRepository, IMaterialRepository materialRepository)
        {
            _productRepository = productRepository;
            _materialProductRepository = materialProductRepository;
            _materialService = materialService;
            _goldPriceService = goldPriceService;
            _employeeService = employeeService;
            _priceRateRepository = priceRateRepository;
            _materialRepository = materialRepository;
        }

        public List<Product> GetProductList()
        {
            var goldPriceList = _goldPriceService.GetGoldPrices();
            var productList = _productRepository.GetProductList().ToList();

            var goldBar100fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 100 fences");
            var priceGoldBar100fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar100fences != null && priceGoldBar100fences != null)
            {
                goldBar100fences.Price = Convert.ToDecimal(priceGoldBar100fences.SellPrice);
            }

            var goldBar50fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 50 fences");
            var priceGoldBar50fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar50fences != null && priceGoldBar50fences != null)
            {
                goldBar50fences.Price = Convert.ToDecimal((50 * priceGoldBar50fences.SellPrice) / 100);
            }

            var goldBar10fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 10 fences");
            var priceGoldBar10fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar10fences != null && priceGoldBar10fences != null)
            {
                goldBar10fences.Price = Convert.ToDecimal((10 * priceGoldBar10fences.SellPrice) / 100);
            }

            var gold24K50fences = productList.FirstOrDefault(c => c.ProductName == "24K Gold 50 fences");
            var pricegold24K50fences = goldPriceList.FirstOrDefault(p => p.Type == "24K Gold");

            if (gold24K50fences != null && pricegold24K50fences != null)
            {
                gold24K50fences.Price = Convert.ToDecimal((50 * pricegold24K50fences.SellPrice) / 100);
            }

            var gold18K50fences = productList.FirstOrDefault(c => c.ProductName == "18K Gold 50 fences");
            var pricegold18K50fences = goldPriceList.FirstOrDefault(p => p.Type == "18K Gold");

            if (gold18K50fences != null && pricegold18K50fences != null)
            {
                gold18K50fences.Price = Convert.ToDecimal((50 * pricegold18K50fences.SellPrice) / 100);
            }

            var gold14K50fences = productList.FirstOrDefault(c => c.ProductName == "14K Gold 50 fences");
            var pricegold14K50fences = goldPriceList.FirstOrDefault(p => p.Type == "14K Gold");

            if (gold14K50fences != null && pricegold14K50fences != null)
            {
                gold14K50fences.Price = Convert.ToDecimal((50 * pricegold14K50fences.SellPrice) / 100);
            }

            var gold10K50fences = productList.FirstOrDefault(c => c.ProductName == "10K Gold 50 fences");
            var pricegold10K50fences = goldPriceList.FirstOrDefault(p => p.Type == "10K Gold");

            if (gold10K50fences != null && pricegold10K50fences != null)
            {
                gold10K50fences.Price = Convert.ToDecimal((50 * pricegold10K50fences.SellPrice) / 100);
            }

            return productList;
        }

        public List<Product> SearchProduct(string searchValue)
        {
            var goldPriceList = _goldPriceService.GetGoldPrices();
            List<Product> productList = _productRepository.GetProductList().
                Where(c => c.ProductName.ToLower().Contains(searchValue.ToLower())).ToList();

            var goldBar100fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 100 fences");
            var priceGoldBar100fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar100fences != null && priceGoldBar100fences != null)
            {
                goldBar100fences.Price = Convert.ToDecimal(priceGoldBar100fences.SellPrice);
            }

            var goldBar50fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 50 fences");
            var priceGoldBar50fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar50fences != null && priceGoldBar50fences != null)
            {
                goldBar50fences.Price = Convert.ToDecimal((50 * priceGoldBar50fences.SellPrice) / 100);
            }

            var goldBar10fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 10 fences");
            var priceGoldBar10fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar10fences != null && priceGoldBar10fences != null)
            {
                goldBar10fences.Price = Convert.ToDecimal((10 * priceGoldBar10fences.SellPrice) / 100);
            }

            var gold24K50fences = productList.FirstOrDefault(c => c.ProductName == "24K Gold 50 fences");
            var pricegold24K50fences = goldPriceList.FirstOrDefault(p => p.Type == "24K Gold");

            if (gold24K50fences != null && pricegold24K50fences != null)
            {
                gold24K50fences.Price = Convert.ToDecimal((50 * pricegold24K50fences.SellPrice) / 100);
            }

            var gold18K50fences = productList.FirstOrDefault(c => c.ProductName == "18K Gold 50 fences");
            var pricegold18K50fences = goldPriceList.FirstOrDefault(p => p.Type == "18K Gold");

            if (gold18K50fences != null && pricegold18K50fences != null)
            {
                gold18K50fences.Price = Convert.ToDecimal((50 * pricegold18K50fences.SellPrice) / 100);
            }

            var gold14K50fences = productList.FirstOrDefault(c => c.ProductName == "14K Gold 50 fences");
            var pricegold14K50fences = goldPriceList.FirstOrDefault(p => p.Type == "14K Gold");

            if (gold14K50fences != null && pricegold14K50fences != null)
            {
                gold14K50fences.Price = Convert.ToDecimal((50 * pricegold14K50fences.SellPrice) / 100);
            }

            var gold10K50fences = productList.FirstOrDefault(c => c.ProductName == "10K Gold 50 fences");
            var pricegold10K50fences = goldPriceList.FirstOrDefault(p => p.Type == "10K Gold");

            if (gold10K50fences != null && pricegold10K50fences != null)
            {
                gold10K50fences.Price = Convert.ToDecimal((50 * pricegold10K50fences.SellPrice) / 100);
            }
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

                    var material = _materialService.GetMaterial(productDTO.MaterialDTOs[i].MaterialId);
                    material.AmountInStock -= decimal.Parse(productDTO.MaterialDTOs[i].MaterialSize);
                    _materialRepository.UpdateMaterial(material);
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
            foreach(var i in existingMaterial)
            {
                var material = _materialService.GetMaterial(i.MaterialId.ToString());
                material.AmountInStock += i.MaterialSize;
                _materialRepository.UpdateMaterial(material);
            }
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
                    var material = _materialService.GetMaterial(productDTO.MaterialDTOs[i].MaterialId);
                    material.AmountInStock -= decimal.Parse(productDTO.MaterialDTOs[i].MaterialSize);
                    _materialRepository.UpdateMaterial(material);
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
            var goldPriceList = _goldPriceService.GetGoldPrices();
            Guid.TryParse(productId, out Guid parseProductId);
            var product = _productRepository.GetById(parseProductId);
            if (product != null)
            {
                if(product.ProductName == "SJC Gold Bar 100 fences")
                {
                    var priceGoldBar100fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");
                    product.Price = Convert.ToDecimal(priceGoldBar100fences.SellPrice);
                }
                if (product.ProductName == "SJC Gold Bar 50 fences")
                {
                    var priceGoldBar50fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");
                    product.Price = Convert.ToDecimal((50 * priceGoldBar50fences.SellPrice) / 100);
                }
                if (product.ProductName == "SJC Gold Bar 10 fences")
                {
                    var priceGoldBar10fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");
                    product.Price = Convert.ToDecimal((10 * priceGoldBar10fences.SellPrice) / 100);
                }
                if (product.ProductName == "24K Gold 50 fences")
                {
                    var pricegold24K50fences = goldPriceList.FirstOrDefault(p => p.Type == "24K Gold");
                    product.Price = Convert.ToDecimal((50 * pricegold24K50fences.SellPrice) / 100);
                }
                if (product.ProductName == "18K Gold 50 fences")
                {
                    var pricegold18K50fences = goldPriceList.FirstOrDefault(p => p.Type == "18K Gold");
                    product.Price = Convert.ToDecimal((50 * pricegold18K50fences.SellPrice) / 100);
                }
                if (product.ProductName == "14K Gold 50 fences")
                {
                    var pricegold14K50fences = goldPriceList.FirstOrDefault(p => p.Type == "14K Gold");
                    product.Price = Convert.ToDecimal((50 * pricegold14K50fences.SellPrice) / 100);
                }

                if (product.ProductName == "10K Gold 50 fences")
                {
                    var pricegold10K50fences = goldPriceList.FirstOrDefault(p => p.Type == "10K Gold");
                    product.Price = Convert.ToDecimal((50 * pricegold10K50fences.SellPrice) / 100);
                }
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
                    var material = _materialService.GetMaterial(list[i].MaterialId);
                    if (list[i].MaterialId != null
                    && list[i]?.MaterialSize != "1"
                    && material.MaterialType.TypeName == "Diamond")
                    {
                        return new ResponseDTO("The size for diamond at material " + ++i + " must be 1", false);
                    }

                    if (list[i].MaterialId != null
                        && !regexSizeGold.IsMatch(list[i]?.MaterialSize)
                        && material.MaterialType.TypeName == "Gold")
                    {
                        return new ResponseDTO("The size for gold at material " + ++i + " must be number", false);
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

        public List<Product> SearchProductByStaff(string searchValue, string employeeId)
        {
            var goldPriceList = _goldPriceService.GetGoldPrices();
            var counterId = _employeeService.GetEmployee(employeeId).CounterId;
            List<Product> productList = _productRepository.GetProductList().
                Where(c => c.ProductName.ToLower().Contains(searchValue.ToLower()) && c.CounterId == counterId && c.AmountInStock > 0).ToList();

            var goldBar100fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 100 fences");
            var priceGoldBar100fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar100fences != null && priceGoldBar100fences != null)
            {
                goldBar100fences.Price = Convert.ToDecimal(priceGoldBar100fences.SellPrice);
            }

            var goldBar50fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 50 fences");
            var priceGoldBar50fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar50fences != null && priceGoldBar50fences != null)
            {
                goldBar50fences.Price = Convert.ToDecimal((50 * priceGoldBar50fences.SellPrice) / 100);
            }

            var goldBar10fences = productList.FirstOrDefault(c => c.ProductName == "SJC Gold Bar 10 fences");
            var priceGoldBar10fences = goldPriceList.FirstOrDefault(p => p.Type == "SJC Gold Bar");

            if (goldBar10fences != null && priceGoldBar10fences != null)
            {
                goldBar10fences.Price = Convert.ToDecimal((10 * priceGoldBar10fences.SellPrice) / 100);
            }

            var gold24K50fences = productList.FirstOrDefault(c => c.ProductName == "24K Gold 50 fences");
            var pricegold24K50fences = goldPriceList.FirstOrDefault(p => p.Type == "24K Gold");

            if (gold24K50fences != null && pricegold24K50fences != null)
            {
                gold24K50fences.Price = Convert.ToDecimal((50 * pricegold24K50fences.SellPrice) / 100);
            }

            var gold18K50fences = productList.FirstOrDefault(c => c.ProductName == "18K Gold 50 fences");
            var pricegold18K50fences = goldPriceList.FirstOrDefault(p => p.Type == "18K Gold");

            if (gold18K50fences != null && pricegold18K50fences != null)
            {
                gold18K50fences.Price = Convert.ToDecimal((50 * pricegold18K50fences.SellPrice) / 100);
            }

            var gold14K50fences = productList.FirstOrDefault(c => c.ProductName == "14K Gold 50 fences");
            var pricegold14K50fences = goldPriceList.FirstOrDefault(p => p.Type == "14K Gold");

            if (gold14K50fences != null && pricegold14K50fences != null)
            {
                gold14K50fences.Price = Convert.ToDecimal((50 * pricegold14K50fences.SellPrice) / 100);
            }

            var gold10K50fences = productList.FirstOrDefault(c => c.ProductName == "10K Gold 50 fences");
            var pricegold10K50fences = goldPriceList.FirstOrDefault(p => p.Type == "10K Gold");

            if (gold10K50fences != null && pricegold10K50fences != null)
            {
                gold10K50fences.Price = Convert.ToDecimal((50 * pricegold10K50fences.SellPrice) / 100);
            }
            return productList;
        }

        public decimal GetPriceProduct(List<MaterialDTO> list, decimal wage)
        {
            var priceRate = _priceRateRepository.GetPriceRate();
            decimal totalPriceMaterial = 0;
            foreach (var item in list) 
            {
                if(item.MaterialId != null)
                {
                    var material = _materialService.GetMaterial(item.MaterialId);
                    totalPriceMaterial += (decimal.Parse(material.Price.ToString()) * decimal.Parse(item.MaterialSize.ToString()));
                }
            }
            return (totalPriceMaterial + wage) * decimal.Parse(priceRate.ToString());
            
        }

        public ResponseDTO CheckMaterialAmountInStock(ProductRequestDTO productDTO)
        {
            foreach(var i in productDTO.MaterialDTOs)
            {
                if (i.MaterialId != null)
                {
                    var material = _materialService.GetMaterial(i.MaterialId);
                    if(material.AmountInStock < decimal.Parse(i.MaterialSize))
                    {
                        return new ResponseDTO(material.MaterialName + " only " + material.AmountInStock + " left in stock", false);
                    }
                }

            }
            return new ResponseDTO("Check Ok", true);
        }
    }
}
