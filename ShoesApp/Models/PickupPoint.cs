namespace ShoesApp.Models;

public class PickupPoint
{
    public int Id  { get; set; }
    public string Name { get; set; }
    
    public List<Order> Orders { get; set; }
}