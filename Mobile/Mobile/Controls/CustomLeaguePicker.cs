using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mobile.Controls
{
    public class CustomLeaguePicker:Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomLeaguePicker), string.Empty);

        public string Icon
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
