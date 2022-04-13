using Mobile.Abstract;
using Mobile.Helpers;
using Mobile.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //dependency injection
            DependencyService.Register<IApiSrv, ApiSrv>();
            DependencyService.Register<IMatchSrv, MatchSrv>();
            DependencyService.Register<ILeagueSrv, LeagueSrv>();
            DependencyService.Register<ITeamSrv, TeamSrv>();
            DependencyService.Register<ITeamStatsSrv, TeamStatsSrv>();
            DependencyService.Register<IUserSrv, UserSrv>();
        }

    }
}
