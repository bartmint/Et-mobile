using Mobile.Abstract;
using Mobile.Controls;
using Mobile.Helpers;
using Mobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class ResultsQueueVM : BaseViewModel
    {
        //services
        private readonly IMatchSrv _matchSrv;
        private readonly ILeagueSrv _leagueSrv;

        //commands
        private Command buttonClickedCommand;

        //data
        private int _queue;
        public int Queue
        {
            get => _queue;
            set { _queue = value; OnPropertyChanged(); }
        }
        private string _queueInfo;
        public string QueueInfo
        {
            get => _queueInfo;
            set { _queueInfo = value; OnPropertyChanged(); }
        }
        public string _currentLeague;
        public string CurrentLeague
        {
            get => _currentLeague;
            set {
                _currentLeague = value;
                OnPropertyChanged(); }
        }
        private ObservableCollection<Match> _matches;
        public ObservableCollection<Match> Matches
        {
            get => _matches;
            set { _matches = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _leagues;
        public ObservableCollection<League> LeaguesCollection { get; set; }
        public ObservableCollection<string> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        private bool _isFetching=false;
        public bool IsFetching
        {
            get => _isFetching;
            set { _isFetching = value; OnPropertyChanged();}
        }

        public ResultsQueueVM()
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
        //2 means current weekend
        //leaugeId = getUserPreferedLeague
        private async Task SetMatches(int? queue = null, int option = 2)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                var league = GetLeagueById();
                var matches = await _matchSrv.GetList(option, league.Id, queue);

                Matches = new ObservableCollection<Match>(matches.Queues);
                Queue = matches.QueueNumber;
                QueueInfo = $"Kolejka {matches.QueueNumber}\n{ league.Title }";
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
                //tutaj bedziemy mieli uzytkownika jeszcze w storze, jesli nie to wybierzemy pierwsza lepsza wartosc
                CurrentLeague = await CurrentLeagueStorageHandler.HandleCurrentLeague(leagues);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public ICommand ButtonClickedCommand
        {
            get
            {
                if (buttonClickedCommand == null)
                {
                    buttonClickedCommand = new Command(ButtonClicked);
                }

                return buttonClickedCommand;
            }
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
                int matchId = (int)sender;
                Match m = GetMatchById(matchId);

                bool isAdmin = await SecureStorage.GetAsync("isAdmin") == "True";

                await PopupNavigation.Instance.PushAsync(new ModalMatch(m, isAdmin));

            }
            catch (Exception ex)
            {
                return;
            }
            
        }
        private async void ButtonClicked(object sender)
        {
            switch (sender.ToString())
            {
                case "previous":
                    {
                        if (Queue <= 1)
                            return;

                        await SetMatches(Queue, 1);
                        break;
                    }
                case "actual":
                    {
                        await SetMatches();
                        break;
                    }
                case "next":
                    {
                        await SetMatches(Queue, 3);
                        break;
                    }
                default:
                    return;
            }
        }
        private League GetLeagueById()
        {
            return LeaguesCollection.Where(x => x.Title == CurrentLeague).FirstOrDefault();
        }
        private Match GetMatchById(int id)
        {
            return Matches.Where(x => x.Id == id).FirstOrDefault();
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
