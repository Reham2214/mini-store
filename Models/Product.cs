using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_store.Models;
public class Product
{
    public int Id {get; set;}

    [Required(ErrorMessage ="يجب أدخال أسم المنتج")]
    public required string Name {get; set;}

    
    [Range(10, 1000)]
    [Required(ErrorMessage ="يجب أدخال سعر المنتج")]
    public decimal Price {get; set;}
    public string? Image {get; set;}

    [NotMapped]
    [Required(ErrorMessage ="يجب رفع الملف")]
    public IFormFile ImageFile {get; set;}

    public int CategoryId {get; set;}

    [ForeignKey("CategoryId")]
    public virtual Category? Categories {get; set;}
}
