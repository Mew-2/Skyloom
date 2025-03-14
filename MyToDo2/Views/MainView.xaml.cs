using DryIoc;
using MaterialDesignThemes.Wpf;
using MyToDo2.Common;
using MyToDo2.Extensions;
using Prism.Dialogs;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MyToDo2.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IMaterialDesignDialogService dialogService;
        private bool _canClose = false;

        public MainView(IEventAggregator aggregator, IMaterialDesignDialogService dialogService)
        {
            InitializeComponent();

            //注册提示消息
            aggregator.RegisterMessage(arg => { MainSnackbar.MessageQueue.Enqueue(arg); });

            //注册等待消息窗口
            aggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                if (DialogHost.IsOpen) { DialogHost.DialogContent = new ProgressView(); }
            });
            this.dialogService = dialogService;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NavDrawer.OpenMode is not DrawerHostOpenMode.Standard)
            {
                //until we had a StaysOpen flag to Drawer, this will help with scroll bars
                var dependencyObject = Mouse.Captured as DependencyObject;

                while (dependencyObject != null)
                {
                    if (dependencyObject is ScrollBar) return;
                    dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
                }

                MenuToggleButton.IsChecked = false;
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true; // 防止窗口关闭
                var result = await dialogService.Message("确认退出？");
                if (result is bool confirmResult && confirmResult)
                {
                    _canClose = true; // 设置标志位允许关闭
                    Close(); // 手动触发关闭
                }
            }
        }
    }
}