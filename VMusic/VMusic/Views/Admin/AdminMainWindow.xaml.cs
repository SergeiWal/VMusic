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
using VMusic.ViewModels.Admin;

namespace VMusic.Views.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            Loaded += AdminMainWindowWindow_Loaded;
        }

        private void AdminMainWindowWindow_Loaded(object sender, RoutedEventArgs e)
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
