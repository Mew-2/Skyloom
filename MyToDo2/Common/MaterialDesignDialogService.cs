using MaterialDesignThemes.Wpf;
using Prism.Common;
using Prism.Ioc;
using System.ComponentModel;
using System.Windows;

using Prism.Ioc;

using System;
using System.Threading.Tasks;
using Prism.Dialogs;

namespace MyToDo2.Common
{
    //public class MaterialDesignDialogService : IDialogService
    //{
    //    private readonly IContainerExtension _containerExtension;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DialogService"/> class.
    //    /// </summary>
    //    /// <param name="containerExtension">The <see cref="IContainerExtension" /></param>
    //    public MaterialDesignDialogService(IContainerExtension containerExtension)
    //    {
    //        _containerExtension = containerExtension;
    //    }

    //    public void ShowDialog(string name, IDialogParameters parameters, DialogCallback callback)
    //    {
    //        parameters ??= new DialogParameters();
    //        var isModal = parameters.TryGetValue<bool>(KnownDialogParameters.ShowNonModal, out var show) ? !show : true;
    //        var windowName = parameters.TryGetValue<string>(KnownDialogParameters.WindowName, out var wName) ? wName : null;

    //        IDialogWindow dialogWindow = CreateDialogWindow(windowName);
    //        ConfigureDialogWindowEvents(dialogWindow, callback);
    //        ConfigureDialogWindowContent(name, dialogWindow, parameters);

    //        ShowDialogWindow(dialogWindow, isModal);
    //    }

    //    /// <summary>
    //    /// Shows the dialog window.
    //    /// </summary>
    //    /// <param name="dialogWindow">The dialog window to show.</param>
    //    /// <param name="isModal">If true; dialog is shown as a modal</param>
    //    protected virtual void ShowDialogWindow(IDialogWindow dialogWindow, bool isModal)
    //    {
    //        if (isModal)
    //            dialogWindow.ShowDialog();
    //        else
    //            dialogWindow.Show();
    //    }

    //    /// <summary>
    //    /// Create a new <see cref="IDialogWindow"/>.
    //    /// </summary>
    //    /// <param name="name">The name of the hosting window registered with the IContainerRegistry.</param>
    //    /// <returns>The created <see cref="IDialogWindow"/>.</returns>
    //    protected virtual IDialogWindow CreateDialogWindow(string name)
    //    {
    //        if (string.IsNullOrWhiteSpace(name))
    //            return _containerExtension.Resolve<IDialogWindow>();
    //        else
    //            return _containerExtension.Resolve<IDialogWindow>(name);
    //    }

    //    /// <summary>
    //    /// Configure <see cref="IDialogWindow"/> content.
    //    /// </summary>
    //    /// <param name="dialogName">The name of the dialog to show.</param>
    //    /// <param name="window">The hosting window.</param>
    //    /// <param name="parameters">The parameters to pass to the dialog.</param>
    //    protected virtual void ConfigureDialogWindowContent(string dialogName, IDialogWindow window, IDialogParameters parameters)
    //    {
    //        var content = _containerExtension.Resolve<object>(dialogName);
    //        if (!(content is FrameworkElement dialogContent))
    //            throw new NullReferenceException("A dialog's content must be a FrameworkElement");

    //        MvvmHelpers.AutowireViewModel(dialogContent);

    //        if (!(dialogContent.DataContext is IDialogAware viewModel))
    //            throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

    //        ConfigureDialogWindowProperties(window, dialogContent, viewModel);

    //        MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
    //    }

    //    protected virtual void ConfigureDialogWindowEvents(IDialogWindow dialogWindow, DialogCallback callback)
    //    {
    //        Action<IDialogResult> requestCloseHandler = (r) =>
    //        {
    //            dialogWindow.Result = r;
    //            dialogWindow.Close();
    //        };

    //        RoutedEventHandler loadedHandler = null;
    //        loadedHandler = (o, e) =>
    //        {
    //            dialogWindow.Loaded -= loadedHandler;
    //            DialogUtilities.InitializeListener((IDialogAware)dialogWindow.DataContext, requestCloseHandler);
    //        };
    //        dialogWindow.Loaded += loadedHandler;

    //        CancelEventHandler closingHandler = null;
    //        closingHandler = (o, e) =>
    //        {
    //            if (!((IDialogAware)dialogWindow.DataContext).CanCloseDialog())
    //                e.Cancel = true;
    //        };
    //        dialogWindow.Closing += closingHandler;

    //        EventHandler closedHandler = null;
    //        closedHandler = async (o, e) =>
    //        {
    //            dialogWindow.Closed -= closedHandler;
    //            dialogWindow.Closing -= closingHandler;

    //            ((IDialogAware)dialogWindow.DataContext).OnDialogClosed();

    //            if (dialogWindow.Result == null)
    //                dialogWindow.Result = new DialogResult();

    //            await callback.Invoke(dialogWindow.Result);

    //            dialogWindow.DataContext = null;
    //            dialogWindow.Content = null;
    //        };
    //        dialogWindow.Closed += closedHandler;
    //    }

    //    /// <summary>
    //    /// Configure <see cref="IDialogWindow"/> properties.
    //    /// </summary>
    //    /// <param name="window">The hosting window.</param>
    //    /// <param name="dialogContent">The dialog to show.</param>
    //    /// <param name="viewModel">The dialog's ViewModel.</param>
    //    protected virtual void ConfigureDialogWindowProperties(IDialogWindow window, FrameworkElement dialogContent, IDialogAware viewModel)
    //    {
    //        var windowStyle = Dialog.GetWindowStyle(dialogContent);
    //        if (windowStyle != null)
    //            window.Style = windowStyle;

    //        window.Content = dialogContent;
    //        window.DataContext = viewModel; //we want the host window and the dialog to share the same data context

    //        if (window.Owner == null)
    //            window.Owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
    //    }
    //}

    public class MaterialDesignDialogService : DialogService, IMaterialDesignDialogService
    {
        private readonly IContainerExtension containerExtension;

        public MaterialDesignDialogService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<object?> MDShowDialogAsync(string name, IDialogParameters parameters, object dialogIdentifier)
        {
            parameters ??= new DialogParameters();

            var windowName = parameters.TryGetValue<string>(KnownDialogParameters.WindowName, out var wName) ? wName : null;

            //从容器中取出实例

            IDialogWindow dialogWindow = CreateDialogWindow(windowName);

            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性

            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            //自动注入
            MvvmHelpers.AutowireViewModel(dialogContent);

            if (!(dialogContent.DataContext is IDialogAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            //窗口设置
            ConfigureDialogWindowProperties(dialogWindow, dialogContent, viewModel);

            //传递参数
            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
            //打开窗口
            return await DialogHost.Show(content, "RootDialog");
        }
    }
}