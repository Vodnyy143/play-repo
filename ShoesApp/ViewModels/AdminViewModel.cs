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
    private List<Product> _allProducts = new();

    [ObservableProperty] private string _searchText = "";
    [ObservableProperty] private Category _selectedCategory;
    [ObservableProperty] private string _selectedSort = "Без сортировки";

    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<OrderProduct> OrderProducts { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();
    public List<string> SortOptions { get; } = ["Без сортировки", "Цена по возрастанию", "Цена по убыванию"];
    [ObservableProperty] private string _currentUserName = "";
    
    partial void OnSearchTextChanged(string value) => Filter();
    partial void OnSelectedCategoryChanged(Category value) => Filter();
    partial void OnSelectedSortChanged(string value) => Filter();
    
    public AdminViewModel(User? user)
    {
        LoadProducts();
        LoadOrders();
        LoadCategories();
        CurrentUserName = user?.FullName ?? "";
    }

    private void LoadProducts()
    {
        _allProducts = _db.Products
            .Include(p => p.Unit)
            .Include(p => p.Supplier)
            .Include(p => p.Manufacturer)
            .Include(p => p.Category)
            .ToList();

        Filter();
    }
    
    private void LoadOrders()
    {
        OrderProducts.Clear();
            
        foreach (var p in _db.Orders
                     .Include(p => p.PickupPoint)
                     .Include(p => p.User)
                     .Include(p => p.OrdersProducts)
                     .ThenInclude(pp =>  pp.Product)
                     .ToList())
        {
            foreach (var op in p.OrdersProducts)
            {
                OrderProducts.Add(op);
            }
        }
    }
    
    private void LoadCategories()
    {
        Categories.Clear();
            
        foreach (var p in _db.Categories
                     .ToList())
        {
            Categories.Add(p);
        }
    }

    [RelayCommand]
    private void Logout(Window window)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        window.Close();
    }
    
    private void Filter()
    {
        Products.Clear();

        var query = _allProducts.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            query = query.Where(p =>
                p.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        if (SelectedCategory?.Id != null)
        {
            query = query.Where(p =>
                p.CategoryId == SelectedCategory.Id);
        }

        if (SelectedSort != "Без сортировки")
        {
            switch (SelectedSort)
            {
                case "Цена по возрастанию":
                    query = query.OrderBy(p => p.Price);
                    break;
                
                case "Цена по убыванию":
                    query = query.OrderByDescending(p => p.Price);
                    break;
            }
        }
        
        foreach (var p in query)
        {
            Products.Add(p);
        }
    }
    
}