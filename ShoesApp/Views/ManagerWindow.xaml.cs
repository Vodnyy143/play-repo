using System.Windows;
using ShoesApp.Models;
using ShoesApp.ViewModels;

namespace ShoesApp.Views;

public partial class ManagerWindow : Window
{
    public ManagerWindow(User? user)
    {
        InitializeComponent();
        var managerViewModel = new ManagerViewModel(user);
        DataContext = managerViewModel;
    }
}