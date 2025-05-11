using PaymentIntegration.Data.Domain;
using PaymentIntegration.Data;
using PaymentIntegration.DataAccess.Interfaces;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.DataAccess.Services
{
    public class BalanceDbService : IBalanceDbService
    {
        private readonly AppDbContext _context;

        public BalanceDbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UpdatedBalanceResponse dto, string orderId)
        {
            Balance balance = new()
            {
                OrderId = orderId,
                UserId = dto.UserId,
                TotalBalance = dto.TotalBalance,
                AvailableBalance = dto.AvailableBalance,
                BlockedBalance = dto.BlockedBalance,
                Currency = dto.Currency,
                LastUpdated = dto.LastUpdated
            };

            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdatedBalanceResponse dto, string orderId)
        {
            var balance = await _context.Balances.FindAsync(orderId);
            if (balance is null) return;

            balance.UserId = dto.UserId;
            balance.TotalBalance = dto.TotalBalance;
            balance.AvailableBalance = dto.AvailableBalance;
            balance.BlockedBalance = dto.BlockedBalance;
            balance.Currency = dto.Currency;
            balance.LastUpdated = dto.LastUpdated;

            _context.Balances.Update(balance);
            await _context.SaveChangesAsync();
        }

        public async Task<UpdatedBalanceResponse?> GetByOrderIdAsync(string orderId)
        {
            var balance = await _context.Balances.FindAsync(orderId);
            if (balance is null) return null;

            return new UpdatedBalanceResponse
            {
                UserId = balance.UserId,
                TotalBalance = balance.TotalBalance,
                AvailableBalance = balance.AvailableBalance,
                BlockedBalance = balance.BlockedBalance,
                Currency = balance.Currency,
                LastUpdated = balance.LastUpdated
            };
        }
    }
}
