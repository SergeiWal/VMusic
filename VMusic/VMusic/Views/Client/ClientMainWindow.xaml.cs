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
using VMusic.ViewModels;
using VMusic.ViewModels.Client;

namespace VMusic.Views.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : Window
    {
        public ClientMainWindow()
        {
            InitializeComponent();
            Loaded += ClientMainWindowWindow_Loaded;
        }

        private void ClientMainWindowWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IWindowController vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };

                vm.SizeChange += () =>
                {
                    this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
                };
                
                vm.Collapse += () =>
                {
                    this.WindowState = WindowState.Minimized;
                };
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
