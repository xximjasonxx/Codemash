using Caliburn.Micro;

namespace Codemash.Phone8.App.ViewModels
{
    public abstract class ViewModelBase : PropertyChangedBase
    {
        private INavigationService _navigationService;
        protected INavigationService NavigationService { get { return _navigationService; } }

        protected ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}