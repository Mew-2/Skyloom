using MyToDo2.Common;
using MyToDo2.Extensions;
using MyToDo2.Services;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;
using System.Collections.ObjectModel;

namespace MyToDo2.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        #region 绑定属性

        private bool isRightDrawerOpen;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> toDoDtos;

        /// <summary>
        /// 待办事项集合
        /// </summary>
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        private ToDoDto currentDto;

        /// <summary>
        /// 当前待办事项
        /// </summary>
        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        private string search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;

        /// <summary>
        /// 下拉列表项
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        #endregion 绑定属性

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        private readonly IToDoService service;
        private readonly IMaterialDesignDialogService dialogService;
        private readonly IEventAggregator aggregator;

        public ToDoViewModel(IToDoService service, IEventAggregator aggregator, IMaterialDesignDialogService dialogService)
            : base(aggregator)
        {
            this.service = service;
            this.dialogService = dialogService;
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            ToDoDtos = new ObservableCollection<ToDoDto>();
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "打开新增页": OpenAdd(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }

        private async void Selected(ToDoDto dto)
        {
            var toDoResult = await service.GetAsync(dto.Id);

            if (toDoResult.Status)
            {
                CurrentDto = toDoResult.Result;
                IsRightDrawerOpen = true;
            }
        }

        private async void Delete(ToDoDto dto)
        {
            var result = await dialogService.Message($"确认删除待办：{dto.Title}？");

            if (result is bool confirmResult && !confirmResult) return;

            UpdateLoading(true);
            try
            {
                var deleteResult = await service.DeleteAsync(dto.Id);

                if (deleteResult.Status)
                {
                    var todo = ToDoDtos.FirstOrDefault(t => t.Id == dto.Id);
                    if (todo != null)
                    {
                        ToDoDtos.Remove(todo);
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
            }
        }

        /// <summary>
        /// 添加待办
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OpenAdd()
        {
            CurrentDto = new ToDoDto();
            IsRightDrawerOpen = true;
        }

        private async void GetDataAsync()
        {
            UpdateLoading(true);

            int? status = SelectedIndex == 0 ? null : SelectedIndex == 1 ? 0 : 1;

            var toDoResult = await service.GetFilterAllAsync(new ToDoParameter
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
                Status = status,
            });

            if (toDoResult != null && toDoResult.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in toDoResult.Result.Items) ToDoDtos.Add(item);
            }

            UpdateLoading(false);
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(CurrentDto.Title) || string.IsNullOrEmpty(CurrentDto.Content)) return;
            UpdateLoading(true);

            try
            {
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await service.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id == currentDto.Id);

                        if (todo != null)
                        {
                            todo.Title = currentDto.Title;
                            todo.Content = currentDto.Content;
                            todo.Status = currentDto.Status;
                        }
                    }
                }
                else
                {
                    var addResult = await service.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        ToDoDtos.Add(addResult.Result);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                IsRightDrawerOpen = false;
                UpdateLoading(false);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("value"))
                SelectedIndex = navigationContext.Parameters.GetValue<int>("value");

            GetDataAsync();
        }
    }
}