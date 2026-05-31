namespace ShoesApp.Models;

public class Order
{
    public int Id  { get; set; }
    public int Number { get; set; }
    public DateTime DateOrder { get; set; }
    public DateTime DateDelivery { get; set; }
    public int PickupPointId { get; set; }
    public int UserId { get; set; }
    public int Code { get; set; }
    
    public PickupPoint PickupPoint { get; set; }
    public User User { get; set; }
    public List<OrderProduct> OrdersProducts { get; set; }
}