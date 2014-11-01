using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DartPractise.Contexts;
using Domain;

namespace DartPractise.Services
{
    public class GameService
    {
        private readonly DartPractiseContext _dartPractiseContext = new DartPractiseContext();
        private readonly PlayerService _playerService = new PlayerService();

        public IList<GameType> GetGameTypes()
        {
            return _dartPractiseContext.GameTypes.ToList();
        }

        public GameType GetGameType(long gameTypeId)
        {
            var gameType = _dartPractiseContext.GameTypes.SingleOrDefault(g => g.Id == gameTypeId);

            if(gameType == null) throw new Exception("No GameType Found!");

            return gameType;
        }

        public IList<Game> GetGames(string emailAddress)
        {
            var player = _playerService.GetPlayer(emailAddress);
            return player.Games;
        }

        public Game GetGame(long id)
        {
            var game = _dartPractiseContext.Games.SingleOrDefault(g => g.Id == id);
            if(game == null) throw new Exception("Game not found!");

            return game;
        }

        public void DeleteGame(long id)
        {
            var game = _dartPractiseContext.Games.SingleOrDefault(g => g.Id == id);
            if(game == null) throw new Exception("Game not found!");

            _dartPractiseContext.Games.Remove(game);
            _dartPractiseContext.SaveChanges();
        }

        public void StartNewGame(GameType gameType, string emailAddress)
        {
            var player = _dartPractiseContext.Players.SingleOrDefault(p => p.EmailAddress == emailAddress);
            var game = new Game
            {
                GameType = gameType,
                NumberOfDartsThrown = 0,
                IsFinished = false,
                Player = player
            };

            _dartPractiseContext.Games.Add(game);
            _dartPractiseContext.SaveChanges();
        }
    }
}