using DryIoc;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.ViewModels.Dialogs
{
    public class AddDialogViewModel : BindableBase, IDialogAware
    {
        private string title = "";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string content = "";

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        private string addType;

        public string AddType
        {
            get { return addType; }
            set { SetProperty(ref addType, value); }
        }

        public DelegateCommand<bool?> ExcuteCommand { get; set; }

        public DialogCloseListener RequestClose { get; set; }

        public AddDialogViewModel()
        {
            ExcuteCommand = new DelegateCommand<bool?>(Excute);
        }

        private void Excute(bool? buttonValue)
        {
            var dialogResult = new DialogResult();
            var parameters = new DialogParameters();
            if (buttonValue == true)
            {
                dialogResult.Result = ButtonResult.OK;

                if (string.IsNullOrEmpty(Title)) return;

                parameters.Add("AddType", AddType);
                parameters.Add("Title", Title);
                parameters.Add("Content", Content);
            }
            else
            {
                dialogResult.Result = ButtonResult.Cancel;
            }

            dialogResult.Parameters = parameters;

            DialogHost.CloseDialogCommand.Execute(dialogResult, null);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            AddType = parameters.GetValue<string>("AddType");
        }
    }
}