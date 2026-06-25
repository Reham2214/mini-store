using System.ComponentModel.DataAnnotations.Schema;

namespace mini_store.Models;
public class Product
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public decimal Price {get; set;}
    public string? Image {get; set;}

    public int CategoryId {get; set;}

    [ForeignKey("CategoryId")]
    public virtual Category Categories {get; set;}
}
