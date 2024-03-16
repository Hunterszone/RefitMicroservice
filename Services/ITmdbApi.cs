using Refit;
using RefitMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitMicroservice.Services
{
    [Headers("accept: application/json",
             "Authorization: Bearer")]
    public interface ITmdbApi
    {
        [Get("/search/person?query={name}")]
        Task<ActorList> GetActors(string name);

        [Get("/person/{id}/movie_credits")]
        Task<MovieList> GetMoviesByActorId([AliasAs("id")] string actorId);
    }
}
