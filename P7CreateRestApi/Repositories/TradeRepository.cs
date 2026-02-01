using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public class TradeRepository : ITradeRepository
{
    private readonly LocalDbContext _dbContext;

    public TradeRepository(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Trade>> GetAsync()
    {
        return await _dbContext.Trades.ToListAsync();
    }

    public async Task<Trade?> GetByIdAsync(int id)
    {
        return await _dbContext.Trades.SingleOrDefaultAsync(t => t.TradeId == id);
    }

    public async Task AddAsync(Trade trade)
    {
        _dbContext.Trades.Add(trade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Trade trade)
    {
        _dbContext.Trades.Remove(trade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}