using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;


namespace ImageTools
{

    public class FrameBuffer : IDisposable
    {
        //From: https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }


        //From: https://stackoverflow.com/questions/26260654/wpf-converting-bitmap-to-imagesource/26261562#26261562
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public System.Windows.Media.ImageSource ImageSource {
            get
            {
                var handle = Bitmap.GetHbitmap();
                try
                {
                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                }
                finally { DeleteObject(handle); }
            }
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            Bits = new int[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject()); 
        }

        public FrameBuffer(Bitmap bmp)
        {
            Width = bmp.Width;
            Height = bmp.Height;
            Bits = new int[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height);

            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.DrawImage(bmp, 0, 0, Width, Height);
            }
        }

        //From: https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
        public void SetPixel(int x, int y, Color c)
        {
             
            int index = x + y * Width;
            int color = c.ToArgb();

            Bits[index] = color;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + y * Width;
            return Color.FromArgb(Bits[index]);
        }

        public void FillImage(int x, int y, FrameBuffer buffer)
        {
            for(int i = 0; i < buffer.Width; i++)
            {
                for(int j = 0; j < buffer.Height; j++)
                {
                    SetPixel(i + x, j + y, buffer.GetPixel(x, y));
                }
            }
        }

        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
