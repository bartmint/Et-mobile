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
    public partial class AdminPanelPage : ContentPage
    {
        public AdminPanelPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is AdminPanelVM vmRQ)
            {
                vmRQ.Initialize();
            }
        }

        private void UpdateLeague(object sender, EventArgs e)
        {
            var obs = (Button)sender;

            if (BindingContext is AdminPanelVM vmRQ)
            {
                vmRQ.UpdateLeagueHandler((int)obs.CommandParameter);
            }
        }

        private void DeleteLeague(object sender, EventArgs e)
        {
            var obs = (Button)sender;
            if (BindingContext is AdminPanelVM vmRQ)
            {
                vmRQ.DeleteLeagueHandler((int)obs.CommandParameter);
            }

        }
    }
}