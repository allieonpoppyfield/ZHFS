namespace ZHFS.Database;
public class Sale
{
    public int Id { get; set; }
    public User? User { get; set; }
    public List<SaleItem> SaleItems { get; set; }
    public DateTime CreatedDt { get; set; }
}
