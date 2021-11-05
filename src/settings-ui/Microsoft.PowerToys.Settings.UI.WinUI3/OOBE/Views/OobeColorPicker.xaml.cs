﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using Microsoft.PowerToys.Settings.UI.Library;
using Microsoft.PowerToys.Settings.UI.WinUI3.OOBE.Enums;
using Microsoft.PowerToys.Settings.UI.WinUI3.OOBE.ViewModel;
using Microsoft.PowerToys.Settings.UI.WinUI3.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Microsoft.PowerToys.Settings.UI.WinUI3.OOBE.Views
{
    public sealed partial class OobeColorPicker : Page
    {
        public OobePowerToysModule ViewModel { get; set; }

        public OobeColorPicker()
        {
            this.InitializeComponent();
            ViewModel = new OobePowerToysModule(OobeShellPage.OobeShellHandler.Modules[(int)PowerToysModulesEnum.ColorPicker]);
            DataContext = ViewModel;
        }

        private void Start_ColorPicker_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (OobeShellPage.ColorPickerSharedEventCallback != null)
            {
                using (var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, OobeShellPage.ColorPickerSharedEventCallback()))
                {
                    eventHandle.Set();
                }
            }

            ViewModel.LogRunningModuleEvent();
        }

        private void SettingsLaunchButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (OobeShellPage.OpenMainWindowCallback != null)
            {
                OobeShellPage.OpenMainWindowCallback(typeof(ColorPickerPage));
            }

            ViewModel.LogOpeningSettingsEvent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LogOpeningModuleEvent();
            HotkeyControl.Keys = SettingsRepository<ColorPickerSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.ActivationShortcut.GetKeysList();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.LogClosingModuleEvent();
        }
    }
}
