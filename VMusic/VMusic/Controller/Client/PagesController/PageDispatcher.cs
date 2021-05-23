using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VMusic.ViewModels;
using VMusic.ViewModels.Client;
using VMusic.Views.Client;

namespace VMusic.Controller.Client.PagesController
{
    class PageDispatcher
    {

        public HomePage HomePage { get; set; }
        public CreatePlaylistPage CreatePlaylistPage { get; set; }
        public SettingPage SettingPage { get; set; }
        public HomePage TopMusicPage { get; set; }
        public PlaylistsPage PlaylistsPage { get; set; }
        public SinglePlaylistPage SinglePlaylistPage { get; set; }
        public UpdatePlaylistPage UpdatePlaylistPage { get; set; }
        public HomePage CurrentSongListPage { get; set; }
        public HomePage FindSongPage { get; set; }
        public GenrePage GenrePage { get; set; }


        public Page CurrentPage { get; set; }

        public PageDispatcher()
        {
            HomePage = new HomePage();
            PlaylistsPage = new PlaylistsPage();
            CreatePlaylistPage = new CreatePlaylistPage();
            SettingPage = new SettingPage();
            TopMusicPage = new HomePage();
            SinglePlaylistPage = new SinglePlaylistPage();
            FindSongPage = new HomePage();
            CurrentSongListPage = new HomePage();
            UpdatePlaylistPage = new UpdatePlaylistPage();
            GenrePage = new GenrePage();

            CurrentPage = HomePage;
        }

        public static CurrentGenrePage CreateCurrentPage(PlaylistViewModel currentGenre, Player.Player player)
        {
            CurrentGenrePage currentGenrePage = new CurrentGenrePage();
            currentGenrePage.DataContext = new CurrentGenreViewModel(currentGenre, player);
            return currentGenrePage;
        }

    }
}
