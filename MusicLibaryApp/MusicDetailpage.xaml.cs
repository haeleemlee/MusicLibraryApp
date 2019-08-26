using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicLibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MusicDetailpage : Page
    {

        public MusicDetailpage()
        {
            this.InitializeComponent();
        }

        private MusicEntry _musicEntry;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _musicEntry = e.Parameter as MusicEntry;

            FileStream fs = new FileStream(_musicEntry.MusicFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            MP3MetafileReader(fs);

            BitmapImage image = new BitmapImage();
            var storageFile = await StorageFile.GetFileFromPathAsync(_musicEntry.ImageFilePath);
            using (Windows.Storage.Streams.IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
                await image.SetSourceAsync(stream);
            }
            this.image.Source = image;
        }

        private void MP3MetafileReader(FileStream streamingfile)
        {
            byte[] b = new byte[128];
            streamingfile.Seek(-128, SeekOrigin.End);
            streamingfile.Read(b, 0, 128);
            bool isSet = false;
            String sFlag = System.Text.Encoding.Default.GetString(b, 0, 3);
            if (sFlag.CompareTo("TAG") == 0)
            {
                System.Console.WriteLine("Tag   is   setted! ");
                isSet = true;
            }

            if (isSet)
            {
                this.TxtStrTitle.Text = System.Text.Encoding.Default.GetString(b, 3, 30);
                this.TxtStrSinger.Text = System.Text.Encoding.Default.GetString(b, 33, 30);
                this.TxtStrAlbum.Text = System.Text.Encoding.Default.GetString(b, 63, 30);
                this.TxtStrYear.Text = System.Text.Encoding.Default.GetString(b, 93, 4);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}