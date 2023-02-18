using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneLoopDAL.Models
{
    // Movie myDeserializedClass = JsonConvert.DeserializeObject<Movie>(myJsonResponse);
    public class Movie
    {
        [JsonPropertyName("_id")]
        public string _id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("runtimeInMinutes")]
        public int runtimeInMinutes { get; set; }

        [JsonPropertyName("budgetInMillions")]
        public int budgetInMillions { get; set; }

        [JsonPropertyName("boxOfficeRevenueInMillions")]
        public double boxOfficeRevenueInMillions { get; set; }

        [JsonPropertyName("academyAwardNominations")]
        public int academyAwardNominations { get; set; }

        [JsonPropertyName("academyAwardWins")]
        public int academyAwardWins { get; set; }

        [JsonPropertyName("rottenTomatoesScore")]
        public double rottenTomatoesScore { get; set; }
    }

}
