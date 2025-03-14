using MyToDo2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator aggregator;

        public NavigationViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void UpdateLoading(bool IsOpen)
        {
            aggregator.UpdateLoading(new Common.Events.UpdateModel()
            {
                IsOpen = IsOpen
            });
        }
    }
}