using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.DataAccess.Interfaces
{
    public interface IOrderDbService
    {
        Task AddAsync(OrderResponse dto);
        Task UpdateAsync(OrderResponse dto);
        Task<OrderResponse?> GetByIdAsync(string orderId);
    }
}
