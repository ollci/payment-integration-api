using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentIntegration.Data.Domain;
using PaymentIntegration.Data;
using PaymentIntegration.Helper.Dtos.Responses;
using PaymentIntegration.DataAccess.Interfaces;

namespace PaymentIntegration.DataAccess.Services
{
    public class OrderDbService : IOrderDbService
    {
        private readonly AppDbContext _context;

        public OrderDbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderResponse dto)
        {
            var entity = new Order
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                Status = dto.Status,
                Timestamp = dto.Timestamp,
                CompletedAt = dto.CompletedAt,
                CancelledAt = dto.CancelledAt
            };

            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderResponse dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);

            if (order is null)
                return;

            order.Status = dto.Status;
            order.CompletedAt = dto.CompletedAt;
            order.CancelledAt = dto.CancelledAt;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderResponse?> GetByIdAsync(string orderId)
        {
            var entity = await _context.Orders.FindAsync(orderId);

            if (entity is null)
                return null;

            return new OrderResponse
            {
                OrderId = entity.OrderId,
                Amount = entity.Amount,
                Status = entity.Status,
                Timestamp = entity.Timestamp,
                CompletedAt = entity.CompletedAt,
                CancelledAt = entity.CancelledAt
            };
        }
    }
}
