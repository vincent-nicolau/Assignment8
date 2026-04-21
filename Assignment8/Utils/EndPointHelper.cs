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

            app.MapGet("/movie/{id:int}", GetMovieById)
                .WithName("GetMovieById");

            app.MapPost("/movie", CreateMovie)
                .WithName("CreateMovie");

            app.MapPut("/movie/{id:int}", UpdateMovie)
                .WithName("UpdateMovie");

            app.MapDelete("/movie/{id:int}", DeleteMovie)
                .WithName("DeleteMovie");
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

        public static async Task<IResult> GetMovieById(int id, IMovieRepo repo)
        {
            try
            {
                Movie? movie = await repo.GetByIdAsync(id);

                if (movie == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(movie);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        }

        public static async Task<IResult> CreateMovie(Movie movie, IMovieRepo repo)
        {
            try
            {
                await repo.AddAsync(movie);
                await repo.SaveAsync();

                return Results.Created($"/movie/{movie.Id}", movie);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        }

        public static async Task<IResult> UpdateMovie(int id, Movie movie, IMovieRepo repo)
        {
            try
            {
                Movie? existingMovie = await repo.GetByIdAsync(id);

                if (existingMovie == null)
                {
                    return Results.NotFound();
                }

                movie.Id = id;

                repo.Update(movie);
                await repo.SaveAsync();

                return Results.Ok(movie);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        }

        public static async Task<IResult> DeleteMovie(int id, IMovieRepo repo)
        {
            try
            {
                Movie? movie = await repo.GetByIdAsync(id);

                if (movie == null)
                {
                    return Results.NotFound();
                }

                repo.Delete(movie);
                await repo.SaveAsync();

                return Results.Ok(movie);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        }
    }
}