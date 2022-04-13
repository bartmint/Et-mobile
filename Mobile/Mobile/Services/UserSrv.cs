using Mobile.Abstract;
using Mobile.Extensions;
using Mobile.Helpers;
using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Services
{
    public class UserSrv : ApiSrv, IUserSrv
    {
        public async Task<bool> Register(RegistrationUser user)
        {
            string url = $"{Url}Account/register";

            try
            {
                var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                UserInfo userInfo = JsonExtensions.DeserializeCamelCase<UserInfo>(responseAsString);

                await SetStorage(userInfo, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Login(User user)
        {
            string url = $"{Url}Account/login";

            try
            {
                var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                UserInfo userInfo = JsonExtensions.DeserializeCamelCase<UserInfo>(responseAsString);

                await SetStorage(userInfo, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task SetStorage(UserInfo userInfo, bool withToken)
        {
            if(withToken)
                await SecureStorage.SetAsync("token", userInfo.Token);
            await SecureStorage.SetAsync("isAdmin", userInfo.IsAdmin.ToString());
            await SecureStorage.SetAsync("Email", userInfo.Email);
            await SecureStorage.SetAsync("League", userInfo.FavLeagueName);
        }

        public async Task<bool> UpdatePreferences(int leagueId)
        {
            string url = $"{Url}Account/updatePreferences";

            try
            {
                string email = await SecureStorage.GetAsync("Email");

                var response = await client.GetAsync($"{url}/{leagueId}/{email}");

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();

                UserInfo userInfo = JsonExtensions.DeserializeCamelCase<UserInfo>(responseAsString);

                await SetStorage(userInfo, false);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
