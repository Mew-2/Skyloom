namespace MyToDo2.Shared.Dtos
{
    public class MemoDto : BaseDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }
    }
}