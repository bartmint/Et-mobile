using Mobile.Models;
using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalMatch : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ModalMatch(Match match, bool isAdmin)
        {
            InitializeComponent();

            var context = (ModalMatchVM)BindingContext;

            context.Match=match;
            context.IsAdmin = isAdmin;

            MatchToUpdate updatedMatch = new MatchToUpdate
            {
                AwayTeamGoals = match.AwayTeamGoals.HasValue ? match.AwayTeamGoals.Value : 0,
                HomeTeamGoals = match.AwayTeamGoals.HasValue ? match.HomeTeamGoals.Value : 0,
            };
            context.UpdatedMatch = updatedMatch;
        }
    }
}