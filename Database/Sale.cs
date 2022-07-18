namespace ZHFS.Database;
public class Sale
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; } //тут кстати есть вопрос. думаем ли мы о повышении цены в будущем и как это влияет на статистику
    public User User { get; set; }
    public Product Product { get; set; }
}
