using MyToDo2.Common;
using MyToDo2.Common.Events;

namespace MyToDo2.Extensions
{
    public static class DialogExtensions
    {
        public static async Task<object?> Message(this IMaterialDesignDialogService dialogService, string message)
        {
            var parameter = new DialogParameters();
            parameter.Add("Message", message);
            var result = await dialogService.MDShowDialogAsync("MessageDialog", parameter, "RootDialog");
            return result;
        }

        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator aggregator, Action<UpdateModel> action)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void RegisterMessage(this IEventAggregator aggregator, Action<MessageModel> action,
            string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, m =>
            {
                return m.Filter.Equals(filterName);
            });
        }

        /// <summary>
        /// 发送提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator aggregator, string message, string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                Filter = filterName,
                Message = message
            });
        }
    }
}