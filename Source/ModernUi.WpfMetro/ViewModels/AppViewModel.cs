using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NetVortex.ModernUi.WpfMetro.ViewModels.Tabs;

namespace NetVortex.ModernUi.WpfMetro.ViewModels
{
    public class AppViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;

        public AppViewModel(IWindowManager windowManager, SettingsViewModel settingsViewModel)
        {
            _windowManager = windowManager;

            Settings = settingsViewModel;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "ModernUi Metro (Caliburn Autofac)";

            Items.Add(IoC.Get<CustomerTabViewModel>());
            Items.Add(IoC.Get<AboutTabViewModel>());
        }

        public void OpenWindow()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            
            _windowManager.ShowWindow(new AppViewModel(_windowManager, Settings), null, settings);
        }

        public SettingsViewModel Settings { get; set; }
        public CupcakesViewModel Cupcakes { get; set; }

        public void OpenSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        public void OpenCupcakes()
        {
            IsCupcakesFlyoutOpen = true;
        }

        private bool _isSettingsFlyoutOpen;
        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }

        private bool _isCupcakesFlyoutOpen;
        public bool IsCupcakesFlyoutOpen
        {
            get { return _isCupcakesFlyoutOpen; }
            set
            {
                _isCupcakesFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsCupcakesFlyoutOpen);
            }
        }

        public bool CanShowProgress()
        {
            return true;
        }

        public async void ShowProgress()
        {
            var win = Application.Current.MainWindow as MetroWindow;

            if (win == null)
                return;
            
            var progressDialogController = await win.ShowProgressAsync("Downloading", "Downloading internet...");

            await Task.Delay(4000);

            await progressDialogController.CloseAsync();
        }
    }
}
