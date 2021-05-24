using System;
using System.IO;
using Microsoft.Win32;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.Controller.Admin
{
    class AddUpdateMusicController
    {
        private const string FILE_NAME_DELIMITOR = "_";
        private const string FILE_EXTENSION = ".mp3";
        private const string FILE_PATH_PREFIX = @"..\..\Songs\";
        private const string PREFIX_FOR_DELETE = @"..\";

        private UnitOfWork dbWorker;

        public AddUpdateMusicController()
        {
            dbWorker = new UnitOfWork();
        }

        public Byte[] GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"D:\";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imgBuf = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                if ((imgBuf.Length / 1024) < 1024)
                {
                    return imgBuf;
                }
            }

            return null;
        }

        public string GetSongPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
            openFileDialog.InitialDirectory = @"D:\";

            if (openFileDialog.ShowDialog() == true)
            {
               return openFileDialog.FileName;
            }

            return string.Empty;
        }

        public string CopySongToLocalFileRepository(string path, Song song)
        {
            string newPath = FILE_PATH_PREFIX + song.Name + FILE_NAME_DELIMITOR + song.Author + FILE_EXTENSION;
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.CopyTo(newPath,true);
                }

                return newPath;
            }
            catch (IOException)
            {
                return null;
            }
        }

        public void DeleteOldFileInLocalDirectory(Song song)
        {
            FileInfo fileInfo = new FileInfo( song.Source);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }

        public Song GetSongFromDb(int id)
        {
            return dbWorker.Songs.GetById(id);
        }

        public void AddSongToDB(Song song)
        {
            dbWorker.Songs.Create(song);
            dbWorker.Save();
        }

        public void SongDataUpdate(int id, Song song)
        {
            var dbSong = dbWorker.Songs.GetById(id);
            dbSong.Album = song.Album;
            dbSong.Author = song.Author;
            dbSong.Genre = song.Genre;
            dbSong.Name = song.Name;
            dbSong.Image = song.Image;
            dbSong.Source = song.Source;
            dbWorker.Save();
        }

    }
}
