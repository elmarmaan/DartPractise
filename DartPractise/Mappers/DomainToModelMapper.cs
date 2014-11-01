using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DartPractise.Models;

namespace DartPractise.Mappers
{
    public static class DomainToModelMapper
    {
        public static GamesFrontEndModel Map(IEnumerable<Domain.Game> games)
        {
            var gamesFrontEndModel = new GamesFrontEndModel();
            gamesFrontEndModel.Games = new List<GameFronteEndModel>();
            foreach (var game in games)
            {
                gamesFrontEndModel.Games.Add(Map(game));
            }

            return gamesFrontEndModel;
        }

        private static GameFronteEndModel Map(Domain.Game game)
        {
            return new GameFronteEndModel
            {
                Id = game.Id,
                GameType = Map(game.GameType),
                NumberOfDartsThrown = game.NumberOfDartsThrown,
                IsFinished = game.IsFinished,
                CreationDateTime = game.CreationDateTime
            };
        }

        private static GameTypeFrontEndModel Map(Domain.GameType gameType)
        {
            return new GameTypeFrontEndModel
            {
                Id = gameType.Id,
                Name = gameType.Name
            };
        }
    }
}