using MyToDo2.Common;
using MyToDo2.Common.Models;
using MyToDo2.Extensions;
using System.Collections.ObjectModel;

namespace MyToDo2.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { SetProperty(ref menuBars, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; set; }
        public DelegateCommand MovePrevCommand { get; set; }
        public DelegateCommand MoveNextCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }

        private IRegionManager regionManager;
        private readonly IDialogService dialogService;
        private IRegionNavigationJournal? journal;

        public MainViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            MovePrevCommand = new DelegateCommand(() => { if (journal != null && journal.CanGoBack) journal.GoBack(); });
            MoveNextCommand = new DelegateCommand(() => { if (journal != null && journal.CanGoForward) journal.GoForward(); });
            ExitCommand = new DelegateCommand(Exit);
            this.regionManager = regionManager;
            this.dialogService = dialogService;
        }

        private void Exit()
        {
            App.Current.MainWindow.Hide();

            dialogService.Show("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                App.Current.MainWindow.Show();
                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null) service.Configure();
            });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace)) return;

            regionManager.RequestNavigate(RegionNames.MainViewRegionName, obj.NameSpace, back =>
            {
                if (back.Context != null) { journal = back.Context.NavigationService.Journal; };
            });
        }

        private void CreateMenuBar()
        {
            MenuBars = new ObservableCollection<MenuBar>();
            MenuBars.Add(new MenuBar() { SelectedIcon = "Home", UnselectedIcon = "HomeOutline", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { SelectedIcon = "ListBox", UnselectedIcon = "ListBoxOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { SelectedIcon = "Notebook", UnselectedIcon = "NotebookOutline", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { SelectedIcon = "Cog", UnselectedIcon = "CogOutline", Title = "设置", NameSpace = "SettingsView" });
        }

        public void Configure()
        {
            UserName = AppSession.UserName;
            CreateMenuBar();
            regionManager.RequestNavigate(RegionNames.MainViewRegionName, "IndexView");
        }
    }
}