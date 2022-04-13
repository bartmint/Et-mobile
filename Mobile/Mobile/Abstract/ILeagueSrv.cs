using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Abstract
{
    public interface ILeagueSrv
    {
        Task<List<League>> GetList();
        Task<List<League>> AddLeague(string url);
        Task UpdateLeague(int leagueId);
        Task UpdateLeagues();
        Task<List<League>> DeleteLeague(int leagueId);
    }
}
