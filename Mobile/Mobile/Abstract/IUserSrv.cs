using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Abstract
{
    public interface IUserSrv
    {
        Task<bool> Register(RegistrationUser user); 
        Task<bool> Login(User user); 
        Task<bool> UpdatePreferences(int leagueId); 
    }
}
