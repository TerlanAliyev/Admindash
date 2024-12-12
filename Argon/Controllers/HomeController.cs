using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using Argon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        // Login i�lemi
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var loginRequest = new
            {
                UserName = username,
                UserPassword = password
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), System.Text.Encoding.UTF8, "application/json");

            // API'ye login iste�i g�nder
            var response = await _httpClient.PostAsync("https://localhost:44314/api/user/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Token'� al
                var token = await response.Content.ReadAsStringAsync();

                // Token'� session'a kaydet
                HttpContext.Session.SetString("jwtToken", token);

                // API'den korumal� veriyi al
                return RedirectToAction("ProtectedData");
            }

            ViewData["ErrorMessage"] = "Invalid credentials";
            return View();
        }

        // Korumal� veriyi almak i�in API'ye GET iste�i g�nder
        public async Task<IActionResult> ProtectedData()
        {
            var token = HttpContext.Session.GetString("jwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44314/api/protected-resource");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                ViewData["ProtectedData"] = data;
                return View();
            }

            return Unauthorized();
        }




        // Kategori say�s�n� almak i�in API'den GET iste�i
        private async Task<int> GetCategoryCount()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/category/count");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var categoryCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("categoryCount").GetInt32();
                return categoryCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }

        // Kategori say�s�n� almak i�in API'den GET iste�i
        private async Task<int> GetCategoryCountByLang1()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/category/count/1");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var categoryCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("categoryCount").GetInt32();
                return categoryCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }
        private async Task<int> GetCategoryCountByLang2()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/category/count/2");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var categoryCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("categoryCount").GetInt32();
                return categoryCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }
        private async Task<int> GetCategoryCountByLang3()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/category/count/3");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var categoryCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("categoryCount").GetInt32();
                return categoryCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }





        // Kategoriye aid xeberler almak i�in API'den GET iste�i
        private async Task<List<CategoryNews>> GetCategoryNews()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/statistic/CategoryNewsCount");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                // JSON verisini listeye deserialize et
                var categoryNewsList = JsonSerializer.Deserialize<List<CategoryNews>>(data);

                return categoryNewsList;  // Listeyi d�nd�r
            }

            return new List<CategoryNews>();  // E�er hata olu�ursa bo� bir liste d�nd�r
        }









        // News say�s�n� almak i�in API'den GET iste�i
        private async Task<int> GetNewsCount()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/news/count");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var newsCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("newsCount").GetInt32();
                return newsCount;
            }
            return 0; 
        }

        // Pdf say� almaq ucun API'dan GET 
        private async Task<int> GetPdfCount()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/newspaper/count");
            if (response.IsSuccessStatusCode)
            {
                var data= await response.Content.ReadAsStringAsync();
                var pdfCount=JsonSerializer.Deserialize<JsonElement>(data).GetProperty("newsPaperCount").GetInt32();
                return pdfCount;
            }
            return 0;

        }

        private async Task<int> GetInfogCount()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/%C4%B0nfographics/count");
            if (response.IsSuccessStatusCode)
            {
                var data= await response.Content.ReadAsStringAsync();
                var infcount =JsonSerializer.Deserialize<JsonElement>(data).GetProperty("infs").GetInt32(); 
                return infcount;
            }
            return 0;
        }



        private async Task<int> GetNewsCountByLang1()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/news/count/1");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var NewsCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("newsCount").GetInt32();
                return NewsCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }
        private async Task<int> GetNewsCountByLang2()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/news/count/2");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var NewsCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("newsCount").GetInt32();
                return NewsCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }

        private async Task<int> GetNewsCountByLang3()
        {
            var response = await _httpClient.GetAsync("https://localhost:44314/api/news/count/3");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var NewsCount = JsonSerializer.Deserialize<JsonElement>(data).GetProperty("newsCount").GetInt32();
                return NewsCount;
            }
            return 0; // E�er hata olu�ursa 0 d�nd�r
        }




        //  view-a gonder 
        public async Task<IActionResult> Index()
        {
            var categoryCount = await GetCategoryCount();
            var newsCount = await GetNewsCount();
            var pdfCount = await GetPdfCount();
            var infcount =await GetInfogCount();

            ViewData["CategoryCount"] = categoryCount;
            ViewData["NewsCount"] = newsCount;
            ViewData["Pdfcount"] = pdfCount;
            ViewData["infcount"] = infcount;

            return View();
        }


        public async Task<IActionResult> Categories()
        {
            var categoryCount = await GetCategoryCount();
            var categoryNewsList = await GetCategoryNews();
            var newsCountAze = await GetCategoryCountByLang1();
            var newsCountEng = await GetCategoryCountByLang2();
            var newsCountRus = await GetCategoryCountByLang3();



            // Kategori say�s�n� ViewData'ya g�nder
            ViewData["CategoryCount"] = categoryCount;
            ViewData["CategoryNewsList"] = categoryNewsList;
            ViewData["CategoryCountAze"] = newsCountAze;
            ViewData["CategoryCountEng"] = newsCountEng;
            ViewData["CategoryCountRus"] = newsCountRus;


            return View();
        }
        public async Task<IActionResult> News()
        {
            var categoryNewsList = await GetCategoryNews();
            var newsCountAze = await GetNewsCountByLang1();
            var newsCountEng = await GetNewsCountByLang2();
            var newsCountRus = await GetNewsCountByLang3();



            // Kategori say�s�n� ViewData'ya g�nder
            ViewData["CategoryNewsList"] = categoryNewsList;
            ViewData["NewsCountAze"] = newsCountAze;
            ViewData["NewsCountEng"] = newsCountEng;
            ViewData["NewsCountRus"] = newsCountRus;


            return View();
        }









        // Di�er aksiyonlar
        public IActionResult tables() => View();
        public IActionResult PostNews() => View();


        public IActionResult login() => View();

        public IActionResult profile() => View();
        public IActionResult signUp() => View();
        public IActionResult PostCategory() => View();
        public IActionResult Privacy() => View();




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
