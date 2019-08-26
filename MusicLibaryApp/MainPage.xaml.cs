using System;
using System.Collections.ObjectModel;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MusicLibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        MediaPlayer player;
        bool playing;

        public MainPage()
        {
            this.InitializeComponent();
            player = new MediaPlayer();
            Musiclist.ItemsSource = MusicEntries;
            //this.DataContext = await MusicEntry.GetAllMusicEntriesAsync();

        }


        public static ObservableCollection<MusicEntry> MusicEntries { set; get; } = new ObservableCollection<MusicEntry>();

        protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // this.DataContext = await MusicEntry.GetAllMusicEntriesAsync();
            //if (e.Parameter != null && e.Parameter.GetType() == typeof(MusicEntry))
            //{
            //    MusicEntry entry = e.Parameter as MusicEntry;

            //    GlobalData._entryList.Add(entry);

            // add new entry to GUI listbox here
            //}
        }
        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Musiclist.SelectedItem == null)
            {
                var dialog = new MessageDialog("Please select one from the list.");
                dialog.ShowAsync();
                return;
            }

            var music = (MusicEntry)Musiclist.SelectedItem;

            Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(music.MusicFilePath);

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            if (playing)
            {
                player.Source = null;
                playing = false;
            }
            else
            {
                player.Play();
                playing = true;
            }
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (Musiclist.SelectedItem == null)
            {
                var dialog = new MessageDialog("Please select one from the list.");
                dialog.ShowAsync();
                return;
            }

            var music = (MusicEntry)Musiclist.SelectedItem;
            //MusicEntry entry = GlobalData._entryList[0];    // need to use the selected index from GUI ListBox

            this.Frame.Navigate(typeof(MusicDetailpage), music);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddMusicPage));
        }

        /*private void One_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
ListView one = sender as ListView;
string selected = one.SelectedItem.ToString();
MessageDialog dlg = new MessageDialog("selected number;" + selected);

}*/

    }

}
