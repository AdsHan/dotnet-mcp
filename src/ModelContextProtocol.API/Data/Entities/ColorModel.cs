using ModelContextProtocol.API.Domain.DomainObjects;

namespace ModelContextProtocol.API.Data.Entities;

public class ColorModel : BaseEntity
{
    // EF constructor
    public ColorModel() { }

    public string Name { get; set; }
    public string HexCode { get; set; }
}
