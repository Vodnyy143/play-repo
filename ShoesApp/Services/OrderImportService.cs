using ClosedXML.Excel;
using ShoesApp.Models;

namespace ShoesApp.Services;

public class OrderImportService
{
    private readonly AppDbContext _db = new();
    
    public void Import(string filePath)
    {
        var workbook = new XLWorkbook(filePath);
        
        var worksheet =  workbook.Worksheet(1);
        
        var rows =  worksheet.RowsUsed().Skip(1);

        foreach (var row in rows)
        {
            var number =  row.Cell(1).GetValue<int>();
            var articlesOrder =  row.Cell(2).GetValue<string>();
            var dateOrder =  row.Cell(3).GetValue<DateTime>();
            var dateDelivery =  row.Cell(4).GetValue<DateTime>();
            var pickupPointId =  row.Cell(5).GetValue<int>();
            var userName =  row.Cell(6).GetValue<string>();
            var code =  row.Cell(7).GetValue<int>();

            var user = UserGet(userName);

            if (_db.Orders.Any(o => o.Number == number))
            {
                continue;
            }
            
            var order = new Order
            {
                Number = number,
                DateOrder = DateTime.SpecifyKind(dateOrder, DateTimeKind.Utc),
                DateDelivery = DateTime.SpecifyKind(dateDelivery, DateTimeKind.Utc),
                PickupPointId = pickupPointId,
                UserId =  user.Id,
                Code = code
            };
            
            _db.Orders.Add(order);
            _db.SaveChanges();

            var articlesOrderArray = articlesOrder.Split(',');
            
            for (int i = 0; i < articlesOrderArray.Length-1; i+=2)
            {
                var article = articlesOrderArray[i].Trim();
                var quantity = int.Parse(articlesOrderArray[i+1].Trim());

                var product = _db.Products.FirstOrDefault(p => p.Article == article);
                
                if(product == null) continue;

                var orderProduct = new OrderProduct
                {
                    ProductId = product.Id,
                    OrderId = order.Id,
                    Quantity = quantity
                };
                
                _db.OrderProducts.Add(orderProduct);
            }

            _db.SaveChanges();
        }
    }
    
    private User UserGet(string name)
    {
        var user = _db.Users.First(u => u.FullName == name);

        return user;
    }
}