using Newtonsoft.Json;

public class ApiResult<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("messsage")] 
    public string? Message { get; set; }

    [JsonProperty("data")]
    public T? Data { get; set; }
}
