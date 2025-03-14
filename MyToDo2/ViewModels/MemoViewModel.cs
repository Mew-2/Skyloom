using MyToDo2.Common;
using MyToDo2.Extensions;
using MyToDo2.Services;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;
using Prism.Dialogs;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace MyToDo2.ViewModels
{
    public class MemoViewModel : NavigationViewModel
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

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        private MemoDto currentDto;

        /// <summary>
        /// 当前备忘录
        /// </summary>
        public MemoDto CurrentDto
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
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }

        private readonly IMemoService service;
        private readonly IMaterialDesignDialogService dialogService;

        public MemoViewModel(IMemoService service, IEventAggregator aggregator, IMaterialDesignDialogService dialogService)
            : base(aggregator)
        {
            this.service = service;
            this.dialogService = dialogService;
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            MemoDtos = new ObservableCollection<MemoDto>();
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

        private async void Selected(MemoDto dto)
        {
            var memoResult = await service.GetAsync(dto.Id);

            if (memoResult.Status)
            {
                CurrentDto = memoResult.Result;
                IsRightDrawerOpen = true;
            }
        }

        private async void Delete(MemoDto dto)
        {
            var result = await dialogService.Message($"确认删除备忘：{dto.Title}？");

            if (result is bool confirmResult && !confirmResult) return;

            UpdateLoading(true);
            try
            {
                var deleteResult = await service.DeleteAsync(dto.Id);

                if (deleteResult.Status)
                {
                    var memo = MemoDtos.FirstOrDefault(t => t.Id == dto.Id);
                    if (memo != null)
                    {
                        MemoDtos.Remove(memo);
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
        /// 打开备忘录添加页面
        /// </summary>
        private void OpenAdd()
        {
            CurrentDto = new MemoDto();
            IsRightDrawerOpen = true;
        }

        private async void GetDataAsync()
        {
            UpdateLoading(true);
            var meMoResult = await service.GetAllAsync(new QueryParameter
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search
            });

            if (meMoResult != null && meMoResult.Status)
            {
                MemoDtos.Clear();
                foreach (var item in meMoResult.Result.Items) MemoDtos.Add(item);
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
                        var memo = MemoDtos.FirstOrDefault(t => t.Id == currentDto.Id);

                        if (memo != null)
                        {
                            memo.Title = currentDto.Title;
                            memo.Content = currentDto.Content;
                        }
                    }
                }
                else
                {
                    var addResult = await service.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        MemoDtos.Add(addResult.Result);
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
            GetDataAsync();
        }
    }
}