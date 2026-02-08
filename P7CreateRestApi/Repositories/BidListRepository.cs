using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public class BidListRepository : IBidListRepository
{
    private readonly LocalDbContext _dbContext;

    public BidListRepository(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BidList>> GetAsync()
    {
        return await _dbContext.BidLists.ToListAsync();
    }

    public async Task<BidList?> GetByIdAsync(int id)
    {
        return await _dbContext.BidLists.SingleOrDefaultAsync(b => b.BidListId == id);
    }

    public async Task AddAsync(BidList bidList)
    {
        _dbContext.BidLists.Add(bidList);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(BidList bidList)
    {
        _dbContext.BidLists.Remove(bidList);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}