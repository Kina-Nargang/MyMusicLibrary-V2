using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyMusicLibrary.Model;
using Windows.System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyMusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
        private ObservableCollection<MusicCategory> categories;
        private StorageFile copyfile_image;
        private StorageFile copyfile_song;
        private string msg_image;
        private string msg_song;

        public PlayerPage()
        {
            this.InitializeComponent();
            categories = new ObservableCollection<MusicCategory>();
            categories.Add(MusicCategory.Classical);
            categories.Add(MusicCategory.Country);
            categories.Add(MusicCategory.International);
            categories.Add(MusicCategory.Others);
            categories.Add(MusicCategory.Rock);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newmusic = new MusicManager();
            // Get the app's local folder to use as the destination folder.
            //StorageFolder localFolder = Windows.Application­Model.Package.Current.Installed­Location;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //$"Assets/Images/{drop_category.Text}/";

            // Specify a new name for the copied file.
            string renamedFileName_image = copyfile_image.Name;
            string renamedFileName_song = copyfile_song.Name;

            // Copy the file to the destination folder and rename it.
            // Replace the existing file if the file already exists.
            StorageFile copiedFile_image = await copyfile_image.CopyAsync(localFolder, renamedFileName_image, NameCollisionOption.ReplaceExisting);
            StorageFile copiedFile_song = await copyfile_song.CopyAsync(localFolder, renamedFileName_song, NameCollisionOption.ReplaceExisting);

            Windows.Storage.Provider.FileUpdateStatus status_image =
                    await CachedFileManager.CompleteUpdatesAsync(copiedFile_image);

            Windows.Storage.Provider.FileUpdateStatus status_song =
                    await CachedFileManager.CompleteUpdatesAsync(copiedFile_image);

            if (status_image == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                msg_image = "File " + copiedFile_image.Name + " was saved.";
            }
            else
            {
                msg_image = "File " + copiedFile_image.Name + " couldn't be saved.";
            }

            if (status_song == Windows.Storage.Provider.FileUpdateStatus.Complete)
            {
                msg_song = "File " + copiedFile_song.Name + " was saved.";
            }
            else
            {
                msg_song = "File " + copiedFile_song.Name + " couldn't be saved.";
            }

            txt_msg.Text = msg_image + " & " + msg_song;

        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_songname.Text))
            {
                txt_songname.Text = string.Empty;
            }


            if (!string.IsNullOrEmpty(txt_songpath.Text))
            {
                txt_songpath.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(txt_imagename.Text))
            {
                txt_imagename.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(txt_imagenpath.Text))
            {
                txt_imagenpath.Text = string.Empty;
            }

            if (drop_category.SelectedIndex != -1)
            {
                drop_category.SelectedIndex = -1;
            }
        }

        private async void upload_song(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".mp4");
            

            StorageFile file = await openPicker.PickSingleFileAsync();

            if(file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                var image = new BitmapImage();

                image.SetSource(stream);
            }

            txt_songpath.Text = file.Path;
            txt_songname.Text = file.Name;
            copyfile_song = file;

        }

        private async void upload_image(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                var image = new BitmapImage();

                image.SetSource(stream);
            }

            txt_imagenpath.Text = file.Path;
            txt_imagename.Text = file.Name;
            copyfile_image = file;
        }



    }
}
