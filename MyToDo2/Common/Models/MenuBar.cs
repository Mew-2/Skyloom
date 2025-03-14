namespace MyToDo2.Common.Models
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    public class MenuBar : BindableBase
    {
        private string selectedIcon = "";

        /// <summary>
        /// 选中图标
        /// </summary>
        public string SelectedIcon
        {
            get { return selectedIcon; }
            set { SetProperty(ref selectedIcon, value); }
        }

        private string unselectedIcon = "";

        /// <summary>
        /// 未选中图标
        /// </summary>
        public string UnselectedIcon
        {
            get { return unselectedIcon; }
            set { SetProperty(ref unselectedIcon, value); }
        }

        private string title = "";

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string nameSpace = "";

        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { SetProperty(ref nameSpace, value); }
        }
    }
}