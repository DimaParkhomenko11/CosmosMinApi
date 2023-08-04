using System.ComponentModel.DataAnnotations;

namespace CosmosMinApi.Dtos;

public class ProductCreateDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;
    
    [Required]
    public double Price { get; set; }
}