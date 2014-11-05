using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DartPractise.Mappers;
using DartPractise.Services;
using Domain;

namespace DartPractise.Controllers
{
    public class GameController : Controller
    {
        private GameService gameService = new GameService();

        public ActionResult Index()
        {
            var games = gameService.GetGames(User.Identity.Name);
            ViewData.Model = games;
            return PartialView();
        }

        public ActionResult OneHundredAndSeventyOverView()
        {
            var games = gameService.GetGames(User.Identity.Name);
            var orderedGames = games.Where(g => g.GameType.Name == "170").OrderBy(o => o.IsFinished).ThenByDescending(o => o.CreationDateTime);

            var bestGame = orderedGames.Aggregate((c, d) => c.NumberOfDartsThrown < d.NumberOfDartsThrown ? c : d);

            ViewData["Best"] = bestGame;
            ViewData.Model = orderedGames;
            return View();
        }

        public ActionResult AroundTheWorldOverView()
        {
            var games = gameService.GetGames(User.Identity.Name);
            var orderedGames = games.Where(g => g.GameType.Name == "Around the world").OrderBy(o => o.IsFinished).ThenByDescending(o => o.CreationDateTime);

            var bestGame = new Game();
            if (orderedGames.Count() > 0)
            {
                bestGame = orderedGames.Aggregate((c, d) => c.Score > d.Score ? c : d);
            }
            

            ViewData["Best"] = bestGame;
            ViewData.Model = orderedGames;
            return View();
        }

        [HttpGet]
        public ActionResult Start()
        {
            ViewData.Model = gameService.GetGameTypes();
            return View();
        }

        [HttpPost]
        public ActionResult Start(long gameTypeId)
        {
            var gameType = gameService.GetGameType(gameTypeId);
            var gameId = gameService.StartNewGame(gameType, User.Identity.Name);
            return Continue(gameId);
        }

        public ActionResult Continue(long id)
        {
            var game = gameService.GetGame(id);
            ViewData.Model = game;
            return View("GameView");
        }

        public ActionResult Delete(long id)
        {
            gameService.DeleteGame(id);
            return RedirectToAction("Index");
        }

        public JsonResult SetScore(long gameId, int score)
        {
            var game = gameService.SetScore(gameId, score);
            return Json(new
            {
                Score = game.Score,
                NumberOfDartsThrown = game.NumberOfDartsThrown,
                IsFinished = game.IsFinished
            });
        }
    }
}