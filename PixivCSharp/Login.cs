﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PixivCSharp
{
    public partial class PixivClient
    {
        private WebRequests requestClient;

        public PixivClient()
        {
            requestClient = new WebRequests();
        }

        public bool CheckTokens()
        {
            if (requestClient.access_token == null || requestClient.refresh_token == null ||
                requestClient.device_token == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public async Task<IllustSearchResult> WalkthoughIllusts()
        {
            //Retrieves walkthrough illusts and converts to json, and then returns it as IllustSearchResult
            HttpResponseMessage response = await requestClient.Request(PixivUrls.WalkthroughIllusts).ConfigureAwait(false);
            JObject json = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            IllustSearchResult result = json.ToObject<IllustSearchResult>();
            return result;
        }

        public async Task<EmojiList> EmojiList()
        {
            //Retrieves emoji list, converts to json, and returns as search result object
            HttpResponseMessage response = await requestClient.Request(PixivUrls.GetEmoji).ConfigureAwait(false);
            JObject json = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            EmojiList result = json.ToObject<EmojiList>();
            return result;
        }

        //Login/refresh method
        public async Task<LoginResponse> Login(string username, string password)
        {
            //Parameter dictionary
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "client_id", "MOBrBDS8blbauoSck0ZfDbtuzpyT" },
                { "client_secret", "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj" },
                { "grant_type", "password" },
                { "username", username },
                { "password", password },
                { "device_token", "pixiv" },
                { "secure_url", "true" },
                { "include_policy", "true" }
            };

            //Login request is sent, converted to json and returned
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = await requestClient.Request(PixivUrls.Login, encodedParams).ConfigureAwait(false);
            JObject json = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            LoginResponse result = json["response"].ToObject<LoginResponse>();
            requestClient.setTokens(result.access_token, result.refresh_token, result.device_token);
            return result;
            
          
        }

        public async Task<LoginResponse> RefreshLogin()
        {
            //Parameter dictionary
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "client_id", "MOBrBDS8blbauoSck0ZfDbtuzpyT" },
                { "client_secret", "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj" },
                { "grant_type", "refresh_token" },
                { "refresh_token", requestClient.refresh_token },
                { "device_token", requestClient.device_token },
                { "secure_url", "true" },
                { "include_policy", "true" }
            };
            
            //Refresh request is sent, and new token is retrieved
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = await requestClient.Request(PixivUrls.Login, encodedParams).ConfigureAwait(false);
            JObject json = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            LoginResponse result = json["response"].ToObject<LoginResponse>();
            requestClient.setTokens(result.access_token, result.refresh_token, result.device_token);
            return result;
        }
    }
}