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
    public class ModalMatchVM: BaseViewModel
    {
        private readonly IMatchSrv _matchSrv;

        private Match _match;
        public Match Match
        {
            get => _match;
            set { _match = value; OnPropertyChanged(); }
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged(); }
        }
        private MatchToUpdate _updatedMatch;
        public MatchToUpdate UpdatedMatch
        {
            get => _updatedMatch;
            set { _updatedMatch = value; OnPropertyChanged(); }
        }
        public ModalMatchVM()
        {
            _matchSrv = DependencyService.Get<IMatchSrv>();
  
        }
        public ICommand UpdateMatchCommand
        {
            get
            {
                return new Command(UpdateMatchHandler);
            }
        }
        private async void UpdateMatchHandler(object sender)
        {
            await PopupNavigation.Instance.PushAsync(new ModalIndicator());
            try
            {
                int matchId = (int)sender;
                UpdatedMatch.Id = matchId;

                await _matchSrv.UpdateMatch(UpdatedMatch);
            }

            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
