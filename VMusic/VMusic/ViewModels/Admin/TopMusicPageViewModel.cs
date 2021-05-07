﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class TopMusicPageViewModel: BaseViewModel
    {
        public static int TOP_LIST_SIZE = 10;
        public static string TOP_LIST_NAME = "BestMusic";

        private UnitOfWork dbWorker;
        private Playlist topSongPlaylist;
        public ObservableCollection<SongViewModel> TopSongList { get; set; }

        public TopMusicPageViewModel()
        {
            dbWorker = new UnitOfWork();

            TopSongList = new ObservableCollection<SongViewModel>(dbWorker.Songs.GetAllObject().OrderBy(n => n.Rating)
                .Select(s => new SongViewModel(s)).Take(TOP_LIST_SIZE));

            topSongPlaylist = new Playlist()
            {
                Name = TOP_LIST_NAME,
                Image = null,
                User = null,
                UserId = null
            };
        }


        private Command updateTopPlaylist;

        public Command UpdateTopPlaylist
        {
            get
            {
                return updateTopPlaylist ?? (updateTopPlaylist = new Command((obj) =>
                {
                    var playlist = dbWorker.Playlist.GetByPredicate((b) => b.Name == TOP_LIST_NAME && b.UserId == null);
                    topSongPlaylist.Songs = new List<Song>(TopSongList.Select(b => b.song));
                    if (playlist == null)
                    {
                        dbWorker.Playlist.Create(topSongPlaylist);
                        AddSongsToPlaylist();
                        dbWorker.Save();
                    }
                    else
                    {
                        AddSongsToPlaylist();
                        dbWorker.Save();
                    }
                }));
            }
        }

        private void AddSongsToPlaylist()
        {
            var playlist = dbWorker.Playlist.GetAllObject().FirstOrDefault(p=>p.Name==TOP_LIST_NAME);
            if (playlist != null)
            {
                foreach (var song in TopSongList)
                {
                    playlist.Songs.Add(song.song);
                }
                dbWorker.Save();
            }
        }

    }
}
