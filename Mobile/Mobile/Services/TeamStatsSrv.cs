using Mobile.Abstract;
using Mobile.Extensions;
using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class TeamStatsSrv : ApiSrv, ITeamStatsSrv
    {
        public async Task<List<TeamStats>> GetList(int leagueId)
        {
            string url = $"{Url}teamStatistics/list";
            try
            {
                var response = await client.GetAsync($"{url}/{leagueId}");
                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                return JsonExtensions.DeserializeCamelCase<List<TeamStats>>(responseAsString);
            }
            catch (Exception ex)
            {
                return new List<TeamStats>();
            }
        }
    }
}
