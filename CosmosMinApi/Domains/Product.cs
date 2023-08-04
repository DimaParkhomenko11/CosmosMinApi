using System.ComponentModel.DataAnnotations;

namespace CosmosMinApi.Domains;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;
    
    [Required]
    public double Price { get; set; }
}