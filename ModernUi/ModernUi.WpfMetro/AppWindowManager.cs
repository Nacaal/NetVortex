﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Metro.Core;
using MahApps.Metro.Controls;
using ModernUi.WpfMetro.Views;

namespace ModernUi.WpfMetro
{
    public class AppWindowManager : MetroWindowManager
    {
        public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
        {
            if (windowIsView)
            {
                return view as MainWindowContainer;
            }

            return new MainWindowContainer
            {
                Content = view
            };
        }
    }
}
