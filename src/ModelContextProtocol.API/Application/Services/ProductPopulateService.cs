using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.API.Data;
using ModelContextProtocol.API.Data.Entities;
using ModelContextProtocol.API.Data.Enums;

namespace ModelContextProtocol.API.Application.Services
{
    public class ProductPopulateService
    {
        private readonly CatalogDbContext _dbContext;

        public ProductPopulateService(CatalogDbContext context)
        {
            _dbContext = context;
        }

        public async Task Initialize()
        {
            if (_dbContext.Database.EnsureCreated())
            {
                var random = new Random();

                if (!await _dbContext.Colors.AnyAsync())
                {
                    var commonColors = new List<ColorModel>
                    {
                        new ColorModel { Name = "Preto", HexCode = "#000000" },
                        new ColorModel { Name = "Branco", HexCode = "#FFFFFF" },
                        new ColorModel { Name = "Marrom", HexCode = "#8B4513" },
                        new ColorModel { Name = "Bege", HexCode = "#F5F5DC" },
                        new ColorModel { Name = "Azul", HexCode = "#0000FF" },
                        new ColorModel { Name = "Cinza", HexCode = "#808080" },
                        new ColorModel { Name = "Vermelho", HexCode = "#FF0000" },
                        new ColorModel { Name = "Rosa", HexCode = "#FFC0CB" },
                        new ColorModel { Name = "Verde", HexCode = "#008000" },
                        new ColorModel { Name = "Dourado", HexCode = "#FFD700" }
                    };

                    _dbContext.Colors.AddRange(commonColors);
                    await _dbContext.SaveChangesAsync();
                }

                var colors = await _dbContext.Colors.ToListAsync();

                var shoeTypes = new List<string>
                {
                    "Sandália", "Bota", "Scarpin", "Chinelo", "Tênis", "Mocassim", "Rasteirinha", "Sapatilha", "Tamanco", "Oxford"
                };

                // 3. Geração dos produtos
                for (int i = 1; i <= 100; i++)
                {
                    var shoeType = shoeTypes[random.Next(shoeTypes.Count)];
                    int colorCount = random.Next(1, 4); // entre 1 e 3 cores
                    var selectedColors = colors.OrderBy(_ => random.Next()).Take(colorCount).ToList();

                    var product = new ProductModel
                    {
                        Title = $"{shoeType} - {i}",
                        Description = $"{shoeType} confortável modelo {i}",
                        Price = Math.Round(random.NextDouble() * (300 - 50) + 50, 2),
                        Quantity = random.Next(10, 200),
                        Status = random.Next(1, 3) == 1 ? EntityStatusEnum.Active : EntityStatusEnum.Inactive,
                        Colors = selectedColors
                    };

                    _dbContext.Products.Add(product);
                }

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
