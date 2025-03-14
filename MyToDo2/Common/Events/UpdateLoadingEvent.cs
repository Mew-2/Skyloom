using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Common.Events
{
    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {
    }

    public class UpdateModel
    {
        public bool IsOpen { get; set; }
    }
}