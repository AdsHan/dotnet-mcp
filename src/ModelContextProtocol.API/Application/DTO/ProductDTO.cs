namespace ModelContextProtocol.API.Application.DTO;

public class ProductDTO
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public List<int> ColorIds { get; set; } = new();
}