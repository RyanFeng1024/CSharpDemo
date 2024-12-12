using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RestfulClient
{
    public class ProductsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductsApiClient(ApiSettings settings)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(settings.BaseUrl) };

            // 设置Basic认证
            var credentials = Encoding.UTF8.GetBytes($"{settings.Username}:{settings.Password}");
            var base64Credentials = Convert.ToBase64String(credentials);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Product>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/products");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetAll失败: {response.StatusCode}");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Product>>(content, _jsonOptions);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetById失败: {response.StatusCode}");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(content, _jsonOptions);
        }

        public async Task<List<Product>?> SearchByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/products/search?name={Uri.EscapeDataString(name)}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"SearchByName失败: {response.StatusCode}");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Product>>(content, _jsonOptions);
        }

        public async Task<Product?> AddAsync(Product product)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/products", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Add失败: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(content, _jsonOptions);
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/products/{product.Id}", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Update失败: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Product>(content, _jsonOptions);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Delete失败: {response.StatusCode}");
                return false;
            }
            return true;
        }
    }
}
