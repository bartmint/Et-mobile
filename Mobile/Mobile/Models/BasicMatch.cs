using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class BasicMatch
    {
        public string Date { get; set; }
        public string Time { get; set; }

        public int? HomeTeamGoals { get; set; }
        public int? AwayTeamGoals { get; set; } 
    }
}
