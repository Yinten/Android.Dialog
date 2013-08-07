using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Graphics;
using Java.IO;

namespace Android.Dialog
{
    public class ImageUtility
    {
        public static void SaveImage(Bitmap bitmap, String fileName)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);                                                                         
            byte[] byteArray = stream.GetBuffer();


            FileOutputStream fo = new FileOutputStream(fileName, false);  

            fo.Write(byteArray);
          
        }

        public static Bitmap LoadImage(String fileName)
        {
            return BitmapFactory
                .DecodeFile(fileName);

        }
    }
}

