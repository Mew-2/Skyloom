using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Shared.Dtos
{
    public class SummaryDto : BaseDto
    {
        private int toDoSum;

        /// <summary>
        /// 待办事项总数
        /// </summary>
        public int ToDoSum
        {
            get { return toDoSum; }
            set { toDoSum = value; OnPropertyChanged(); }
        }

        private int completedCount;

        /// <summary>
        /// 完成数量
        /// </summary>
        public int CompletedCount
        {
            get { return completedCount; }
            set { completedCount = value; OnPropertyChanged(); }
        }

        private string completedRatio = "";

        /// <summary>
        /// 完成比例
        /// </summary>
        public string CompletedRatio
        {
            get { return completedRatio; }
            set { completedRatio = value; OnPropertyChanged(); }
        }

        private int memoryCount;

        /// <summary>
        /// 备忘录数量
        /// </summary>
        public int MemoryCount
        {
            get { return memoryCount; }
            set { memoryCount = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> todoList;

        /// <summary>
        /// 待办列表
        /// </summary>
        public ObservableCollection<ToDoDto> TodoList
        {
            get { return todoList; }
            set { todoList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MemoDto> memoList;

        /// <summary>
        /// 备忘录列表
        /// </summary>
        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; OnPropertyChanged(); }
        }
    }
}