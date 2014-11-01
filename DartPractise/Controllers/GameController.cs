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
            gameService.StartNewGame(gameType, User.Identity.Name);
            return View();
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

        public void SetScore(long gameId, int score)
        {
            throw new NotImplementedException();
        }
    }
}