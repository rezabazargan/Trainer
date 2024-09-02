using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Trainer.App.Model;

namespace Trainer.App.Services
{
    public class AuthenticationService(HttpClient httpClient)
    {
        private const string TokenKey = "token";
        public static async Task<bool> IsAuthenticated()
        {
            var authDataString = await SecureStorage.GetAsync(TokenKey);
            if (authDataString == null)
                return false;
            var authData = JsonSerializer.Deserialize<AuthenticationData>(authDataString);
            if(authData == null)
                return false;

            if (authData.ExpireOn > DateTime.Now) return true;
            SecureStorage.Remove(TokenKey);
            return false;

        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await httpClient.PostAsJsonAsync(ApplicationSettings.loginUrl, new
            {
                Username = username,
                Password = password
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
                SecureStorage.SetAsync(TokenKey, JsonSerializer.Serialize(new AuthenticationData()
                {
                    Token = result.Token,
                    ExpireOn = DateTime.Now.AddDays(7)
                }));
                return true;
            }

            return false;
        }
    }

    class AuthenticationResult
    {
        public required string Token { get; set; }
    }
}
