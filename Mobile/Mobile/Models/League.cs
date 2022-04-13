using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class League
    {
        public int Id { get; set; }
        public string UrlForHtml { get; set; }
        public string Title { get; set; }
        public string PercentageOfMatchesPlayed { get; set; }
        public int TeamsAmmount { get; set; }
        public List<Herb> Herbs { get; set; }
    }
}
