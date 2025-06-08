using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Server.DTO;
using System.Net;
using System.Net.Http.Headers;

namespace ModelContextProtocol.Server.Integration;

public class ProductService : HttpService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<ProductDTO>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("/api/products");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return new List<ProductDTO>();

        return await DeserializarObjectResponse<List<ProductDTO>>(response);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/products/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        return await DeserializarObjectResponse<ProductDTO>(response);
    }

    public async Task<List<ProductDTO>> SearchProductsAsync(string query)
    {
        var response = await _httpClient.GetAsync($"/api/products/search?query={Uri.EscapeDataString(query)}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return new List<ProductDTO>();

        return await DeserializarObjectResponse<List<ProductDTO>>(response);
    }

    public async Task<bool> AddProductAsync(ProductDTO product)
    {
        var content = GetContent(product);

        var response = await _httpClient.PostAsync("/api/products", content);

        return HandleErrorResponse(response);
    }
}

