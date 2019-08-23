using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Collections.Generic;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicLibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer player;
        bool playing;

        public MainPage()
        {
            this.InitializeComponent();
            player = new MediaPlayer();
        }
        

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {

            MusicEntry musicEntry = GlobalData._entryList[0]; // need to use index from GUI list box
            Windows.Storage.StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(musicEntry.musicFilePath);

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            if(playing)
            { player.Source=null;
                playing = false;
            }
            else
            { player.Play();
                playing = true;
            }
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            MusicEntry entry = GlobalData._entryList[0];    // need to use the selected index from GUI ListBox

            this.Frame.Navigate(typeof(MusicDetailpage), entry);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddMusicPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.GetType() == typeof(MusicEntry))
            {
                MusicEntry entry = e.Parameter as MusicEntry;

                GlobalData._entryList.Add(entry);

                // add new entry to GUI listbox here
            }
        }



        /*private void One_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView one = sender as ListView;
            string selected = one.SelectedItem.ToString();
            MessageDialog dlg = new MessageDialog("selected number;" + selected);

        }*/
    }

    }
