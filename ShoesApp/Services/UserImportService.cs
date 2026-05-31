using ClosedXML.Excel;
using ShoesApp.Models;

namespace ShoesApp.Services;

public class UserImportService
{
    private readonly AppDbContext _db = new();
    
    public void Import(string filePath)
    {
        var workbook = new XLWorkbook(filePath);
        
        var worksheet =  workbook.Worksheet(1);
        
        var rows =  worksheet.RowsUsed().Skip(1);

        foreach (var row in rows)
        {
            var roleName =  row.Cell(1).GetValue<string>();
            var fullName =  row.Cell(2).GetValue<string>();
            var login =  row.Cell(3).GetValue<string>();
            var password =  row.Cell(4).GetValue<string>();

            var role = RoleGetOrCreate(roleName);

            if (_db.Users.Any(u => u.Login == login))
            {
                continue;
            }

            var user = new User
            {
                RoleId = role.Id,
                FullName = fullName,
                Login = login,
                Password = password
            };
            
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }

    private Role RoleGetOrCreate(string roleName)
    {
        var role = _db.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role != null)
        {
            return role;
        }

        role = new Role
        {
            Name = roleName
        };
        
        _db.Roles.Add(role);
        _db.SaveChanges();

        return role;
    }
}