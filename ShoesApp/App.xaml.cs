using System.Configuration;
using System.Data;
using System.Windows;
using ShoesApp.Models;
using ShoesApp.Services;

namespace ShoesApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        using var dbContext = new AppDbContext();
        dbContext.Database.EnsureCreated();

        var userImport = new UserImportService();
        userImport.Import("Resources/user_import.xlsx");
        
        var productImport = new ProductImportService();
        productImport.Import("Resources/Tovar.xlsx");
        
        var pickupImport = new PickupPointImportService();
        pickupImport.Import("Resources/Пункты выдачи_import.xlsx");
        
        var orderImport = new OrderImportService();
        orderImport.Import("Resources/Заказ_import.xlsx");
    }
}