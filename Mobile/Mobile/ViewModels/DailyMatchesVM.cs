using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Helpers;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class DailyMatchesVM: BaseViewModel
    {
        private readonly IMatchSrv _matchSrv;
        private readonly ILeagueSrv _leagueSrv;

        private ObservableCollection<Match> _matches = new ObservableCollection<Match>();
        public ObservableCollection<Match> Matches
        {
            get => _matches;
            set { _matches = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _leagues = new ObservableCollection<string>();
        public ObservableCollection<League> LeaguesCollection { get; set; }
        public string _currentLeague;
        public string CurrentLeague
        {
            get => _currentLeague;
            set
            {
                _currentLeague = value;
                OnPropertyChanged();
            }
        }
        private string _info;
        public string Info
        {
            get => _info;
            set { _info = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }

        public DailyMatchesVM()
        {
            _matchSrv = DependencyService.Get<IMatchSrv>();
            _leagueSrv = DependencyService.Get<ILeagueSrv>();
        }
        public void Initialize()
        {
            Task.Run(async () =>
            {
                await SetLeagues();
                await SetMatches();
            });
        }
        private async Task SetMatches()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var league = GetLeagueById();
                var matches = await _matchSrv.GetDailyList(league.Id);
                var date = CurrentDatePL.GetCurrentDate();

                Info = $"{date}\n{league.Title}";

                Matches = new ObservableCollection<Match>(matches);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }

        }
        private async Task SetLeagues()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var leagues = await _leagueSrv.GetList();

                Leagues = new ObservableCollection<string>(leagues.Select(x => x.Title).ToList());
                LeaguesCollection = new ObservableCollection<League>(leagues);
                CurrentLeague = await CurrentLeagueStorageHandler.HandleCurrentLeague(leagues);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        private League GetLeagueById()
        {
                return LeaguesCollection.Where(x => x.Title == CurrentLeague).FirstOrDefault();
        }
        public async Task HandleLeagueChange(string selectedItem)
        {
            if (selectedItem != null && selectedItem != CurrentLeague)
            {
                CurrentLeague = selectedItem;
                await SetMatches();
            }
        }
    }
}
