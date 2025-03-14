using MyToDo2.Common;
using MyToDo2.Services;
using MyToDo2.ViewModels;
using MyToDo2.ViewModels.Dialogs;
using MyToDo2.Views;
using MyToDo2.Views.Dialogs;
using RestSharp;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;

namespace MyToDo2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();

            dialog.Show("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                base.OnInitialized();
                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null) service.Configure();
            });
        }

        public static void LogOut(IDialogService dialog)
        {
            dialog.Show("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                //base.OnInitialized();
                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null) service.Configure();
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:32882", serviceKey: "webUrl");

            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IMaterialDesignDialogService, MaterialDesignDialogService>();

            containerRegistry.RegisterDialog<AddDialog, AddDialogViewModel>();
            containerRegistry.RegisterDialog<MessageDialog, MessageDialogViewModel>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();

            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
        }
    }
}