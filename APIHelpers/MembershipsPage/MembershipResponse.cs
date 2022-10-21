using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    [JsonObject]
    public class MembershipsWithUsersResponse
    {
        [JsonProperty("memberships")]
        public List<Membership> Memberships { get; set; }

        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }

    [JsonObject]
    public partial class Membership
    {
        [JsonProperty("membershipId")]
        public long MembershipId { get; set; }

        [JsonProperty("membershipName")]
        public string MembershipName { get; set; }

        [JsonProperty("membershipUsers")]
        public List<MembershipUser> MembershipUsers { get; set; }
    }

    [JsonObject]
    public partial class MembershipUser
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("userMembershipId")]
        public long UserMembershipId { get; set; }
    }

    [JsonObject]
    public partial class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }


}
