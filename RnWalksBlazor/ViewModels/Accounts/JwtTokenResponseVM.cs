using System.Text.Json.Serialization;

namespace RnWalksBlazor.ViewModels.Accounts
{
    public class JwtTokenResponseVM
    {
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }
    }
}
