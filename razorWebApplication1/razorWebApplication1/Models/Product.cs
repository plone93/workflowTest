using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace razorWebApplication1.Models
{

    [Table("PRODUCT")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Column("PRICE")]
        public decimal Price { get; set; }

        public static implicit operator List<object>(Product v)
        {
            throw new NotImplementedException();
        }
    }
}
