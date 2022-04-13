using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is AccountVM vmRQ)
            {
                vmRQ.Initialize();
            }
        }
        private void LeaguePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pck = (Picker)sender;

            if (BindingContext is AccountVM vmRQ)
            {
                vmRQ.HandleLeagueChange(pck.SelectedItem as string);
            }
        }
    }
}