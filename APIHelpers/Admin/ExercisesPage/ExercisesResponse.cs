using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCMAutomation.APIHelpers
{

    public class RequestAddExercises
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("videoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("tempoBold")]
        public int TempoBold { get; set; }

        [JsonProperty("relatedExercises")]
        public List<RelatedExerciseRequest> RelatedExercises { get; set; }
    }

    public class RelatedExerciseRequest
    {
        [JsonProperty("exerciseId")]
        public int? ExerciseId { get; set; }

        [JsonProperty("exerciseType")]
        public int? ExerciseType { get; set; }
    }

    public class RequestEditExercises
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("videoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("tempoBold")]
        public int TempoBold { get; set; }

        [JsonProperty("relatedExercises")]
        public List<RelatedExerciseRequest> RelatedExercises { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }
    }

    public class ResponseGetExercises
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("videoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("tempoBold")]
        public int TempoBold { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("relatedExercises")]
        public List<ResponseRelatedExercise> RelatedExercises { get; set; }
    }
    public class ResponseRelatedExercise
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("videoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("tempoBold")]
        public int TempoBold { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
