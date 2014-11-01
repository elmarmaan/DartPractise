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
        private DartPractiseContext dartPractiseContext = new DartPractiseContext();

        public void AddPlayer(Player player)
        {
            var existingPlayer = dartPractiseContext.Players.FirstOrDefault(p => p.EmailAddress == player.EmailAddress);

            if (existingPlayer == null)
            {
                dartPractiseContext.Players.Add(player);
                dartPractiseContext.SaveChanges();
            }
            else
            {
                throw new Exception("Player already exists!");
            }
        }

        public bool CheckPlayer(string emailAddress, string password)
        {
            var existingPlayer = dartPractiseContext.Players.FirstOrDefault(p => p.EmailAddress == emailAddress && p.Password == password);
            if (existingPlayer != null) return true;
            return false;
        }

        public void UpdatePlayer(Player player)
        {
            var existingPlayer = dartPractiseContext.Players.FirstOrDefault(p => p.Id == player.Id);

            //check if specified emailaddress already exists
            if (dartPractiseContext.Players.Any(p => p.EmailAddress == player.EmailAddress) && existingPlayer.EmailAddress != player.EmailAddress)
            {
                throw new Exception("Emailaddress already exists!");
            }

            if (existingPlayer != null)
            {
                existingPlayer.EmailAddress = player.EmailAddress;
                existingPlayer.Name = player.Name;
                existingPlayer.Password = player.Password;
                dartPractiseContext.SaveChanges();
            }
            else
            {
                throw new Exception("You are trying to update the wrong player!");
            }
        }
    }
}