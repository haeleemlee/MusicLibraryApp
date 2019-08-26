using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MusicLibaryApp
{
    public static class FileHelper
    {
        public async static void WriteTextFileAsync(string filename, string content)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var textFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            // var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            // var textWriter = new DataWriter(textStream);
            // textWriter.WriteString(content);
            File.AppendAllLines(textFile.Path, new string[] { content });

            // await textWriter.StoreAsync();
        }
        public async static Task<string> ReadTextFileAsync(string filename)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var textFile = await storageFolder.GetFileAsync(filename);
            var textStream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textStream);
            var textLength = textStream.Size;
            await textReader.LoadAsync((uint)textLength);
            return textReader.ReadString((uint)textLength);
        }
    }
}

