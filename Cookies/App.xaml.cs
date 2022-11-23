using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cookies.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Cookies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            
            //services.AddSingleton<IContext,Context>();
            //services.AddSingleton<MainWindow>();
            //services.AddSingleton<Addwindow>();
            //services.AddScoped<MainViewModel>();
            //services.AddScoped<AddViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetService<MainViewModel>();
            mainWindow.Show();
        }
    }
}