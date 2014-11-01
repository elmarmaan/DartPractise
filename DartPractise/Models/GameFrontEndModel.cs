using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DartPractise.Models
{
    public class GameFronteEndModel
    {
        public long Id { get; set; }
        public int NumberOfDartsThrown { get; set; }
        public GameTypeFrontEndModel GameType { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}