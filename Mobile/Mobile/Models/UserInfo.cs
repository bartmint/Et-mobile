using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class UserInfo
    {
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string FavLeagueName { get; set; }
    }
}
