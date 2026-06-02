using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoesApp.Models;

namespace ShoesApp.ViewModels;

public partial class CreateProductViewModel: ObservableObject
{
    private readonly AppDbContext _db = new();
    private Action _onProductCreated;

    public ObservableCollection<Unit> Units { get; } = new();
    public ObservableCollection<Supplier> Suppliers { get; } = new();
    public ObservableCollection<Manufacturer> Manufacturers { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();
    
    [ObservableProperty] public string _newArticle = "";
    [ObservableProperty] public string _newName = "";
    [ObservableProperty] public Unit _selectedUnit;
    [ObservableProperty] public decimal _newPrice;
    [ObservableProperty] public Supplier _selectedSupplier;
    [ObservableProperty] public Manufacturer _selectedManufacturer;
    [ObservableProperty] public Category _selectedCategory;
    [ObservableProperty] public string _newDescription = "";
    [ObservableProperty] public int _newDiscount;
    [ObservableProperty] public int _newQuantity;
    [ObservableProperty] public string _newPhoto = "";
    [ObservableProperty] public string _errorMessage = "";

    public CreateProductViewModel(Action onProductCreated)
    {
        _onProductCreated = onProductCreated;
        LoadUnits();
        LoadSuppliers();
        LoadManufacturers();
        LoadCategories();
    }

    private void LoadUnits()
    {
        Units.Clear();

        foreach (var u in _db.Units.ToList())
        {
            Units.Add(u);
        }
    }
    
    private void LoadSuppliers()
    {
        Suppliers.Clear();

        foreach (var u in _db.Suppliers.ToList())
        {
            Suppliers.Add(u);
        }
    }
    
    private void LoadManufacturers()
    {
        Manufacturers.Clear();

        foreach (var u in _db.Manufacturers.ToList())
        {
            Manufacturers.Add(u);
        }
    }
    
    private void LoadCategories()
    {
        Categories.Clear();

        foreach (var u in _db.Categories.ToList())
        {
            Categories.Add(u);
        }
    }

    [RelayCommand]
    private void CreateProduct(Window window)
    {
        var product = _db.Products.FirstOrDefault(p => p.Name == NewName && p.Description == NewDescription);
        if (product != null)
        {
            ErrorMessage = "Такой товар уже есть";
            return;
        }

        product = new Product
        {
            Article = NewArticle,
            Name = NewName,
            UnitId = SelectedUnit.Id,
            Price = NewPrice,
            SupplierId = SelectedSupplier.Id,
            ManufacturerId = SelectedManufacturer.Id,
            CategoryId = SelectedCategory.Id,
            Description = NewDescription,
            Discount = NewDiscount,
            Quantity = NewQuantity,
            Photo = NewPhoto
        };
        
        _db.Products.Add(product);
        _db.SaveChanges();
        
        NewArticle = "";
        NewName = "";
        NewDescription = "";
        NewPhoto = "";
        
        _onProductCreated.Invoke();
        window.Close();
    }

    [RelayCommand]
    private void Cancel(Window window)
    {
        NewArticle = "";
        NewName = "";
        NewDescription = "";
        NewPhoto = "";
        window.Close();
    }
}