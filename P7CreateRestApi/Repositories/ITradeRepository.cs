using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public interface ITradeRepository
{
    Task<List<Trade>> GetAsync();
    Task<Trade?> GetByIdAsync(int id);
    Task AddAsync(Trade trade);
    Task DeleteAsync(Trade trade);
    Task<int> UpdateAsync();
}