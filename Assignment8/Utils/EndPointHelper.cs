using Assignment8.Data;
using Assignment8.Models;

namespace Assignment8.Utils
{
    public static class EndPointHelper
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGet("/movies", GetAllMovies)
                .WithName("GetAllMovies");
        }

        public static async Task<IResult> GetAllMovies(IMovieRepo repo)
        {
            try
            {
                IEnumerable<Movie> movies = await repo.GetAllAsync();
                return Results.Ok(movies);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        }
    }
}