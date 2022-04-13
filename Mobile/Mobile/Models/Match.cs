using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Match: BasicMatch
    {
        public int Id { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public BasicMatch PrevMatch { get; set; }
    }
}
