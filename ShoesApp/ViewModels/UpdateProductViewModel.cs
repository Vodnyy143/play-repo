using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoesApp.Models;

namespace ShoesApp.ViewModels;

public partial class UpdateProductViewModel: ObservableObject
{
    private readonly AppDbContext _db = new();
    private int _productId;
    private Action _onProductUpdated;

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

    public UpdateProductViewModel(Action onProductUpdated, Product product)
    {
        _onProductUpdated = onProductUpdated;
        _productId = product.Id;
        LoadUnits();
        LoadSuppliers();
        LoadManufacturers();
        LoadCategories();

        NewArticle = product.Article ?? "";
        NewName = product.Name ?? "";
        NewPrice = product.Price;
        NewDescription = product.Description ?? "";
        NewDiscount = product.Discount;
        NewQuantity = product.Quantity;
        NewPhoto = product.Photo ?? "";
        
        SelectedUnit = Units.FirstOrDefault(x => x.Id == product.UnitId);
        SelectedSupplier = Suppliers.FirstOrDefault(x => x.Id == product.SupplierId);
        SelectedManufacturer = Manufacturers.FirstOrDefault(x => x.Id == product.ManufacturerId);
        SelectedCategory = Categories.FirstOrDefault(x => x.Id == product.CategoryId);
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
    private void UpdateProduct(Window window)
    {
        var product = _db.Products.Find(_productId);
        if (product == null)
        {
            ErrorMessage = "Такой товара нет";
            return;
        }

        product.Article = NewArticle;
        product.Name = NewName;
        product.UnitId = SelectedUnit.Id;
        product.Price = NewPrice;
        product.SupplierId = SelectedSupplier.Id;
        product.ManufacturerId = SelectedManufacturer.Id;
        product.CategoryId = SelectedCategory.Id;
        product.Description = NewDescription;
        product.Discount = NewDiscount;
        product.Quantity = NewQuantity;
        product.Photo = NewPhoto;
        
        _db.SaveChanges();
        
        _onProductUpdated.Invoke();
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