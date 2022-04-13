using Foundation;
using Mobile.Controls;
using Mobile.iOS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomLeaguePicker), typeof(CustomPickerRenderer))]
namespace Mobile.iOS.Controls
{
    public class CustomPickerRenderer:PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomLeaguePicker)Element;

            //if (this.Control != null && this.Element != null)
            //{
            //    var downarrow = UIImage.FromBundle(element.Image);
            //    Control.RightViewMode = UITextFieldViewMode.Always;
            //    Control.RightView = new UIImageView(downarrow);
            //}
            element.TextColor = Color.Black;
        }
    }
}