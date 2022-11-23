using Cookies.Model;
using Cookies.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cookies.View
{
    /// <summary>
    /// Логика взаимодействия для SalePersonView.xaml
    /// </summary>
    public partial class SalePersonView : Window
    {
        private Context _db;
        public SalePersonView(Context db)
        {
            InitializeComponent();
            _db = db;
            this.DataContext = new SalePersonViewModel(_db);
            
        }
    }
}
