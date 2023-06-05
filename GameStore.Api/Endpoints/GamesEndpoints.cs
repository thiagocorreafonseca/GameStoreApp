using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndPointName = "GetGame";
        static List<Game> Games = new()
        {
            new Game()
            {
                Id = 1,
                Name = "Street Figther II",
                Genre = "Fight",
                Price = 19.99M,
                ReleaseDate = new DateTime(1991, 2, 1),
                ImageUri = "https://placehold.co/100"
            },
            new Game()
            {
                Id = 2,
                Name = "Final Fantasy XIV",
                Genre = "RolePlaying",
                Price = 59.99M,
                ReleaseDate = new DateTime(2010, 9, 30),
                ImageUri = "https://placehold.co/100"
            },
            new Game()
            {
                Id = 3,
                Name = "FIFA 23",
                Genre = "Sports",
                Price = 69.99M,
                ReleaseDate = new DateTime(2022, 9, 27),
                ImageUri = "https://placehold.co/100"
            }
        };


        public static RouteGroupBuilder MapGamesEndpoint(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/games")
                        .WithParameterValidation();

            group.MapGet("/", () => Games);
            group.MapGet("/{id}", (int id) =>
            {
                Game? game = Games.Find(game => game.Id == id);

                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(game);
            }).WithName(GetGameEndPointName);
            group.MapPost("/", (Game game) =>
            {
                game.Id = Games.Max(game => game.Id) + 1;
                Games.Add(game);

                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            }
            );

            group.MapPut("/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = Games.Find(game => game.Id == id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                return Results.NoContent();
            }
            );

            group.MapDelete("/{id}", (int id) =>
            {
                Game? existingGame = Games.Find(game => game.Id == id);

                if (existingGame is not null)
                {
                    Games.Remove(existingGame);
                }

                return Results.NoContent();
            });

            return group;
        }
    }
}