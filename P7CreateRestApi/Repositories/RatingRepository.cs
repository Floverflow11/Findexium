using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly LocalDbContext _dbContext;

    public RatingRepository(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Rating>> GetAsync()
    {
        return await _dbContext.Ratings.ToListAsync();
    }

    public async Task<Rating?> GetByIdAsync(int id)
    {
        return await _dbContext.Ratings.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Rating rating)
    {
        _dbContext.Ratings.Add(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Rating rating)
    {
        _dbContext.Ratings.Remove(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}