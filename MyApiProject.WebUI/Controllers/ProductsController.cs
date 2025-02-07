using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApiProject.WebUI.Dtos;
using Newtonsoft.Json;
using System.Text;

namespace MyApiProject.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7118/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7118/api/Category");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> values2 =(from x in values
                                          select new SelectListItem
                                          {
                                              Text = x.categoryName,
                                              Value = x.categoryId.ToString()
                                          }).ToList();
            ViewBag.v=values2;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent= new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7118/api/Product", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7118/api/Product?id=/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7118/api/Category");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> values2 = (from x in values
                                            select new SelectListItem
                                            {
                                                Text = x.categoryName,
                                                Value = x.categoryId.ToString()
                                            }).ToList();
            ViewBag.v = values2;
            var client2 = _httpClientFactory.CreateClient();
            var response2 = await client2.GetAsync("https://localhost:7118/api/Product/GetProduct?id=/" + id);
            if (response2.IsSuccessStatusCode)
            {
                var jsonData2 = await response2.Content.ReadAsStringAsync();
                var updatedValues = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData2);
                return View(updatedValues);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7118/api/Product/", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }
    }
}
