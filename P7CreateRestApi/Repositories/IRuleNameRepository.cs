using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public interface IRuleNameRepository
{
    Task<List<RuleName>> GetAsync();
    Task<RuleName?> GetByIdAsync(int id);
    Task AddAsync(RuleName ruleName);
    Task DeleteAsync(RuleName ruleName);
    Task<int> UpdateAsync();
}