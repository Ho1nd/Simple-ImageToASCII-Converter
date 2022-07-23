using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASC2
{
    internal class Program
    {
        private const double WIDTH_OFFSET = 1.5;
        private const int MAX_WIDTH = 500;

        [STAThread]
        static void Main(string[] args)
        {
            //char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', '%', 'S', '#', '@' };

            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Нажми enter чтобы начать\n");

            while (true)
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayScale();

                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);
                
                Console.SetCursorPosition(0, 0);

                Console.ReadLine();
            }

        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
