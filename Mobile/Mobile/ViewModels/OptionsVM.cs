using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.ViewModels
{
    public class OptionsVM : BaseViewModel
    {
        private bool _tokenExists;
        public bool TokenExists
        {
            get => _tokenExists;
            set { _tokenExists = value; OnPropertyChanged(); }
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        public OptionsVM()
        {
            IsAdmin = false;
        }
    }
}
