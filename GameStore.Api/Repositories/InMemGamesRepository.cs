using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> Games = new()
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

    public IEnumerable<Game> GetAll()
    {
        return Games;
    }

    public Game? Get(int id)
    {
        return Games.Find(game => game.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = Games.Max(game => game.Id) + 1;
        Games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = Games.FindIndex(game => game.Id == updatedGame.Id);
        Games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = Games.FindIndex(game => game.Id == id);
        Games.RemoveAt(index);
    }
}