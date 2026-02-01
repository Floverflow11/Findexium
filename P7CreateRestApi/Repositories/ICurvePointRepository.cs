using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories;

public interface ICurvePointRepository
{
    Task<List<CurvePoint>> GetAsync();
    Task<CurvePoint?> GetByIdAsync(int id);
    Task AddAsync(CurvePoint curvePoint);
    Task DeleteAsync(CurvePoint curvePoint);
    Task<int> UpdateAsync();
}