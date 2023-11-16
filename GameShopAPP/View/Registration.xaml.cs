﻿using GameShopAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private RegistrationViewModel _viewModel;
        public Registration()
        {
            InitializeComponent();
            _viewModel = new RegistrationViewModel();
            DataContext = _viewModel;

            // Подписываемся на событие вью-модели для закрытия окна
            _viewModel!.RequestClose += (sender, args) => Close();
        }
    }
}
