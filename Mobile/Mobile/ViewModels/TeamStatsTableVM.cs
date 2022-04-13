using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Helpers;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class TeamStatsTableVM : BaseViewModel
    {
        private readonly ITeamStatsSrv _teamStatsSrv;
        private readonly ITeamSrv _teamInfoSrv;
        private readonly ILeagueSrv _leagueSrv;

        private ObservableCollection<TeamStats> _teamsStats;
        public ObservableCollection<TeamStats> TeamsStats
        {
            get => _teamsStats;
            set { _teamsStats = value; OnPropertyChanged(); }
        }
        private string _info;
        public string Info
        {
            get => _info;
            set { _info = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _leagues = new ObservableCollection<string>();
        public ObservableCollection<League> LeaguesCollection { get; set; }
        private ObservableCollection<Team> _teamsInfo;

        public ObservableCollection<Team> TeamsInfo
        {
            get => _teamsInfo;
            set { _teamsInfo = value; OnPropertyChanged(); }
        }
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
        public ObservableCollection<string> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        public TeamStatsTableVM()
        {
            _teamStatsSrv = DependencyService.Get<ITeamStatsSrv>();
            _teamInfoSrv = DependencyService.Get<ITeamSrv>();
            _leagueSrv = DependencyService.Get<ILeagueSrv>();
        }
        public async Task SetTeamsStats()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var league = GetLeagueById();

                var teams =  await _teamStatsSrv.GetList(league.Id);

                TeamsStats = new ObservableCollection<TeamStats>(teams);

                string title = league.Title;

                Info = title;
            }

            finally{
                await PopupNavigation.Instance.PopAsync();
            }
            
        }
        public async Task SetTeamsInfo()
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var league = GetLeagueById();

                var teamsInfo =  await _teamInfoSrv.GetList(league.Id);

                TeamsInfo = new ObservableCollection<Team>(PrepareTeamsToView(teamsInfo));
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }

        }
        public void Initialize()
        {
            Task.Run(async () => {
                await SetLeagues();
                await SetTeamsStats();
                await SetTeamsInfo();
            });
        }
        public ICommand RowClicked
        {
            get
            {
                return new Command(ShowModal);
            }

        }
        private async void ShowModal(object sender)
        {
            try
            {
                string teamName = (string)sender;
                Team t = GetTeamByName(teamName);

                await PopupNavigation.Instance.PushAsync(new ModalTeam(t));

            }
            catch (Exception ex)
            {
                return;
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
        private Team GetTeamByName(string name)
        {
            return TeamsInfo.Where(x => x.Name == name).FirstOrDefault();
        }
        private List<Team> PrepareTeamsToView(List<Team> teams)
        {
            return teams.Select(x => new Team
            {
                Name = !string.IsNullOrEmpty(x.Name) ? x.Name : "-",
                YearOfFundation = !string.IsNullOrEmpty(x.YearOfFundation) ? x.YearOfFundation : "-",
                Colors = !string.IsNullOrEmpty(x.Name) ? x.Colors : "-",
                PageWWW = !string.IsNullOrEmpty(x.PageWWW) ? x.PageWWW : "-",
                Phone = !string.IsNullOrEmpty(x.Phone) ? x.Phone : "-",
                Email = !string.IsNullOrEmpty(x.Email) ? x.Email : "-",
                Address = !string.IsNullOrEmpty(x.Address) ? x.Address : "-",
                Photo = x.Photo,

            }).ToList();
        }
        public async Task HandleLeagueChange(string selectedItem)
        {
            if (selectedItem != null && selectedItem != CurrentLeague)
            {
                CurrentLeague = selectedItem;
                await SetTeamsInfo();
                await SetTeamsStats();
            }
        }

    }
}
