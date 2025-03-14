using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Common
{
    public interface IMaterialDesignDialogService : IDialogService
    {
        Task<object?> MDShowDialogAsync(string name, IDialogParameters parameters, object dialogIdentifier);
    }
}