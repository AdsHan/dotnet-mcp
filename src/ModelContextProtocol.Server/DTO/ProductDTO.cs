namespace ModelContextProtocol.Server.DTO;

public record ProductDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public int Quantity { get; init; }
    public List<int> ColorIds { get; init; } = new();
}
