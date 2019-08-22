using System;
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

            // Get the app's local folder to use as the destination folder.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // Copy the file to the destination folder and rename it.
            // Replace the existing file if the file already exists.
            StorageFile copiedImgFile = await imgfile.CopyAsync(localFolder, imgfile.Name, NameCollisionOption.ReplaceExisting);

            if (imgfile != null)
            {
                this.TxtStrImg.Text = copiedImgFile.Name;
                this.image.Source = new BitmapImage(new Uri("ms-appx:///" + this.TxtStrImg.Text));
            }
            else
            {
                this.TxtStrImg.Text = "Operation cancelled";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save music file path and cover image file path in txt format
            // show "saved"


            // collect data from controls to music entry here

            MusicEntry entry = new MusicEntry();

            //entry.musicFilePath = blabla;
            //entry.imageFilePath = blablbla;


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

            // Get the app's local folder to use as the destination folder.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // Copy the file to the destination folder and rename it.
            // Replace the existing file if the file already exists.
            StorageFile copiedMusicFile = await musicfile.CopyAsync(localFolder, musicfile.Name, NameCollisionOption.ReplaceExisting);

            if (musicfile != null)
            {
                 // Save musicfile path in the txt 
            }
            else
            {
                // show the file is empty
            }

        }
    }

}
