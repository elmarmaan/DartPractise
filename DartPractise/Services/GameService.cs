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

        public long StartNewGame(GameType gameType, string emailAddress)
        {
            var player = _dartPractiseContext.Players.SingleOrDefault(p => p.EmailAddress == emailAddress);

            var score = 0;

            switch (gameType.Name)
            {
                case "170":
                    score = 170;
                    break;
            }
            

            var game = new Game
            {
                GameType = gameType,
                NumberOfDartsThrown = 0,
                IsFinished = false,
                Player = player,
                Score = score
            };

            var newGame = _dartPractiseContext.Games.Add(game);
            _dartPractiseContext.SaveChanges();

            return newGame.Id;
        }

        public Game SetScore(long gameId, int score)
        {
            var game = GetGame(gameId);

            if (!game.IsFinished)
            {
                game.NumberOfDartsThrown += 3;

                switch (game.GameType.Name)
                {
                    case "170":
                        if (game.Score - score <= 0)
                        {
                            game.Score = 0;
                            game.IsFinished = true;
                        }
                        else
                        {
                            game.Score -= score;
                        }
                        break;
                    case "Around the world":
                        game.Score += score;
                        if (game.NumberOfDartsThrown == 63)
                        {
                            game.IsFinished = true;
                        }
                        break;
                }
                _dartPractiseContext.SaveChanges();
            }
            return game;
        }
    }
}