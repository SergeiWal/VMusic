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
using VMusic.ViewModels.Autorization;

namespace VMusic.Views.Autorization
{
    /// <summary>
    /// Логика взаимодействия для LoginAsAdmin.xaml
    /// </summary>
    public partial class LoginAsAdmin : Window
    {
        public LoginAsAdmin()
        {
            InitializeComponent();
            DataContext = new AutorizationViewModel(this);
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
