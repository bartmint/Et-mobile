using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.ViewModels
{
    public class ModalTeamVM:BaseViewModel
    {
        private Team _team;
        public Team Team
        {
            get => _team;
            set { _team = value; OnPropertyChanged(); }
        }
    }
}
