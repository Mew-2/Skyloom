using MyToDo2.Common;
using MyToDo2.Extensions;
using MyToDo2.Services;
using MyToDo2.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        #region 绑定属性

        private string account;

        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return account; }
            set { SetProperty(ref account, value); }
        }

        private string password;
        private readonly ILoginService loginService;
        private readonly IEventAggregator aggregator;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        private RegisterUserDto registerUser;

        public RegisterUserDto RegisterUser
        {
            get { return registerUser; }
            set { SetProperty(ref registerUser, value); }
        }

        #endregion 绑定属性

        public string Title { get; set; } = "云衢";

        public DelegateCommand<string> ExcuteCommand { get; set; }

        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            ExcuteCommand = new DelegateCommand<string>(Excute);
            this.loginService = loginService;
            this.aggregator = aggregator;
            RegisterUser = new RegisterUserDto();
        }

        private void Excute(string args)
        {
            switch (args)
            {
                case "Login": Login(); break;
                case "LoginOut": LogOut(); break;
                case "Register": Register(); break;
                case "Go": SelectedIndex = 1; break;
                case "Return": SelectedIndex = 0; break;
            }
        }

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(RegisterUser.Account) ||
                string.IsNullOrWhiteSpace(RegisterUser.Password) ||
                string.IsNullOrWhiteSpace(RegisterUser.Username) ||
                string.IsNullOrWhiteSpace(RegisterUser.ConfirmPassword))
            {
                aggregator.SendMessage("选项不能为空！", "Login");
                return;
            }

            if (RegisterUser.Password != RegisterUser.ConfirmPassword)
            {
                aggregator.SendMessage("密码不一致！", "Login");
                return;
            }

            var registerResult = await loginService.RegisterAsync(new UserDto()
            {
                Account = RegisterUser.Account,
                Username = RegisterUser.Username,
                Password = RegisterUser.Password,
                Avatar = ""
            });

            if (registerResult != null && registerResult.Status)
            {
                aggregator.SendMessage("注册成功！", "Login");
                SelectedIndex = 0;
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password))
            {
                aggregator.SendMessage("账户和密码不能为空！", "Login");
                return;
            }

            var loginResult = await loginService.LoginAsync(new Shared.Dtos.UserDto()
            {
                Account = Account,
                Password = Password,
                Avatar = "",
                Username = ""
            });

            if (loginResult.Status)
            {
                AppSession.UserName = loginResult.Result.Username;
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                aggregator.SendMessage("登录失败，账户或密码错误！", "Login");
            }
        }

        private void LogOut()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.No));
        }

        #region 对话框接口

        public DialogCloseListener RequestClose { get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion 对话框接口
    }
}