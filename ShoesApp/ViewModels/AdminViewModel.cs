using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ShoesApp.Models;
using ShoesApp.Views;

namespace ShoesApp.ViewModels;

public partial class AdminViewModel: ObservableObject
{
    private readonly AppDbContext _db = new();

    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<Order> Orders { get; } = new();
    [ObservableProperty] private string _currentUserName = "";
    
    
    public AdminViewModel(User? user)
    {
        LoadProducts();
        LoadOrders();
        CurrentUserName = user?.FullName ?? "";
    }

    private void LoadProducts()
    {
        Products.Clear();
        
        foreach (var p in _db.Products
                     .Include(p => p.Unit)
                     .Include(p => p.Supplier)
                     .Include(p => p.Manufacturer)
                     .Include(p => p.Category)
                     .ToList())
        {
            Products.Add(p);
        }
    }
    
    private void LoadOrders()
    {
        Orders.Clear();
            
        foreach (var p in _db.Orders
                     .Include(p => p.PickupPoint)
                     .Include(p => p.User)
                     .Include(p => p.OrdersProducts)
                     .ToList())
        {
            Orders.Add(p);
        }
    }

    [RelayCommand]
    private void Logout(Window window)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        window.Close();
    }
    
}