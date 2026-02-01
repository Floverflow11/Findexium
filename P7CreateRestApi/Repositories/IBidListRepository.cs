using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public interface IBidListRepository
{
    Task<BidList?> GetByIdAsync(int id);
    Task AddAsync(BidList bidList);
    Task DeleteAsync(BidList bidList);
    Task<int> UpdateAsync();
}