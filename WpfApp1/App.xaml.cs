﻿using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyles = ThemeManager.DetectAppStyle(Application.Current);

            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Cobalt"),
                                        ThemeManager.GetAppTheme("BaseDark"));
            base.OnStartup(e);
        }
    }
}
