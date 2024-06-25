using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Jewelry_store_management.HELPER
{
    public class Base64ToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string base64String = value as string;
                if (string.IsNullOrEmpty(base64String))
                {
                    return new BitmapImage(new Uri("/Drawable/Images/Logo.png", UriKind.Relative));
                }


                byte[] imageBytes = System.Convert.FromBase64String(base64String);
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }
                    return bitmapImage;
                

            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây (logging, thông báo cho người dùng, ...)
                Console.WriteLine($"Error converting Base64 to BitmapImage: {ex.Message}");
                return new BitmapImage(new Uri("/Drawable/Images/Logo.png", UriKind.Relative));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
