using GameStore.Api.Entities;

const string GetGameEndPointName = "GetGame";

List<Game> Games = new()
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/games", () => Games);
app.MapGet("/games/{id}", (int id) =>
{
    Game? game = Games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
}).WithName(GetGameEndPointName);


app.MapPost("/games", (Game game) =>
{
    game.Id = Games.Max(game => game.Id) + 1;
    Games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
}
);


app.Run();
