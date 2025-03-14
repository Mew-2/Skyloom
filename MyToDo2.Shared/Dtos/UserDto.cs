using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged(); }
        }
    }
}