using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.Controller.Client
{
    class UpdatePlaylistController
    {
        private UnitOfWork dbWorker;

        public UpdatePlaylistController()
        {
            dbWorker = new UnitOfWork();
        }

        public Playlist GetPlaylist(int id)
        {
            return dbWorker.Playlist.GetByPredicate(p => p.Id == id);
        }

        public BitmapImage GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"D:\";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imgBuf = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                if ((imgBuf.Length / 1024) < 1024)
                {
                    return ImageConverter.GetImageByByteArray(imgBuf);
                }
            }

            return null;
        }

        public void DeletePlaylist(int id)
        {
            dbWorker.Playlist.Delete(id);
            dbWorker.Save();
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            var plist = dbWorker.Playlist.GetByPredicate(p => p.Id == playlist.Id);
            plist.Name = playlist.Name;
            plist.Image = playlist.Image;
            plist.Songs = playlist.Songs;
            dbWorker.Save();
        }

        public IEnumerable<Song> FindSong(string FindSongName)
        {
            return dbWorker.Songs.GetAllObject()
                .Where(s => s.Name.Contains(FindSongName) || s.Author.Contains(FindSongName) ||
                            s.Album.Contains(FindSongName));
        }

        public void DeleteSongFromPlaylist(Song song, int playlistId)
        {
            var plist = dbWorker.Playlist.GetByPredicate(p => p.Id == playlistId);
            plist.Songs.Remove(song);
        }
    }
}
