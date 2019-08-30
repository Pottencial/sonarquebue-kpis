using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SonarQubeDashBoard.Repositories
{
    public class SonarQubeRepository
    {
        string _personalToken;
        public SonarQubeRepository(string personalToken)
        {
            _personalToken = personalToken;
        }

        public virtual async Task<T> GetData<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _personalToken, ""))));

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return default;

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }

    }
}
