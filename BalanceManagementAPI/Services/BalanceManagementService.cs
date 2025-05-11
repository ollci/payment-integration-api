using System.Net.Http.Json;
using BalanceManagementAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using PaymentIntegration.Helper.Dtos.Responses;

namespace BalanceManagementAPI.Services
{
    public class BalanceManagementService : IBalanceManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public BalanceManagementService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["BalanceApi:BaseUrl"] ?? string.Empty;
        }

        public async Task<List<ProductListResponse>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductListResponse>>>($"{_baseUrl}/api/products");
                return response?.Data ?? [];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ürünler çekilirken hata oluştu.");
                return [];
            }
        }

        public async Task<PreOrderResponse?> PreOrderAsync(string orderId, decimal amount)
        {
            try
            {
                var request = new { orderId, amount };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/balance/preorder", request);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("PreOrder isteği başarısız oldu. StatusCode: {Status}", response.StatusCode);
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PreOrderResponse>>();
                return result?.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PreOrder isteğinde hata oluştu. OrderId: {OrderId}", orderId);
                return null;
            }
        }

        public async Task<CompleteOrderResponse?> CompleteOrderAsync(string orderId)
        {
            try
            {
                var request = new { orderId };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/balance/complete", request);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("CompleteOrder isteği başarısız oldu. StatusCode: {Status}", response.StatusCode);
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CompleteOrderResponse>>();
                return result?.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CompleteOrder isteğinde hata oluştu. OrderId: {OrderId}", orderId);
                return null;
            }
        }
    }
}
