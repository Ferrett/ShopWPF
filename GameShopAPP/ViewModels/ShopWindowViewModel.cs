using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace GameShopAPP.ViewModels
{
    public class ShopWindowViewModel : ViewModelBase
    {
        
        public NavigateCommand<SearchViewModel> NavigateSearchCommand { get; }

        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ShopWindowViewModel(NavigationStore navigationStore) 
        {
            NavigateSearchCommand = new NavigateCommand<SearchViewModel>(navigationStore, () => new SearchViewModel(
              navigationStore));

            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

         
        }

       

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
