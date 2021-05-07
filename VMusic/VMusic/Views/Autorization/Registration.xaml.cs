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
using VMusic.ViewModels.Autorization;

namespace VMusic.Views.Autorization
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            Loaded += RegistrationWindow_Loaded;
            DataContext = new RegistrationViewModel();
        }

        private void RegistrationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IWindowController vm)
            {
                vm.Close += () =>
                {
                    this.Close();
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

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((RegistrationViewModel) this.DataContext).Password = PasswordBox.Password;
            }
        }

        private void RepeatPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((RegistrationViewModel)this.DataContext).RepeatPassword = RepeatPasswordBox.Password;
            }
        }
    }
}
