using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.DataAccess.Interfaces
{
    public interface IProductDbService
    {
        Task AddOrUpdateAsync(ProductListResponse dto);
        Task<List<ProductListResponse>> GetAllAsync();
    }
}
