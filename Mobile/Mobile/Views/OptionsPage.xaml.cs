using Mobile.Helpers;
using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
        }
        async void NavigateToAccount(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AccountPage());
        }
        async void NavigateToStats(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StatsPage());
        }
        async void NavigateToSignInPage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SignInPage());
        }
        async void NavigateToAdminPanel(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AdminPanelPage());
        }
        async void Logout(object sender, EventArgs args)
        {
            SecureStorage.RemoveAll();

            await Shell.Current.Navigation.PopAsync();
            await Shell.Current.GoToAsync("//Results");
        }
        async void NavigateToSignUpPage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is OptionsVM vmRQ)
            {
                bool isTokenExists = await TokenStorage.IsTokenExistAsync();
                bool isAdmin = await SecureStorage.GetAsync("isAdmin") == "True";

                vmRQ.TokenExists = isTokenExists;
                vmRQ.IsAdmin = isAdmin;
            }
        }
    }
}