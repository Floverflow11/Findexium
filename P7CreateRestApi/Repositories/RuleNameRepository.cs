using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public class RuleNameRepository : IRuleNameRepository
{
    private readonly LocalDbContext _dbContext;

    public RuleNameRepository(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RuleName>> GetAsync()
    {
        return await _dbContext.RuleNames.ToListAsync();
    }

    public async Task<RuleName?> GetByIdAsync(int id)
    {
        return await _dbContext.RuleNames.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(RuleName ruleName)
    {
        _dbContext.RuleNames.Add(ruleName);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RuleName ruleName)
    {
        _dbContext.RuleNames.Remove(ruleName);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}