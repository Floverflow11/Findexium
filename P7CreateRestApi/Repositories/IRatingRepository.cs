using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public interface IRatingRepository
{
    Task<List<Rating>> GetAsync();
    Task<Rating?> GetByIdAsync(int id);
    Task AddAsync(Rating rating);
    Task DeleteAsync(Rating rating);
    Task<int> UpdateAsync();
}