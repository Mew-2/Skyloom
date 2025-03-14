using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.ViewModels.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public DialogCloseListener RequestClose { get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("Message");
        }
    }
}