using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.DataAccess.Interfaces
{
    public interface IBalanceDbService
    {
        Task AddAsync(UpdatedBalanceResponse dto, string orderId);
        Task UpdateAsync(UpdatedBalanceResponse dto, string orderId);
        Task<UpdatedBalanceResponse?> GetByOrderIdAsync(string orderId);
    }
}
