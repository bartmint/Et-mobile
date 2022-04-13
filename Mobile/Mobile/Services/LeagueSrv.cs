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
    public class LeagueSrv : ApiSrv, ILeagueSrv
    {
        public async Task<List<League>> AddLeague(string u)
        {
            string url = $"{Url}league/add";

            try
            {
                var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(u), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                return JsonExtensions.DeserializeCamelCase<List<League>>(responseAsString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<League>> GetList()
        {
            string url = $"{Url}league/list";

            try
            {
                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                return JsonExtensions.DeserializeCamelCase<List<League>>(responseAsString);

            }
            catch (Exception ex)
            {
                return new List<League>();
            }
        }
        public async Task<List<League>> DeleteLeague(int leagueId)
        {
            string url = $"{Url}league/delete";

            try
            {
                var response = await client.GetAsync($"{url}/{leagueId}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                return JsonExtensions.DeserializeCamelCase<List<League>>(responseAsString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateLeague(int leagueId)
        {
            string url = $"{Url}league/update";

            try
            {
                var response = await client.GetAsync($"{url}/{leagueId}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateLeagues()
        {
            string url = $"{Url}league/updateLeagues";

            try
            {
                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
