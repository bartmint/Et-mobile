using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Queue
    {
        public int QueueNumber { get; set; }
        public List<Match> Queues { get; set; } = new List<Match>();
    }
}
