using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    
    public class SignInVM : BaseViewModel
    {
        private readonly IUserSrv _userSrv;
        private User _user;
        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }
        public SignInVM()
        {
            User = new User();
            _userSrv = DependencyService.Get<IUserSrv>();
        }
        public ICommand LoginUser
        {
            get
            {
                return new Command(Login);
            }

        }
        private async void Login(object sender)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());

            try
            {
                var user = User;
                user.UserName = User.Email;

                bool result = await _userSrv.Login(user);


                if (result)
                {
                    await PopupNavigation.Instance.PopAsync();
                 
                    await Shell.Current.Navigation.PopAsync();
                }
            }

            finally
            {
                if(PopupNavigation.Instance.PopupStack.Count != 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }
            }

        }
    }
}
