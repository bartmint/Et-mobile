using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Helpers
{
    public static class CurrentLeagueStorageHandler
    {
        public static async Task<string> HandleCurrentLeague(List<League> leagues)
        {
            return await SecureStorage.GetAsync("League") ?? leagues.FirstOrDefault().Title;
        }
    }
}
