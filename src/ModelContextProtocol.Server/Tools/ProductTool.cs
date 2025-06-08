using ModelContextProtocol.Server.DTO;
using ModelContextProtocol.Server.Integration;
using System.ComponentModel;
using System.Globalization;

namespace ModelContextProtocol.Server.Tools;

[McpServerToolType]
public static class ProductTool
{

    [McpServerTool, Description("Lista todos os produtos ativos da API, com filtros opcionais.")]
    public static async Task<List<string>> ListarProdutos(ProductService service, double? precoMin = null, double? precoMax = null, int? quantidadeMin = null)
    {
        var produtos = await service.GetAllProductsAsync();

        var filtrados = produtos
            .Where(p =>
                (!precoMin.HasValue || p.Price >= precoMin) &&
                (!precoMax.HasValue || p.Price <= precoMax) &&
                (!quantidadeMin.HasValue || p.Quantity >= quantidadeMin))
            .ToList();

        return filtrados.Any()
            ? filtrados.Select(FormatProduct).ToList()
            : new List<string> { "Nenhum produto encontrado com os filtros aplicados." };
    }

    [McpServerTool, Description("Lista os produtos com ordenação por nome, preço ou quantidade.")]
    public static async Task<List<string>> ListarOrdenado(ProductService service, string ordenarPor = "nome")
    {
        var produtos = await service.GetAllProductsAsync();

        var ordenados = ordenarPor.ToLower() switch
        {
            "preco" => produtos.OrderBy(p => p.Price),
            "quantidade" => produtos.OrderByDescending(p => p.Quantity),
            _ => produtos.OrderBy(p => p.Title)
        };

        return ordenados.Any()
            ? ordenados.Select(FormatProduct).ToList()
            : new List<string> { "Nenhum produto encontrado para ordenar." };
    }

    [McpServerTool, Description("Busca produtos que contenham o termo no título, descrição ou cor.")]
    public static async Task<List<string>> BuscarProduto(ProductService service, string termo)
    {
        var produtos = await service.SearchProductsAsync(termo);

        return produtos.Any()
            ? produtos.Select(FormatProduct).ToList()
            : new List<string> { $"Nenhum produto encontrado com o termo \"{termo}\"." };
    }

    [McpServerTool, Description("Adiciona um novo produto via API.")]
    public static async Task<string> AdicionarProduto(ProductService service, string titulo, string descricao, double preco, int quantidade)
    {
        var novoProduto = new ProductDTO
        {
            Title = titulo,
            Description = descricao,
            Price = preco,
            Quantity = quantidade,
            ColorIds = new List<int>()
        };

        var sucesso = await service.AddProductAsync(novoProduto);

        return sucesso
            ? $"✅ Produto \"{titulo}\" adicionado com sucesso!"
            : "❌ Erro ao adicionar produto.";
    }

    private static string FormatProduct(ProductDTO p)
    {
        var preco = p.Price.ToString("C", new CultureInfo("pt-BR"));
        return $"🛒 {p.Title}\n📄 {p.Description}\n💰 {preco}\n📦 Quantidade disponível: {p.Quantity}\n";
    }
}
