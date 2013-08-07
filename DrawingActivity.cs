using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;

namespace Android.Dialog
{
    [Activity (Label = "DrawingActivity", Theme = "@android:style/Theme.NoTitleBar.Fullscreen")]
    public class DrawingActivity : Activity
    {

        public static readonly string DRAWING_LOCATION_INTENT = "DrawingLocation";
        public static string BACKGROUND_FILE_PATH = Android.OS.Environment.ExternalStorageDirectory + File.Separator + "drawing_image_reservered_location.png";
        private LinearLayout _signatureLayout; 
        private DrawingView _signatureDrawingView;
        private string drawingLocation;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            drawingLocation = Intent.GetStringExtra(DRAWING_LOCATION_INTENT);


            SetContentView(Resource.Layout.drawing_field);
            _signatureLayout = FindViewById<LinearLayout>(Resource.Id.drawingfield_drawingview);
            _signatureDrawingView = new DrawingView(this, drawingLocation);
            _signatureLayout.AddView(_signatureDrawingView); 
            Button saveButton = FindViewById<Button>(Resource.Id.drawingfield_save);
            Button clearButton = FindViewById<Button>(Resource.Id.drawingfield_clear);


            saveButton.Click += delegate
            {
                _signatureDrawingView.SaveImage(drawingLocation); 

                Finish(); 
            };

            clearButton.Click += delegate
            {
                _signatureDrawingView.ClearImage();
            };
        }
    }
}

