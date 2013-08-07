using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace Android.Dialog
{
    public class DrawingElement : Element
    {
        // Height for rows
        const int dimx = 400;
        const int dimy = 300;
        // radius for rounding
        const int roundPx = 12;
        Bitmap backgroundBitmap;
        Bitmap drawingBitmap;
        string drawingLocation;
        string fieldLabel;

        public DrawingElement(string fieldLabel,
                              Bitmap backgroundBitmap,
                              string drawingLocation)
            : base(string.Empty)
        {
            this.fieldLabel = fieldLabel; 
            this.backgroundBitmap = backgroundBitmap; 
            this.drawingLocation = drawingLocation; 
        }


        private View MakeEmpty(Context context)
        {

       
            LayoutInflater layoutInflater = LayoutInflater.FromContext(context); 
            View curView = (View)layoutInflater.Inflate(Resource.Layout.drawing_element,
                                                        null); 

            return curView;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (backgroundBitmap != null)
                    backgroundBitmap.Dispose();
                if (drawingBitmap != null)
                    drawingBitmap.Dispose(); 
            }
            base.Dispose(disposing);
        }

        /* C# doens't support Tagging 
        private class ViewHolder
        {
            public TextView labelTV;
            public ImageView drawingIV;
        }
*/ 

        public void InitializeView(View row)
        {
            /** C# doesn't support setTag methods, fixed in next patch. 
            ViewHolder viewHolder = new ViewHolder();
            viewHolder.labelTV = (TextView) row.FindViewById(Resource.Id.drawing_element_textview);
            viewHolder.drawingIV = (ImageView) row.FindViewById(Resource.Id.drawing_element_imageview);
            row.SetTag(viewHolder);  */
        }



        public void SetValues(View row)
        {
            //TODO:  Need suppport for tagging to reuse these views.
            TextView labelTV = (TextView) row.FindViewById(Resource.Id.drawing_element_textview);
            ImageView drawingIV = (ImageView) row.FindViewById(Resource.Id.drawing_element_imageview);


            labelTV.SetText(fieldLabel, TextView.BufferType.Normal);

            /* TODO: should only be loaded when it is changed */ 
            drawingBitmap = ImageUtility.LoadImage(this.drawingLocation);

            if (drawingBitmap != null)
            {
                drawingIV.SetImageBitmap(drawingBitmap); 
            }
            else
            {
                drawingIV.SetImageBitmap(backgroundBitmap); 
            }

            /* C# doesn't support tagging
            ViewHolder vh = (ViewHolder) row.GetTag();
            vh.labelTV.SetText(fieldLabel, TextView.BufferType.Normal);
            drawingBitmap = ImageUtility.LoadImage(this.drawingLocation);
            
            if (drawingBitmap != null)
            {
                vh.drawingIV.SetImageBitmap(drawingBitmap); 
                drawingBitmap.Recycle(); 
                drawingBitmap = null; 

            }
            else
            {
                vh.drawingIV.SetImageBitmap(backgroundBitmap); 
            }
            */ 
        }


        public override View GetView(Context context,
                                     View convertView,
                                     ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = MakeEmpty(context); 
                InitializeView(row);
                SetValues(row); 

            } else
            {
                SetValues(row); 
            }
            Click = delegate
            {
                DrawImage();
            };

            return row;
           
        }

        public void DrawImage()
        {
            ImageUtility.SaveImage(backgroundBitmap,
                                   DrawingActivity.BACKGROUND_FILE_PATH); 
            Intent drawImageIntent = new Intent(GetContext(),
                                                typeof(DrawingActivity)); 
            drawImageIntent.PutExtra(DrawingActivity.DRAWING_LOCATION_INTENT,
                                     drawingLocation); 


            GetContext().StartActivity(drawImageIntent); 
        }
    }
}