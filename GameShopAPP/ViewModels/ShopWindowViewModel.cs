using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace GameShopAPP.ViewModels
{
    public class ShopWindowViewModel : WindowViewModel
    {
        public RelayCommand TestCommand { get; }
        public ICommand NavigateSearchCommand { get; }
        public ShopWindowViewModel(NavigationStore navigationStore):base(navigationStore) {
            NavigateSearchCommand = new NavigateCommand<SearchViewModel>(navigationStore, () => new SearchViewModel(
              navigationStore));
            TestCommand = new RelayCommand(Test2);

        }
        private void Test2()
        {
            int a = 0;
        }
    }
}
