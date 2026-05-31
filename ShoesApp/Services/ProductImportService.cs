using ClosedXML.Excel;
using ShoesApp.Models;

namespace ShoesApp.Services;

public class ProductImportService
{
    private readonly AppDbContext _db = new();
    
    public void Import(string filePath)
    {
        var workbook = new XLWorkbook(filePath);
        
        var worksheet =  workbook.Worksheet(1);
        
        var rows =  worksheet.RowsUsed().Skip(1);

        foreach (var row in rows)
        {
            var article =  row.Cell(1).GetValue<string>();
            var name =  row.Cell(2).GetValue<string>();
            var unitName =  row.Cell(3).GetValue<string>();
            var price =  row.Cell(4).GetValue<decimal>();
            var supplierName =  row.Cell(5).GetValue<string>();
            var manufacturerName =  row.Cell(6).GetValue<string>();
            var categoryName =  row.Cell(7).GetValue<string>();
            var discount =  row.Cell(8).GetValue<int>();
            var quantity =  row.Cell(9).GetValue<int>();
            var description =  row.Cell(10).GetValue<string>();
            var photo =  row.Cell(11).GetValue<string>();

            var unit = UnitGetOrCreate(unitName);
            var supplier = SupplierGetOrCreate(supplierName);
            var manufacturer = ManufacturerGetOrCreate(manufacturerName);
            var category = CategoryGetOrCreate(categoryName);

            if (_db.Products.Any(p => p.Description == description))
            {
                continue;
            }

            var product = new Product
            {
                Article = article,
                Name = name,
                UnitId = unit.Id,
                Price = price,
                SupplierId = supplier.Id,
                ManufacturerId = manufacturer.Id,
                CategoryId = category.Id,
                Discount = discount,
                Quantity = quantity,
                Description = description,
                Photo = photo
            };
            
            _db.Products.Add(product);
            _db.SaveChanges();
        }
    }

    private Unit UnitGetOrCreate(string name)
    {
        var unit = _db.Units.FirstOrDefault(u => u.Name == name);
        if (unit != null)
        {
            return unit;
        }

        unit = new Unit
        {
            Name = name
        };
        
        _db.Units.Add(unit);
        _db.SaveChanges();

        return unit;
    }
    private Supplier SupplierGetOrCreate(string name)
        {
            var supplier = _db.Suppliers.FirstOrDefault(s => s.Name == name);
            if (supplier != null)
            {
                return supplier;
            }
    
            supplier = new Supplier
            {
                Name = name
            };
            
            _db.Suppliers.Add(supplier);
            _db.SaveChanges();
    
            return supplier;
        }
    private Manufacturer ManufacturerGetOrCreate(string name)
        {
            var manufacturer = _db.Manufacturers.FirstOrDefault(m => m.Name == name);
            if (manufacturer != null)
            {
                return manufacturer;
            }
    
            manufacturer = new Manufacturer
            {
                Name = name
            };
            
            _db.Manufacturers.Add(manufacturer);
            _db.SaveChanges();
    
            return manufacturer;
        }
    private Category CategoryGetOrCreate(string name)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Name == name);
            if (category != null)
            {
                return category;
            }
    
            category = new Category
            {
                Name = name
            };
            
            _db.Categories.Add(category);
            _db.SaveChanges();
    
            return category;
        }
    
}