using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    public class SignInResponseModel
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("refreshToken")]
        public object RefreshToken { get; set; }

        [JsonProperty("expiresIn")]
        public long ExpiresIn { get; set; }

        [JsonProperty("isProgramsImported")]
        public bool IsProgramsImported { get; set; }

        [JsonProperty("importResponseMessage")]
        public object ImportResponseMessage { get; set; }

        [JsonProperty("conversionSystem")]
        public long ConversionSystem { get; set; }
    }

    public class SignInRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
