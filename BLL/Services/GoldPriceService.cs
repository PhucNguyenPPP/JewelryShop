using BLL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async Task<string> FetchXmlFromApi()
        {
            var response = await _httpClient.GetAsync("https://sjc.com.vn/xml/tygiavang.xml");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<GoldPriceDTO>> GetGoldPrices()
        {
            var goldPrices = new List<GoldPriceDTO>();
            string xml = await FetchXmlFromApi();
            var doc = XDocument.Parse(xml);
            var count = 1;
            foreach (var item in doc.Descendants("item"))
            {
                if (count == 1)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "SJC Gold Bar",
                        BuyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000,
                        SellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 5)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "24K Gold",
                        BuyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000,
                        SellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 6)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "18K Gold",
                        BuyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000,
                        SellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000
                    };
                    goldPrices.Add(goldPrice);

                }
                if (count == 7)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "14K Gold",
                        BuyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000,
                        SellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000
                    };
                    goldPrices.Add(goldPrice);
                }
                if (count == 8)
                {
                    var goldPrice = new GoldPriceDTO
                    {
                        Type = "10K Gold",
                        BuyPrice = float.Parse(item.Attribute("buy").Value.Replace(".", "")) * 1000,
                        SellPrice = float.Parse(item.Attribute("sell").Value.Replace(".", "")) * 1000
                    };
                    goldPrices.Add(goldPrice);
                }
                count ++;   
            }

            return goldPrices;
        }
    }
}
