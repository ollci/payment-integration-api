using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntegration.Helper.Dtos.Responses;

namespace BalanceManagementAPI.Interfaces
{
    public interface IBalanceManagementService
    {
        Task<List<ProductListResponse>> GetProductsAsync();
        Task<PreOrderResponse?> PreOrderAsync(string orderId, decimal amount);
        Task<CompleteOrderResponse?> CompleteOrderAsync(string orderId);
    }
}
