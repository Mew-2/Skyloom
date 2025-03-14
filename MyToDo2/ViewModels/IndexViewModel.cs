using DryIoc.ImTools;
using MaterialDesignThemes.Wpf;
using MyToDo2.Common;
using MyToDo2.Common.Models;
using MyToDo2.Extensions;
using MyToDo2.Services;
using MyToDo2.Shared.Dtos;
using MyToDo2.ViewModels.Dialogs;
using MyToDo2.Views.Dialogs;
using System.Collections.ObjectModel;

namespace MyToDo2.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        #region 绑定属性

        private ObservableCollection<TaskBar> taskBars = new ObservableCollection<TaskBar>();

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { SetProperty(ref taskBars, value); }
        }

        private SummaryDto summary;

        /// <summary>
        /// 首页统计
        /// </summary>
        public SummaryDto Summary
        {
            get { return summary; }
            set { SetProperty(ref summary, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private readonly IEventAggregator aggregator;

        #endregion 绑定属性

        private readonly IMaterialDesignDialogService dialogService;
        private readonly IToDoService toDoService;
        private readonly IRegionManager regionManager;
        private readonly IMemoService memoService;

        public DelegateCommand<string> AddCommand { get; set; }
        public DelegateCommand<ToDoDto> CompletedCommand { get; set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; set; }

        public IndexViewModel(IEventAggregator aggregator, IMaterialDesignDialogService dialogService, IToDoService toDoService, IRegionManager regionManager, IMemoService memoService) : base(aggregator)
        {
            AddCommand = new DelegateCommand<string>(Add);
            CompletedCommand = new DelegateCommand<ToDoDto>(Completed);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            creatTaskBars();
            this.aggregator = aggregator;
            this.dialogService = dialogService;
            this.toDoService = toDoService;
            this.regionManager = regionManager;
            this.memoService = memoService;
        }

        private void Navigate(TaskBar bar)
        {
            if (bar == null || string.IsNullOrEmpty(bar.Target)) return;
            NavigationParameters param = new NavigationParameters();
            if (bar.Title == "已完成")
            {
                param.Add("value", 2);
            }
            else if (bar.Title == "汇总")
            {
                param.Add("value", 0);
            }

            regionManager.Regions[RegionNames.MainViewRegionName].RequestNavigate(bar.Target, param);
        }

        private async void Completed(ToDoDto toDo)
        {
            var removeObj = Summary.TodoList.FindFirst(t => t.Equals(toDo));

            if (removeObj == null) return;
            removeObj.Status = 1;
            var result = await toDoService.UpdateAsync(removeObj);
            if (result.Status)
            {
                Summary.TodoList.Remove(removeObj);
                aggregator.SendMessage($"已完成待办事项：{removeObj.Title}");
                Refresh();
            }
        }

        private async void Add(string addType)
        {
            var parameters = new DialogParameters();
            parameters.Add("AddType", addType);

            var dialogResult = (IDialogResult?)await dialogService.MDShowDialogAsync("AddDialog", parameters, "RootDialog");

            if (dialogResult == null || dialogResult.Result == ButtonResult.Cancel) return;

            var title = dialogResult.Parameters.GetValue<string>("Title");
            var content = dialogResult.Parameters.GetValue<string>("Content");
            if (string.IsNullOrEmpty(title)) return;

            UpdateLoading(true);

            try
            {
                if (dialogResult.Parameters.GetValue<string>("AddType").Contains("待办"))
                {
                    var todoDto = new ToDoDto()
                    {
                        Title = title,
                        Content = content
                    };

                    var addResult = await toDoService.AddAsync(todoDto);
                    if (addResult.Status)
                    {
                        Summary.TodoList.Add(addResult.Result);
                    }
                }
                else
                {
                    var memoDto = new MemoDto()
                    {
                        Title = title,
                        Content = content
                    };

                    var addResult = await memoService.AddAsync(memoDto);
                    if (addResult.Status)
                    {
                        Summary.MemoList.Add(addResult.Result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                UpdateLoading(false);
                Refresh();
            }
        }

        private void creatTaskBars()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#F44336", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FFEB3B", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成率", Color = "#90CAF9", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#CE93D8", Target = "MemoView" });
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Refresh();
            base.OnNavigatedTo(navigationContext);
        }

        private async void Refresh()
        {
            Title = $"你好，{AppSession.UserName}！今天是{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";

            var summaryResult = await toDoService.SummaryAsync();

            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                TaskBars[0].Content = Summary.ToDoSum.ToString();
                TaskBars[1].Content = Summary.CompletedCount.ToString();
                TaskBars[2].Content = Summary.CompletedRatio.ToString();
                TaskBars[3].Content = Summary.MemoryCount.ToString();
            }
        }
    }
}