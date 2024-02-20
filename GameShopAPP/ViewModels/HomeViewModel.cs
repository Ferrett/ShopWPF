using GameShopAPP.Services;
using GameShopAPP.Services.Navigation;
using GameShopAPP.Services.Requests;
using GameShopAPP.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameShopAPP.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public RelayCommand TestCommand { get; }
       

        public HomeViewModel(NavigationStore navigationStore)
        {
           
            TestCommand = new RelayCommand(Test2);
        }

        private void Test2()
        {
            
        }
    }
}
