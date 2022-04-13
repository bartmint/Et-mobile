using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Abstract
{
    public interface ITeamStatsSrv
    {
        Task<List<TeamStats>> GetList(int leagueId);
    }
}
