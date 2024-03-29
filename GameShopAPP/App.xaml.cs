﻿using GameShopAPP.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using GameShopAPP.ViewModels;

namespace GameShopAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DIContainer.Initialize();
            base.OnStartup(e);
        }
    }
}
