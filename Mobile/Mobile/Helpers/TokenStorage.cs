using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Helpers
{
    public static class TokenStorage
    {
        public static async void SetTokenAsync(string token)
        {
            await SecureStorage.SetAsync("token", token);
        }
        public static void RemoveToken()
        {
             SecureStorage.Remove("token");
        }
        public static async Task<string> GetTokenAsync()
        {
            return await SecureStorage.GetAsync("token");
        }
        public static async Task<bool> IsTokenExistAsync()
        {
            return !string.IsNullOrEmpty(await GetTokenAsync());
        }
    }
}
