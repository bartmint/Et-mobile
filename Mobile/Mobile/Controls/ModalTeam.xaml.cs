using Mobile.Models;
using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalTeam : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ModalTeam(Team team)
        {
            InitializeComponent();

            var context=(ModalTeamVM)BindingContext;
            context.Team = team;
        }
    }
}