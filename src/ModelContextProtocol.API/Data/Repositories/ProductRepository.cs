using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.API.Data.Entities;
using ModelContextProtocol.API.Data.Enums;

namespace ModelContextProtocol.API.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        return await _dbContext.Products
            .Include(p => p.Colors)
            .Where(a => a.Status == EntityStatusEnum.Active)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductModel> GetByIdAsync(int id)
    {
        return await _dbContext.Products
            .Include(p => p.Colors)
            .Where(a => a.Status == EntityStatusEnum.Active)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<ProductModel>> SearchByTermAsync(string searchTerm)
    {
        var terms = searchTerm
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var query = _dbContext.Products
            .Include(p => p.Colors)
            .Where(p => p.Status == EntityStatusEnum.Active);

        foreach (var term in terms)
        {
            var temp = term;
            query = query.Where(p =>
                EF.Functions.Like(p.Title.ToLower(), $"%{temp}%") ||
                EF.Functions.Like(p.Description.ToLower(), $"%{temp}%") ||
                p.Colors.Any(c => EF.Functions.Like(c.Name.ToLower(), $"%{temp}%"))
            );
        }

        return await query
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<ColorModel>> GetColorsByIdsAsync(List<int> colorIds)
    {
        return await _dbContext.Colors
            .Where(c => colorIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(ProductModel product)
    {
        // Reforço que a entidade foi alterada
        _dbContext.Entry(product).State = EntityState.Modified;
        _dbContext.Update(product);
    }

    public async Task AddAsync(ProductModel product)
    {
        _dbContext.Add(product);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

}
