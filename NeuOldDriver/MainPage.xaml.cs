﻿using System;
using System.Linq;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

using NeuOldDriver.Models;

namespace NeuOldDriver {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();

            Action<Frame> SetBackButton = (frame) => {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                frame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
            };

            SetBackButton(MainFrame);

            NavMenuToggle.Click += (sender, e) => {
                NavMenuContainer.IsPaneOpen = !NavMenuContainer.IsPaneOpen;
            };

            NavMenu.SelectionChanged += (sender, e) => {
                var page = ((sender as Selector).SelectedItem as NavButtonData).Page as Type;
                if (MainFrame.SourcePageType != page)
                    MainFrame.Navigate(page);
            };

            MainFrame.Navigated += (sender, e) => {
                SetBackButton(sender as Frame);

                NavMenu.SelectedItem = NavMenu.Items.FirstOrDefault((item) => {
                    return ((item as NavButtonData).Page as Type) == MainFrame.SourcePageType;
                });
            };

            SystemNavigationManager.GetForCurrentView().BackRequested += (sender, e) => {
                if (MainFrame.CanGoBack) {
                    e.Handled = true;
                    MainFrame.GoBack();
                }
            };
        }
    }
}
