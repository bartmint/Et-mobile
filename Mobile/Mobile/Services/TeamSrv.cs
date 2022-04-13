using Mobile.Abstract;
using Mobile.Extensions;
using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class TeamSrv:ApiSrv, ITeamSrv
    {
        public async Task<List<Team>> GetList(int leagueId)
        {
            string url = $"{Url}team/list";

            try
            {
                var response = await client.GetAsync($"{url}/{leagueId}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                return JsonExtensions.DeserializeCamelCase<List<Team>>(responseAsString);

            }
            catch (Exception ex)
            {
                return new List<Team>();
            }
        }

    }
}
