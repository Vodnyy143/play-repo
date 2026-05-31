using System.Windows;
using ShoesApp.Models;
using ShoesApp.ViewModels;

namespace ShoesApp.Views;

public partial class ClientWindow : Window
{
    public ClientWindow(User? user)
    {
        InitializeComponent();
        var clientViewModel = new ClientViewModel(user);
        DataContext = clientViewModel;
    }
}