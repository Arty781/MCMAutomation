using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    public class MembershipsWithUsersRequest
    {
        public static MembershipsWithUsersResponse GetMembershipsWithUsersList(SignInResponseModel SignIn)
        {


            var restDriver = new RestClient(Endpoints.apiHost);
            RestRequest? request = new RestRequest("/Admin/GetMembershipsWithUsers", Method.Get);
            request.AddHeaders(headers: Headers.HeadersCommon());
            request.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            var response = restDriver.Execute(request);
            var content = response.Content;
            var countdownResponse = JsonConvert.DeserializeObject<MembershipsWithUsersResponse>(content);

            return countdownResponse;
        }
    }
}
