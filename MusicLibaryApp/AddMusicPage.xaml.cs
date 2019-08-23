using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicLibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMusicPage : Page
    {

        private string _imgPath;
        private string _mp3Path;


        public AddMusicPage()
        {
            this.InitializeComponent();
        }

        private async void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            //picker.FileTypeFilter.Add(".jpg");
            //picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile imgfile = await picker.PickSingleFileAsync();

            if (imgfile != null)
            {
                // Get the app's local folder to use as the destination folder.
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                // Copy the file to the destination folder and rename it.
                // Replace the existing file if the file already exists.
                StorageFile copiedImgFile = await imgfile.CopyAsync(localFolder, imgfile.Name, NameCollisionOption.ReplaceExisting);

                this.TxtStrImg.Text = copiedImgFile.Name;

                BitmapImage image = new BitmapImage();
                var storageFile = await StorageFile.GetFileFromPathAsync(copiedImgFile.Path);
                using (Windows.Storage.Streams.IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    await image.SetSourceAsync(stream);
                }
                this.image.Source = image;

                _imgPath = copiedImgFile.Path;
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            MusicEntry entry = new MusicEntry();

            entry.musicFilePath = _mp3Path;
            entry.imageFilePath = _imgPath;


            this.Frame.Navigate(typeof(MainPage), entry);   // passing parameter to the target page
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".mp3");

            Windows.Storage.StorageFile musicfile = await picker.PickSingleFileAsync();

            
            if (musicfile != null)
            {
                // Save musicfile path in the txt 
                // Get the app's local folder to use as the destination folder.
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                // Copy the file to the destination folder and rename it.
                // Replace the existing file if the file already exists.
                StorageFile copiedMusicFile = await musicfile.CopyAsync(localFolder, musicfile.Name, NameCollisionOption.ReplaceExisting);

                this.TxtStrFile.Text = copiedMusicFile.Name;

                FileStream fs = new FileStream(copiedMusicFile.Path, FileMode.Open, FileAccess.Read, FileShare.Read); //
                MP3MetafileReader(fs);

                _mp3Path = copiedMusicFile.Path;
            }
        }
    }

}
