using GameShopAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services.Navigation
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _сurrentViewModel;
        public ViewModelBase CurrentViewModel {
            get => _сurrentViewModel!;
            set
            {
                _сurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
