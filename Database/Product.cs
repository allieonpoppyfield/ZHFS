using System.ComponentModel.DataAnnotations;

namespace ZHFS.Database;
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<Sale>? Sales { get; set; }
}
