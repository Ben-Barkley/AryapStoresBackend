using System.Text;
using Newtonsoft.Json;
using Aryap.Shared.Constants;   



namespace Aryap.Shared.Services.ApiCaller
{
    public class ApiCaller : IApiCaller
    {
        public async Task<string> GetAsync(string url, IDictionary<string, string> headers)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.GeneralErrors.InvalidInput);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode && response.StatusCode.ToString().ToLower() == "ok")
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception($"Failed to fetch data. Status Code: {response.StatusCode}");
                    }
                }
            }

            return apiResponse;
        }

        public async Task<string> PostAsync(string url, object content, IDictionary<string, string> headers)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.GeneralErrors.InvalidInput);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                var serializedContent = new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    HTMLConstants.APPLICATION_CONTENT_TYPE_JSON
                );

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                using (var response = await httpClient.PostAsync(url, serializedContent))
                {
                    if (!response.IsSuccessStatusCode && response.StatusCode.ToString().ToLower() == "paymentrequired")
                    {
                        apiResponse = "paymentrequired";
                    }
                    else if (response.IsSuccessStatusCode && response.StatusCode.ToString().ToLower() == "ok")
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception($"Failed to post data. Status Code: {response.StatusCode}");
                    }
                }
            }

            return apiResponse;
        }

        public async Task<TResult> PostAsync<TResult>(string url, List<KeyValuePair<string, string>> postData, IDictionary<string, string> headers, string headerAccept = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.GeneralErrors.InvalidInput);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                if (!string.IsNullOrEmpty(headerAccept))
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", headerAccept);
                }

                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();

                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    using (HttpResponseMessage response = await httpClient.PostAsync(url, content))
                    {
                        if (response.IsSuccessStatusCode &&
                            (response.StatusCode.ToString().ToLower() == "created" || response.StatusCode.ToString().ToLower() == "ok"))
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            throw new Exception($"Failed to post data. Status Code: {response.StatusCode}");
                        }
                    }
                }
            }

            var deserializedResponse = JsonConvert.DeserializeObject<TResult>(apiResponse);
            return deserializedResponse;
        }
    }
}