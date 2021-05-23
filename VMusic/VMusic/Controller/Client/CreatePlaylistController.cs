using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VMusic.Controller.Client.Messagers;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.Controller.Client
{
    class CreatePlaylistController
    {
        private UnitOfWork dbWorker;

        public CreatePlaylistController()
        {
            dbWorker = new UnitOfWork();
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
                    return  ImageConverter.GetImageByByteArray(imgBuf);
                }
            }

            return null;
        }

        public IEnumerable<Song> FindSong(string FindSongName)
        {
            return dbWorker.Songs.GetAllObject()
                .Where(s => s.Name.Contains(FindSongName) || s.Author.Contains(FindSongName) ||
                            s.Album.Contains(FindSongName));
        }

        public bool IsPlaylistNotRepeat(Playlist playlist)
        {
            var obj = dbWorker.Playlist.GetAllObject()
                .FirstOrDefault(p => p.Name == playlist.Name && p.UserId == playlist.UserId);

            return obj == null;
        }

        public bool AddPlaylist(Playlist playlist)
        {
            if (IsPlaylistNotRepeat(playlist))
            {
                dbWorker.Playlist.Create(playlist);
                dbWorker.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
