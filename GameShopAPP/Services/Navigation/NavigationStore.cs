using GameShopAPP.ViewModels;
using System;

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
