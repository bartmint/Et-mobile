using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Abstract
{
    public interface ITeamSrv
    {
        Task<List<Team>> GetList(int leagueId);
    }
}
