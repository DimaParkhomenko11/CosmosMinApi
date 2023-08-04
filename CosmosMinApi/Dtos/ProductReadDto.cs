namespace CosmosMinApi.Dtos;

public class ProductReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
}