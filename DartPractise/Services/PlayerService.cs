using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DartPractise.Contexts;
using Domain;

namespace DartPractise.Services
{
    public class PlayerService
    {
        private readonly DartPractiseContext _dartPractiseContext = new DartPractiseContext();

        public void AddPlayer(Player player)
        {
            var existingPlayer = _dartPractiseContext.Players.FirstOrDefault(p => p.EmailAddress == player.EmailAddress);

            if (existingPlayer == null)
            {
                _dartPractiseContext.Players.Add(player);
                _dartPractiseContext.SaveChanges();
            }
            else
            {
                throw new Exception("Player already exists!");
            }
        }

        public bool CheckPlayer(string emailAddress, string password)
        {
            var existingPlayer = _dartPractiseContext.Players.FirstOrDefault(p => p.EmailAddress == emailAddress && p.Password == password);
            if (existingPlayer != null) return true;
            return false;
        }

        public void UpdatePlayer(Player player, string emailaddress)
        {
            var existingPlayer = _dartPractiseContext.Players.FirstOrDefault(p => p.EmailAddress == player.EmailAddress);

            //check if specified emailaddress already exists
            if (_dartPractiseContext.Players.Any(p => p.EmailAddress == player.EmailAddress) && player.EmailAddress != emailaddress)
            {
                throw new Exception("Emailaddress already exists!");
            }

            if (existingPlayer != null)
            {
                existingPlayer.EmailAddress = player.EmailAddress;
                existingPlayer.Name = player.Name;
                existingPlayer.Password = player.Password;
                _dartPractiseContext.SaveChanges();
            }
            else
            {
                throw new Exception("You are trying to update the wrong player!");
            }
        }

        public Player GetPlayer(string emailAddress)
        {
            var player = _dartPractiseContext.Players.SingleOrDefault(p => p.EmailAddress == emailAddress);
            if(player == null) throw new Exception("Player not found!");
            return player;
        }
    }
}