using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;
using VMusic.ViewModels;
using VMusic.ViewModels.Client;

namespace VMusic.Controller.Client.PagesController
{
    static class ViewModelCreator
    {
        public static HomePageViewModel CreateHomePageViewModel(SongContent songContent)
        {
            return new HomePageViewModel(songContent);
        }

        public static CreatePlaylistViewModel CreateAddPlaylistPageViewModel(User user,
            PlaylistsPageViewModel playlistsPageViewModel, PropertyChangedEventHandler callback)
        {
            CreatePlaylistViewModel createPlaylistViewModel = new CreatePlaylistViewModel(playlistsPageViewModel, user);
            createPlaylistViewModel.PropertyChanged += callback;
            return createPlaylistViewModel;
        }

        public static SettingViewModel CreateSettingViewModel(User user, PropertyChangedEventHandler callback)
        {
            SettingViewModel settingViewModel = new SettingViewModel(user);
            settingViewModel.PropertyChanged += callback;
            return settingViewModel;
        }

        public static TopSongListViewModel CreateTopMusicViewModel(SongContent songContent)
        {
            TopSongListViewModel topMusicPageViewModel = new TopSongListViewModel(songContent);
            return topMusicPageViewModel;
        }

        public static PlaylistsPageViewModel CreatePlaylistsPageViewModel(User user, PropertyChangedEventHandler callback)
        {
            PlaylistsPageViewModel playlistsPageViewModel = new PlaylistsPageViewModel(user);
            playlistsPageViewModel.PropertyChanged += callback;
            return playlistsPageViewModel;
        }

        public static GenreViewModel CreateGenreViewModel(PropertyChangedEventHandler callback)
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            genreViewModel.PropertyChanged += callback;
            return genreViewModel;
        }

        public static SinglePlaylistViewModel CreateSinglePlaylistViewModel
            (PlaylistViewModel playlist, SongContent songContent, User user, PropertyChangedEventHandler callback)
        {
            SinglePlaylistViewModel singlePlaylistViewModel = new SinglePlaylistViewModel(playlist, songContent, user);
            singlePlaylistViewModel.PropertyChanged += callback;
            return singlePlaylistViewModel;
        }

        public static UpdatePlaylistViewModel CreateUpdatePlaylistViewModel(PlaylistViewModel playlist, PropertyChangedEventHandler callback)
        {
            UpdatePlaylistViewModel updatePlaylistViewModel = new UpdatePlaylistViewModel(playlist);
            updatePlaylistViewModel.PropertyChanged += callback;
            return updatePlaylistViewModel;
        }


    }
}
