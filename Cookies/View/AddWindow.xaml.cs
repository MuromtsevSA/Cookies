using Cookies.Model;
using Cookies.ViewModel;
using System.Windows;

namespace Cookies.View;

public partial class AddWindow : Window
{
    private Context db = new Context();
    public AddWindow()
    {
        InitializeComponent();
        DataContext = new AddViewModel(db);
    }
}