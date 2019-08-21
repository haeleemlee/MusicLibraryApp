using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;




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
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("onesong.mp3");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /*private void One_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView one = sender as ListView;
            string selected = one.SelectedItem.ToString();
            MessageDialog dlg = new MessageDialog("selected number;" + selected);
            
        }*/
    }

}
