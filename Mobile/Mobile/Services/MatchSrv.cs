using Mobile.Abstract;
using Mobile.Extensions;
using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class MatchSrv:ApiSrv, IMatchSrv
    {

        public async Task<Queue> GetList(int option, int leagueId, int? queue=null)
        {
            string url = $"{Url}Match/queue";
            try
            {
                var response = await client.GetAsync($"{url}/{option}/{leagueId}/{queue}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                //nie mapuje nuli np przy golach
                return JsonExtensions.DeserializeCamelCase<Queue>(responseAsString);

            }
            catch(Exception ex)
            {
                return new Queue();
            }
        }
        public async Task<List<Match>> GetDailyList(int leagueId)
        {
            string url = $"{Url}Match/today/list";
            try
            {
                var response = await client.GetAsync($"{url}/{leagueId}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                //nie mapuje nuli np przy golach
                return JsonExtensions.DeserializeCamelCase<List<Match>>(responseAsString);

            }
            catch (Exception ex)
            {
                return new List<Match>();
            }
        }

        public async Task UpdateMatch(MatchToUpdate match)
        {
                string url = $"{Url}Match/updateMatch";

            try
            {
                var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(match), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
            }
        }
    } 
}
