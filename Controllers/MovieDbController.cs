using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RefitMicroservice.Models;
using RefitMicroservice.Services;
using Newtonsoft.Json;

namespace RefitMicroservice.Controllers
{
    [Route("/api")]
    [ApiController]
    public class MovieDbController : ControllerBase
    {
        private readonly ITmdbApi _tmdbApi;

        public MovieDbController(ITmdbApi tmdbApi)
        {
            this._tmdbApi = tmdbApi;
        }

        [HttpGet("actors")]
        public async Task<IActionResult> GetActors([FromQuery][Required] string name)
        {
            var response = await _tmdbApi.GetActors(name);

            var actorNames = response.Actors
                .Where(person => person.Department == "Acting")
                .Select(person => person.Name)
                .Distinct()
                .ToList();

            if (actorNames.Any())
            {
                string allActors = JsonConvert.SerializeObject(actorNames);
                return new OkObjectResult(allActors);
            }

            return new NotFoundObjectResult("The person/group belongs to another department!");
        }

        [HttpGet("{actorId}/movies")]
        public async Task<List<string>> GetMoviesByActorId([Required] string actorId)
        {
            List<string> movieTitles = new List<string>();
            
            var response = await _tmdbApi.GetMoviesByActorId(actorId);
            
            response
                .Movies
                .ForEach(movie => movieTitles.Add(movie.Title));
            
            return movieTitles;
        }

    }
}
