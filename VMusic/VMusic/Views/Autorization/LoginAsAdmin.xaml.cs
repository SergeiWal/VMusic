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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VMusic.Views.Autorization
{
    /// <summary>
    /// Логика взаимодействия для LoginAsAdmin.xaml
    /// </summary>
    public partial class LoginAsAdmin : Page
    {
        public LoginAsAdmin()
        {
            InitializeComponent();
        }

        private void ToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
        private void ToRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
        
    }
}
