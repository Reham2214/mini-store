using System.ComponentModel.DataAnnotations.Schema;

namespace mini_store.Models;
public class Images
{
        public int Id {get ; set;}

        public string Name { get; set; }

        public int ProductId { get ; set ;}

        [ForeignKey("ProductId")]

        public virtual  Product Products {get ; set ;}
}