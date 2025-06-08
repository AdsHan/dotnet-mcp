using ModelContextProtocol.API.Data.Entities;
using ModelContextProtocol.API.Domain.DomainObjects;

namespace ModelContextProtocol.API.Data.Repositories;

public interface IProductRepository : IRepository<ProductModel>
{
    Task<List<ProductModel>> GetAllAsync();
    Task<ProductModel> GetByIdAsync(int id);
    Task AddAsync(ProductModel product);
    Task UpdateAsync(ProductModel product);
    Task<List<ProductModel>> SearchByTermAsync(string searchTerm);
    Task<List<ColorModel>> GetColorsByIdsAsync(List<int> colorIds);
}