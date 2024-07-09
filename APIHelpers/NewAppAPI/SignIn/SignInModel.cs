using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.NewAppAPI.SignIn
{
    public class SignInModel
    {
        public partial class SignInResponse
        {
            [JsonProperty("content")]
            public Content Content { get; set; }

            [JsonProperty("isSuccess")]
            public bool IsSuccess { get; set; }

            [JsonProperty("responseCode")]
            public long ResponseCode { get; set; }

            [JsonProperty("errorMessage")]
            public object ErrorMessage { get; set; }
        }

        public partial class Content
        {
            [JsonProperty("userId")]
            public Guid UserId { get; set; }

            [JsonProperty("isOnboardingCompleted")]
            public bool IsOnboardingCompleted { get; set; }

            [JsonProperty("gender")]
            public long Gender { get; set; }

            [JsonProperty("measurementUnit")]
            public long MeasurementUnit { get; set; }

            [JsonProperty("accessToken")]
            public string AccessToken { get; set; }

            [JsonProperty("refreshToken")]
            public string RefreshToken { get; set; }
        }
    }
}
