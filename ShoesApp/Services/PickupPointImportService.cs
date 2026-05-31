using ClosedXML.Excel;
using ShoesApp.Models;

namespace ShoesApp.Services;

public class PickupPointImportService
{
    private readonly AppDbContext _db = new();
    
    public void Import(string filePath)
    {
        var workbook = new XLWorkbook(filePath);
        
        var worksheet =  workbook.Worksheet(1);
        
        var rows =  worksheet.RowsUsed();

        foreach (var row in rows)
        {
            var name =  row.Cell(1).GetValue<string>();

            if (_db.PickupPoints.Any(pp => pp.Name == name))
            {
                continue;
            }

            var pickupPoint = new PickupPoint
            {
                Name = name
            };
            
            _db.PickupPoints.Add(pickupPoint);
            _db.SaveChanges();
        }
    }
}