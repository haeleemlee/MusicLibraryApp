using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

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


        //private string _entityId;
        private string _filePath;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //_entityId = e.Parameter as string;
            _filePath = e.Parameter as string;


            /*
            //FileStream fs = new FileStream("C:\\Users\\andycom\\Music\\Test.mp3", FileMode.Open);
            FileStream fs = new FileStream("Test.mp3", FileMode.Open, FileAccess.Read, FileShare.Read); // working
            //FileStream fs = new FileStream("onesong.mp3", FileMode.Open, FileAccess.Read, FileShare.Read);
            */

            FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read); // working
            MP3MetafileReader(fs);
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

/*
        private async void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] b = new byte[128];

            //FileStream fs = new FileStream("C:\\Users\\andycom\\Music\\Test.mp3", FileMode.Open);
            FileStream fs = new FileStream("Test.mp3", FileMode.Open, FileAccess.Read, FileShare.Read); // working
            //FileStream fs = new FileStream("onesong.mp3", FileMode.Open, FileAccess.Read, FileShare.Read);
            fs.Seek(-128, SeekOrigin.End);
            fs.Read(b, 0, 128);
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
            StorageFile copiedFile = await imgfile.CopyAsync(localFolder, imgfile.Name, NameCollisionOption.ReplaceExisting);

            // get path of imgfile
            
            if (imgfile != null)
            {
                
                this.image.Source = new BitmapImage(new Uri("ms-appx:///" + this.TxtStrImg.Text));
            }
            else
            {
                this.TxtStrImg.Text = "Operation cancelled";
            }
        }
        */
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
