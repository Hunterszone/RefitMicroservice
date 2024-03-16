using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RefitMicroservice.Models
{
    public class MovieList
    {
        [JsonPropertyName("cast")]
        public List<Movie> Movies { get; set; }
    }
}
