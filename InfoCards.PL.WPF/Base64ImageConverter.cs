using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace InfoCards.PL.WPF
{
    public class Base64ImageConverter
    {
        public BitmapImage Base64ToImage(string base64String)
        {
            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(Convert.FromBase64String(base64String));
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public string ImageToBase64(string imageFilepath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(imageFilepath));
        }
    }
}