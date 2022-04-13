using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Abstract
{
    public interface IMatchSrv
    {
        Task<Queue> GetList(int option, int leagueId, int? queue = null);
        Task<List<Match>> GetDailyList(int leagueId);
        Task UpdateMatch(MatchToUpdate match);
    }
}
