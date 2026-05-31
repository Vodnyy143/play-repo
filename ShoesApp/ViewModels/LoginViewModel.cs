using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ShoesApp.Models;
using ShoesApp.Views;
using System.Windows;
namespace ShoesApp.ViewModels;

public partial class LoginViewModel: ObservableObject
{
    private readonly AppDbContext _db = new();

    [ObservableProperty] private string _login = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _errorMessage = "";
    
    [RelayCommand]
    public void SignIn()
    {
        if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
        {
            ErrorMessage = "Введите все поля";
            return;
        }
        var user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == Login);
        if (user == null)
        {
            ErrorMessage = "Неправильный логин или пароль";
            return;
        }
        
        var isPasswordMatch = user?.Password == Password;
        if (!isPasswordMatch)
        {
            ErrorMessage = "Неправильный логин или пароль";
            return;
        }

        switch (user?.Role.Name) 
        {
            case "Администратор":
                var adminWindow = new AdminWindow(user);
                adminWindow.Show();
                Application.Current.Windows[0]?.Close();
                break;
            case "Менеджер":
                var managerWindow = new ManagerWindow(user);
                managerWindow.Show();
                Application.Current.Windows[0]?.Close();
                break;
            case "Авторизированный клиент":
                var clientWindow = new ClientWindow(user);
                clientWindow.Show();
                Application.Current.Windows[0]?.Close();
                break;
        }
    }
    
    [RelayCommand]
    public void SignInAsGuest()
    {
        var guestWindow = new GuestWindow();
        guestWindow.Show();
        Application.Current.Windows[0]?.Close();
    }
}