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
using VMusic.Models;
using VMusic.ViewModels.Admin;

namespace VMusic.Views.Admin
{
    /// <summary>
    /// Логика взаимодействия для MusicPage.xaml
    /// </summary>
    public partial class MusicPage : Page
    {
        public MusicPage()
        {
            InitializeComponent();
            DataContext = new MusicPageViewModel(new List<Song>());
        }
    }
}
