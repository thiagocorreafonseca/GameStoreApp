using GamesStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndPointName = "GetGame";
        public static RouteGroupBuilder MapGamesEndpoint(this IEndpointRouteBuilder routes)
        {
            //InMemGamesRepository repository = new();

            var group = routes.MapGroup("/games")
                        .WithParameterValidation();

            group.MapGet("/", (IGamesRepository repository) => repository.GetAll().Select(game => game.AsDto()));
            group.MapGet("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? game = repository.Get(id);
                return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
            })
            .WithName(GetGameEndPointName);

            group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                repository.Create(game);
                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            }
            );

            group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGameDto.Name;
                existingGame.Genre = updatedGameDto.Genre;
                existingGame.Price = updatedGameDto.Price;
                existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
                existingGame.ImageUri = updatedGameDto.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            }
            );

            group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is not null)
                {
                    repository.Delete(id);
                }

                return Results.NoContent();
            });

            return group;
        }
    }
}