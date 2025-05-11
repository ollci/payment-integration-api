using PaymentIntegration.DataAccess.Interfaces;
using PaymentIntegration.Helper.Dtos.Responses;
using Microsoft.EntityFrameworkCore;
using PaymentIntegration.Data;
using PaymentIntegration.Data.Domain;

namespace PaymentIntegration.DataAccess.Services
{
    public class ProductDbService : IProductDbService
    {
        private readonly AppDbContext _context;

        public ProductDbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateAsync(ProductListResponse dto)
        {
            var existing = await _context.Products.FindAsync(dto.Id);

            if (existing is null)
            {
                var product = new Product
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Currency = dto.Currency,
                    Category = dto.Category,
                    Stock = dto.Stock
                };

                await _context.Products.AddAsync(product);
            }
            else
            {
                existing.Name = dto.Name;
                existing.Description = dto.Description;
                existing.Price = dto.Price;
                existing.Currency = dto.Currency;
                existing.Category = dto.Category;
                existing.Stock = dto.Stock;

                _context.Products.Update(existing);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductListResponse>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductListResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Currency = p.Currency,
                    Category = p.Category,
                    Stock = p.Stock
                })
                .ToListAsync();
        }
    }
}
