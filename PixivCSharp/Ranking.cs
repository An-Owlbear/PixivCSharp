using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PixivCSharp
{
    public partial class PixivClient
    {
        // Gets a list of ranking illusts or manga
        public async Task<IllustSearchResult> RankingIllustsAsync(string mode = "day", DateTime? date = null,
            string filter = null)
        {
            // Converts time to the correct timezone and produces a date string in correct format
            DateTime dateValue = date ?? TimeZoneInfo.ConvertTime(DateTime.Now, JapanTimeZone).AddDays(-1);
            string dateString = dateValue.Year.ToString("0000") + "-" + dateValue.Month.ToString("00")
                                + "-" + dateValue.Day.ToString("00");

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "mode", mode },
                { "date", dateString }
            };
            
            // Adds filter if required
            string filterText = filter ?? Filter;
            if (filterText != "none" && (filterText == "for_android" || filterText == "for_ios"))
            {
                parameters.Add("filter", filter ?? Filter);
            }
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await RequestClient.RequestAsync(PixivUrls.RankingIllusts, encodedParams)
                .ConfigureAwait(false);

            string resposneString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            IllustSearchResult result = Json.DeserializeJson<IllustSearchResult>(resposneString);
            return result;
        }
        
        // Gets a list of ranking novels
        public async Task<NovelSearchResult> RankingNovelsAsync(string mode = "day", DateTime? date = null)
        {
            // Converts time to the correct timezone and produces a date string in correct format
            DateTime dateValue = date ?? TimeZoneInfo.ConvertTime(DateTime.Now, JapanTimeZone).AddDays(-1);
            string dateString = dateValue.Year.ToString("0000") + "-" + dateValue.Month.ToString("00")
                                + "-" + dateValue.Day.ToString("00");
            
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "mode", mode },
                { "date", dateString }
            };
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await RequestClient.RequestAsync(PixivUrls.RankingNovels, encodedParams)
                .ConfigureAwait(false);

            string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            NovelSearchResult result = Json.DeserializeJson<NovelSearchResult>(responseString);
            return result;
        }
    }
}