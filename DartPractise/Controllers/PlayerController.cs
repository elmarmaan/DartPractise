using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DartPractise.Services;
using Domain;
using Microsoft.Ajax.Utilities;

namespace DartPractise.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerService playerService = new PlayerService();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Player player)
        {
            playerService.AddPlayer(player);
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public void Login(Player player)
        {
            string returnUrl = Request.QueryString["ReturnUrl"] ?? "~/Home";
            if (playerService.CheckPlayer(player.EmailAddress, player.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(player.EmailAddress, false);
                Response.Redirect(returnUrl);
            }
            else
            {
                Response.Redirect(returnUrl);
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            try
            {
                playerService.UpdatePlayer(player, User.Identity.Name);
                return View("Index", "Home");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("EmailAddress", exception.Message);
                return View();
            }
        }
    }
}