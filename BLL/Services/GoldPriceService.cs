using BLL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace BLL.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        private readonly HttpClient _httpClient;
        public GoldPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private string FetchXmlFromApi()
        {
            var response = _httpClient.GetAsync("https://sjc.com.vn/xml/tygiavang.xml").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }

        public List<GoldPriceDTO> GetGoldPrices()
        {
            var goldPrices = new List<GoldPriceDTO>();
            string xml = FetchXmlFromApi();
            var doc = XDocument.Parse(xml);
            var count = 1;
            foreach (var item in doc.Descendants("item"))
            {
                var buyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000;
                var sellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000;

                if (count == 1)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "SJC Gold Bar",
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 5)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "24K Gold",
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 6)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "18K Gold",
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 7)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "14K Gold",
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 8)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "10K Gold",
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice
                    };
                    goldPrices.Add(goldPrice);
                }
                count++;
            }

            return goldPrices;
        }
    }
}
