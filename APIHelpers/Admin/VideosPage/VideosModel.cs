using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCMAutomation.APIHelpers.Admin.VideosPage
{
    public partial class Videos
    {
        public class EditVideoRequest
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("Url")]
            public string Url { get; set; }

            [JsonProperty("IsForAllMemberships")]
            public bool IsForAllMemberships { get; set; }

            [JsonProperty("CategoryIds")]
            public List<int>? CategoryIds { get; set; }

            [JsonProperty("TagIds")]
            public List<int>? TagIds { get; set; }

            [JsonProperty("MembershipIds")]
            public List<int>? MembershipIds { get; set; }
        }

        public class VideoFilterReq
        {
            [JsonProperty("Skip")]
            public int Skip { get; set; }

            [JsonProperty("Take")]
            public int Take { get; set; }

            [JsonProperty("Name")]
            public string? Name { get; set; }

            [JsonProperty("IsForAllMemberships")]
            public bool IsForAllMemberships { get; set; }

            [JsonProperty("categoryIds")]
            public List<int>? CategoryIds { get; set; }

            [JsonProperty("tagIds")]
            public List<int>? TagIds { get; set; }

            [JsonProperty("MembershipIds")]
            public List<int>? MembershipIds { get; set; }
        }
    }
}
