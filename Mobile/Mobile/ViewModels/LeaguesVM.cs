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
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class LeaguesVM: BaseViewModel
    {
        private readonly ILeagueSrv _leagueSrv;
        private ObservableCollection<League> _leagues;
        public ObservableCollection<League> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        public LeaguesVM()
        {
            _leagueSrv = DependencyService.Get<ILeagueSrv>();
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
        
    }
}
