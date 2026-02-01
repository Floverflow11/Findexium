using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public class CurvePointRepository : ICurvePointRepository
{
    private readonly LocalDbContext _dbContext;

    public CurvePointRepository(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CurvePoint>> GetAsync()
    {
        return await _dbContext.CurvePoints.ToListAsync();
    }

    public async Task<CurvePoint?> GetByIdAsync(int id)
    {
        return await _dbContext.CurvePoints.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(CurvePoint curvePoint)
    {
        _dbContext.CurvePoints.Add(curvePoint);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(CurvePoint curvePoint)
    {
        _dbContext.CurvePoints.Remove(curvePoint);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}