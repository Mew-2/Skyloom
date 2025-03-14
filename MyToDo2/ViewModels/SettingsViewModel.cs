using MyToDo2.Common.Models;
using MyToDo2.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        private readonly IRegionManager regionManager;

        private ObservableCollection<MenuBar> menuBars = new ObservableCollection<MenuBar>();

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public SettingsViewModel(IRegionManager regionManager)
        {
            CreatMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace)) return;

            regionManager.Regions[RegionNames.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }

        private void CreatMenuBar()
        {
            MenuBars.Add(new MenuBar { SelectedIcon = "Palette", UnselectedIcon = "PaletteOutline", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar { SelectedIcon = "Cog", UnselectedIcon = "CogOutline", Title = "系统设置", NameSpace = "" });
            MenuBars.Add(new MenuBar { SelectedIcon = "Information", UnselectedIcon = "InformationOutline", Title = "关于更多", NameSpace = "AboutView" });
        }
    }
}