using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Models;
using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ResultsQueueVM vmRQ)
            {
                vmRQ.Initialize();
            }
        }

        private async void LeaguePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pck = (Picker)sender;
            
            if (BindingContext is ResultsQueueVM vmRQ)
            {
                await vmRQ.HandleLeagueChange(pck.SelectedItem as string);
            }
        }
    }
}