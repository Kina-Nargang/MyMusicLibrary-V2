using MyMusicLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyMusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> musics;
        private List<MenuItem> menuItems;

        public MainPage()
        {
            this.InitializeComponent();
            musics = new ObservableCollection<Music>();
            MusicManager.GetAllMusic(musics);
            //MusicManager.GetNewMusic(musics,);
            menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Classical.png",
                Category = MusicCategory.Classical
            });

            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Country.png",
                Category = MusicCategory.Country
            });

            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/International.png",
                Category = MusicCategory.International
            });

            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Others.png",
                Category = MusicCategory.Others
            });

            menuItems.Add(new MenuItem
            {
                IconFile = "Assets/Icons/Rock.png",
                Category = MusicCategory.Rock
            });

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.GetAllMusic(musics);
            CategoryTextBlock.Text = "All Music";
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            CategoryTextBlock.Text = menuItem.Category.ToString();
            MusicManager.GetMusicByCategory(musics, menuItem.Category);
            BackButton.Visibility = Visibility.Visible;
        }

        private void MusicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var music = (Music)e.ClickedItem;
            MyMediaElement.Source = MediaSource.CreateFromUri(new Uri(this.BaseUri, music.MusicFile));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlayerPage));

        }
    }
}
