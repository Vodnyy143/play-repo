using System.Windows;
using ShoesApp.Models;
using ShoesApp.ViewModels;

namespace ShoesApp.Views;

public partial class AdminWindow : Window
{
    public AdminWindow(User? user)
    {
        InitializeComponent();
        var adminViewModel = new AdminViewModel(user);
        DataContext = adminViewModel;
    }
}