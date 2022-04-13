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
    public partial class DailyMatchesPage : ContentPage
    {
        public DailyMatchesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is DailyMatchesVM vmRQ)
            {
                vmRQ.Initialize();
            }
        }

        private async void LeaguePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pck = (Picker)sender;

            if (BindingContext is DailyMatchesVM vmRQ)
            {
                await vmRQ.HandleLeagueChange(pck.SelectedItem as string);
            }
        }
    }
}