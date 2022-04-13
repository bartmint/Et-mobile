using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AdminPanelVM : BaseViewModel
    {
        private readonly ILeagueSrv _leagueSrv;
        private ObservableCollection<League> _leagues;
        public ObservableCollection<League> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        private string _leagueToAdd;
        public string LeagueToAdd
        {
            get => _leagueToAdd;
            set { _leagueToAdd = value; OnPropertyChanged(); }
        }
        private bool _display;
        public bool Display
        {
            get => _display;
            set { _display = value; OnPropertyChanged(); }
        }
        private bool _isNotificationVisible;
        public bool IsNotificationVisible
        {
            get => _isNotificationVisible;
            set { _isNotificationVisible = value; OnPropertyChanged(); }
        }
        private string _notificationMessage;
        public string NotificationMessage
        {
            get => _notificationMessage;
            set { _notificationMessage = value; OnPropertyChanged(); }
        }
        private string _notificationColor;
        public string NotificationColor
        {
            get => _notificationColor;
            set { _notificationColor = value; OnPropertyChanged(); }
        }
        public AdminPanelVM()
        {
            _leagueSrv = DependencyService.Get<ILeagueSrv>();

            Display = false;
            IsNotificationVisible = false;
        }
        public async Task<IEnumerable<League>> GetList()
        {
            return await _leagueSrv.GetList();
        }
        public void Initialize()
        {
            Task.Run(async () =>
            {
                await SetLeagues();
            });
        }
        private async Task SetLeagues()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var leagues = await _leagueSrv.GetList();
                Leagues = new ObservableCollection<League>(leagues);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public ICommand AddLeague
        {
            get
            {
                return new Command(AddLeagueHandler);
            }
        }
        public ICommand ClearLeague
        {
            get
            {
                return new Command(ClearLeagueHandler);
            }
        }
        public ICommand DisplayLeagues
        {
            get
            {
                return new Command(DisplayLeaguesHandler);
            }
        }
        public ICommand UpdateAllLeagues
        {
            get
            {
                return new Command(UpdateAllLeaguesHandler);
            }
        }
        public async void HandleNotification(bool succeded)
        {
            IsNotificationVisible = true;
            NotificationColor = succeded ? "Green" : "Red";
            NotificationMessage = succeded ? "Operacja zakończyła się sukcesem" : "Wystąpił błąd";

            await Task.Delay(4000);

            IsNotificationVisible = false;
        }
        public async void UpdateAllLeaguesHandler()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                await _leagueSrv.UpdateLeagues();
                HandleNotification(true);
            }
            catch (Exception ex)
            {
                HandleNotification(false);
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public void DisplayLeaguesHandler()
        {
            Display = !Display;
        }
        public void ClearLeagueHandler()
        {
            LeagueToAdd = null;
        }
        public async void UpdateLeagueHandler(int leagueId)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                await _leagueSrv.UpdateLeague(leagueId);
                HandleNotification(true);
            }
            catch(Exception ex)
            {
                HandleNotification(true);
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public async void DeleteLeagueHandler(int leagueId)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var leagues = await _leagueSrv.DeleteLeague(leagueId);
                Leagues = new ObservableCollection<League>(leagues);

                HandleNotification(true);

            }
            catch (Exception ex)
            {
                HandleNotification(false);
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public async void AddLeagueHandler(object sender)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var leagues = await _leagueSrv.AddLeague(LeagueToAdd);
                Leagues = new ObservableCollection<League>(leagues);

                HandleNotification(true);
            }
            catch (Exception ex)
            {
                HandleNotification(false);
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
