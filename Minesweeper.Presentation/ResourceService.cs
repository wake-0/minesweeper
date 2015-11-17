using Minesweeper.Presentation.Properties;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper.Presentation
{
    public static class ResourceService
    {
        public static ImageSource FlagImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.flag.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }
    }
}
