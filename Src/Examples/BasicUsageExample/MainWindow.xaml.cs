﻿using System;
using System.Windows;
using ToastNotifications.Core;

namespace BasicUsageExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = _vm = new MainViewModel();
            _vm = new ToastViewModel();

            Unloaded += OnUnload;
        }

        private int _count = 0;
        private readonly ToastViewModel _vm;

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _vm.OnUnloaded();
        }

        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            showMessage(_vm.ShowInformation, "Information");
        }

        private void Button_ShowSuccessClick(object sender, RoutedEventArgs e)
        {
            showMessage(_vm.ShowSuccess, "Success");
        }

        private void Button_ShowWarningClick(object sender, RoutedEventArgs e)
        {
            showMessage(_vm.ShowWarning, "Warning");
        }

        private void Button_ShowErrorClick(object sender, RoutedEventArgs e)
        {
            showMessage(_vm.ShowError, "Error");
        }

        string lastMessage;
        void showMessage(Action<string, MessageOptions> action, string name)
        {
            MessageOptions opts = new MessageOptions
            {
                CloseClickAction = closeAction,
                Tag = "[This is Tag Value]",
                FreezeOnMouseEnter = cbFreezeOnMouseEnter.IsChecked.GetValueOrDefault(),
                ShowCloseButton = cbShowCloseButton.IsChecked.GetValueOrDefault()
            };
            lastMessage = $"{_count++} {name}";
            action(lastMessage, opts);
            bClearLast.IsEnabled = true;
        }

        private void closeAction(NotificationBase obj)
        {
            var opts = obj.DisplayPart.GetOptions();
            MessageBox.Show($"Notification close clicked {opts.Tag.ToString()}");
        }


        private void Button_ClearClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearMessages("");
        }

        private void Button_ClearLastClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearMessages(lastMessage);
            bClearLast.IsEnabled = false;
        }

        private void Button_SameContentClick(object sender, RoutedEventArgs e)
        {
            const string sameContent = "Same Content - not duplicated";
            _vm.ClearMessages(sameContent);
            MessageOptions opts = new MessageOptions
            {
                CloseClickAction = closeAction,
                Tag = "[This is Tag Value]",
                FreezeOnMouseEnter = cbFreezeOnMouseEnter.IsChecked.GetValueOrDefault(),
                ShowCloseButton = cbShowCloseButton.IsChecked.GetValueOrDefault()
            };
            _vm.ShowSuccess(sameContent, opts);
            lastMessage = sameContent;
            bClearLast.IsEnabled = true;
        }
    }
}
