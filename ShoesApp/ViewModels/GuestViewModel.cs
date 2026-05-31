using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ShoesApp.Models;
using ShoesApp.Views;

namespace ShoesApp.ViewModels;

public partial class GuestViewModel: ObservableObject
{
    private readonly AppDbContext _db = new();

    public ObservableCollection<Product> Products { get; } = new();
    
    public GuestViewModel()
    {
        LoadProducts();
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

    [RelayCommand]
    private void Logout(Window window)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        window.Close();
    }
    
}