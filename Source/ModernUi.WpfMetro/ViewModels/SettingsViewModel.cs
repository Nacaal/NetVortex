using System.Windows;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro;
using System.Collections.ObjectModel;
using ModernUi.WpfMetro.Views;

namespace ModernUi.WpfMetro.ViewModels
{
    public class SettingsViewModel : PropertyChangedBase
    {
        public BindableCollection<AppTheme> AppThemes { get; set; }
        public BindableCollection<Accent> Accents { get; set; }

        public AppTheme SelectedAppTheme { get; set; }
        public Accent SelectedAccent { get; set; }

        public SettingsViewModel()
        {
            AppThemes = new BindableCollection<AppTheme>();
            Accents = new BindableCollection<Accent>();

            AppThemes.AddRange(ThemeManager.AppThemes);
            Accents.AddRange(ThemeManager.Accents);

            var currentAppTheme = ThemeManager.DetectAppStyle(Application.Current);
            if (currentAppTheme != null)
            {
                SelectedAppTheme = AppThemes.FirstOrDefault(x => x.Name == currentAppTheme.Item1.Name);
                SelectedAccent = Accents.FirstOrDefault(x => x.Name == currentAppTheme.Item2.Name);
            }

            NotifyOfPropertyChange(() => AppThemes);
            NotifyOfPropertyChange(() => Accents);
        }

        public void OnSelectionChanged()
        {
            ThemeManager.ChangeAppStyle(Application.Current, SelectedAccent, SelectedAppTheme);
        }
    }
}
