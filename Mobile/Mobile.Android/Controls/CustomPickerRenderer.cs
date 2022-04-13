using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Mobile.Controls;
using Mobile.Droid.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLeaguePicker), typeof(CustomPickerRenderer))]
namespace Mobile.Droid.Controls
{
	public class CustomPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
	{
		CustomLeaguePicker element;
        public CustomPickerRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);
            Control.Background = null;

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            LayoutParameters = layoutParams;
            Control.LayoutParameters = layoutParams;
            Control.SetPadding(0, 0, 0, 0);
            SetPadding(0, 0, 0, 0);
            element = (CustomLeaguePicker)this.Element;
			
            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Icon))
                Control.Background = AddPickerStyles(element.Icon);

        }

		public LayerDrawable AddPickerStyles(string imagePath)
		{
			ShapeDrawable border = new ShapeDrawable();
			border.Paint.Color = Android.Graphics.Color.Transparent;

			Drawable[] layers = { border, GetDrawable(imagePath) };
			LayerDrawable layerDrawable = new LayerDrawable(layers);
			layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

			return layerDrawable;
		}

		private BitmapDrawable GetDrawable(string imagePath)
		{
			int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
			var drawable = ContextCompat.GetDrawable(this.Context, resID);
			var bitmap = ((BitmapDrawable)drawable).Bitmap;

			var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 20, 20, true));
			result.Gravity = GravityFlags.Right;

			return result;
		}

	}
}