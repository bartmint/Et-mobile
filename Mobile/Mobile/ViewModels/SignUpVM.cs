using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Models;
using Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class SignUpVM : BaseViewModel
    {
        private readonly IUserSrv _userSrv;
        private RegistrationUser _user;
        public RegistrationUser User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }

        public SignUpVM()
        {
            User = new RegistrationUser();
            _userSrv = DependencyService.Get<IUserSrv>();
        }
       
        public ICommand RegisterUser
        {
            get
            {
                return new Command(Register);
            }

        }
        private async void Register(object sender)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());

            try
            {
                var user = User;
                user.UserName = User.Email;

                bool result = await _userSrv.Register(user);


                if (result)
                {
                    await PopupNavigation.Instance.PopAsync();

                    await Shell.Current.Navigation.PopAsync();
                }
            }
            
            finally
            {
                if (PopupNavigation.Instance.PopupStack.Count != 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }
            }

        }
    }
}
