using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PixivCSharp
{
    public partial class PixivClient
    {
        // Returns a list of resulting illusts for the given search term
        public async Task<IllustSearchResult> SearchIllustsAsync(string searchTerm, string sort ="date_desc",
            string searchTarget = "partial_match_for_tags", bool includeTranslatedTags = true,
            bool mergePlainKeywordResults = true, string filter = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "word", searchTerm },
                { "search_target", searchTarget },
                { "sort", sort },
                { "include_translated_tags", includeTranslatedTags.ToString().ToLower() },
                { "merge_plain_keywork_results", mergePlainKeywordResults.ToString().ToLower() }
            };
            
            // Adds filter if required
            string filterText = filter ?? Filter;
            if (filterText != "none" && (filterText == "for_android" || filterText == "for_ios"))
            {
                parameters.Add("filter", filter ?? Filter);
            }
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = await RequestClient.RequestAsync(PixivUrls.IllustSearch, encodedParams)
                .ConfigureAwait(false);

            string repsonseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            IllustSearchResult result = Json.DeserializeJson<IllustSearchResult>(repsonseContent);
            return result;
        }
        
        // Returns a list of resulting novels for the given search term
        public async Task<NovelSearchResult> SearchNovelsAsync(string searchTerm, string sort = "date_desc",
            string searchTarget = "partial_match_for_tags", bool includeTranslatedTags = true,
            bool mergePlainKeywordResults = true)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "word", searchTerm },
                { "search_target", searchTarget },
                { "sort", sort },
                { "include_translated_tags", includeTranslatedTags.ToString().ToLower() },
                { "merge_plain_keyword_results", mergePlainKeywordResults.ToString().ToLower() }
            };
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response =
                await RequestClient.RequestAsync(PixivUrls.NovelSearch, encodedParams).ConfigureAwait(false);

            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            NovelSearchResult result = Json.DeserializeJson<NovelSearchResult>(responseContent);
            return result;
        }
        
        // Returns a list of resulting users for the given search term
        public async Task<UserSearchResult> SearchUsersAsync(string searchTerm, string filter = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "word", searchTerm }
            };
            
            // Adds filter if required
            string filterText = filter ?? Filter;
            if (filterText != "none" && (filterText == "for_android" || filterText == "for_ios"))
            {
                parameters.Add("filter", filter ?? Filter);
            }
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response =
                await RequestClient.RequestAsync(PixivUrls.UserSearch, encodedParams).ConfigureAwait(false);

            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            UserSearchResult result = Json.DeserializeJson<UserSearchResult>(responseContent);
            return result;
        }
        
        // Returns a list of potential tags to autocomplete the given input
        public async Task<Tag[]> AutocompleteTagAsync(string searchTerm, bool mergePlainKeywordResults = true)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "word", searchTerm },
                { "merge_plain_keyword_results", mergePlainKeywordResults.ToString().ToLower() }
            };
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await RequestClient.RequestAsync(PixivUrls.AutocompleteTag, encodedParams)
                .ConfigureAwait(false);

            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            JObject json = JObject.Parse(responseContent);
            Tag[] result = Json.DeserializeJson<Tag[]>((JArray)json["tags"]);
            return result;
        }
        
        // Returns a list of popular illusts for the given search term, limited to a single page
        public async Task<IllustSearchResult> PopularIllustsPreviewAsync(string searchTerm, string sort = "date_desc",
            string searchTarget = "partial_match_for_tags", bool includeTranslatedTags = true,
            bool mergePlainKeywordResults = true, string filter = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"word", searchTerm},
                {"sort", sort},
                {"search_target", searchTarget},
                {"include_translated_tags", includeTranslatedTags.ToString().ToLower()},
                {"merge_plain_keyword_results", mergePlainKeywordResults.ToString().ToLower()}
            };
            
            // Adds filter if required
            string filterText = filter ?? Filter;
            if (filterText != "none" && (filterText == "for_android" || filterText == "for_ios"))
            {
                parameters.Add("filter", filter ?? Filter);
            }
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await RequestClient
                .RequestAsync(PixivUrls.PopularIllustsPreview, encodedParams).ConfigureAwait(false);

            string resposneContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            IllustSearchResult result = Json.DeserializeJson<IllustSearchResult>(resposneContent);
            return result;
        }
        
        // Returns a list of popular novels for the given search term, limied to a single page
        public async Task<NovelSearchResult> PopularNovelsPreviewAsync(string searchTerm, string sort = "date_desc",
            string searchTarget = "partial_match_for_tags", bool includeTranslatedTags = true,
            bool mergePlainKeywordResults = true)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "word", searchTerm },
                { "sort", sort },
                { "search_target", searchTarget},
                { "include_translated_tags", includeTranslatedTags.ToString().ToLower() },
                { "merge_plain_keyword_results", mergePlainKeywordResults.ToString().ToLower() }
            };
            FormUrlEncodedContent encodedParams = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await RequestClient
                .RequestAsync(PixivUrls.PopularNovelsPreview, encodedParams).ConfigureAwait(false);

            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            NovelSearchResult result = Json.DeserializeJson<NovelSearchResult>(responseContent);
            return result;
        }
    }
}