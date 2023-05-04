# Translator App, React Native

A language translator app powered by Chat GPT.



```c#
public async Task<string> GetResponseAsync(string message)
    {
        var requestBody = CreateRequestBody(message);

        using var response = await _httpClient.PostAsync(GptApiUrl, new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();

        var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        string responseText = Convert.ToString(result.choices[0].text);

        return responseText.Replace(message, "").Trim();
    }
```

 Coming soon...