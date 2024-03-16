using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RefitMicroservice.Models
{
    public class ActorList
    {
        [JsonPropertyName("results")]
        public List<Actor> Actors { get; set; }
    }
}
