using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCMAutomation.APIHelpers.Client.WeightTracker
{
    public class WeightTrackerRequest
    {
        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("take")]
        public int Take { get; set; }

        [JsonProperty("conversionSystem")]
        public int ConversionSystem { get; set; }

    }

    public class WeightTrackerResponse
    {
        public int id { get; set; }
        public string date { get; set; }
        public decimal weight { get; set; }
        public float averageWeight { get; set; }
        public float changeWeek { get; set; }
    }

}
