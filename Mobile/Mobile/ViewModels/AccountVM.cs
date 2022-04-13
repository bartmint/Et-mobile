using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{

    public class AccountVM : BaseViewModel
    {
        private readonly ILeagueSrv _leagueSrv;
        private readonly IUserSrv _userSrv;
        private UserInfo _userInfo;
        public UserInfo UserInfo
        {
            get => _userInfo;
            set { _userInfo = value; OnPropertyChanged(); }
        }
        private string _changedLeague;
        public string ChangedLeague
        {
            get => _changedLeague;
            set { _changedLeague = value; OnPropertyChanged(); }
        }
        private bool _showUpdateButton;
        public bool ShowUpdateButton
        {
            get => _showUpdateButton;
            set { _showUpdateButton = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _leagues = new ObservableCollection<string>();
        public ObservableCollection<League> LeaguesCollection { get; set; }
        public ObservableCollection<string> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        public AccountVM()
        {
            _leagueSrv = DependencyService.Get<ILeagueSrv>();
            _userSrv = DependencyService.Get<IUserSrv>();
            ShowUpdateButton = false;
            ChangedLeague = null;
        }
        public void Initialize()
        {
            ShowUpdateButton = false;
            ChangedLeague = null;

            Task.Run(async () =>
            {
                await SetUser();
                await SetLeagues();
            });
        }
        private async Task SetUser()
        {
            UserInfo userInfo = new UserInfo
            {
                IsAdmin = await SecureStorage.GetAsync("isAdmin") == "True",
                Email = await SecureStorage.GetAsync("Email"),
                FavLeagueName = await SecureStorage.GetAsync("League")
            };
            UserInfo = userInfo;
        }
        private async Task SetLeagues()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var leagues = await _leagueSrv.GetList();

                Leagues = new ObservableCollection<string>(leagues.Select(x => x.Title).ToList());
                LeaguesCollection = new ObservableCollection<League>(leagues);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public void HandleLeagueChange(string selectedItem)
        {
            if (selectedItem != null && selectedItem != UserInfo.FavLeagueName)
            {
                ChangedLeague = selectedItem;
                ShowUpdateButton = true;
            }
        }
        public ICommand UpdatePreferences
        {
            get
            {
                return new Command(UpdatePreferencesHandler);
            }
        }
        public async void UpdatePreferencesHandler(object leagueTitle)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                int leagueId = LeaguesCollection.Where(x=>x.Title == leagueTitle.ToString()).FirstOrDefault().Id;

                await _userSrv.UpdatePreferences(leagueId);

                var us = new UserInfo
                {
                    Email = UserInfo.Email,
                    FavLeagueName = UserInfo.FavLeagueName,
                    IsAdmin = true,
                };

                UserInfo = us;
                ShowUpdateButton = false;
                ChangedLeague = null;
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
