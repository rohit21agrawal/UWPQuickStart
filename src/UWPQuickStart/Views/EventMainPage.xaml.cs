// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Utils;
using UWPQuickStart.Views;

namespace UWPQuickStart
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventMainPage : Page
    {
        //Declare the top level nav items
        private readonly List<NavMenuItem> _navList = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem
                {
                    Symbol = Symbol.Home,
                    Label = "HOME",
                    DestPage = typeof (EventHome)
                },
                new NavMenuItem
                {
                    Symbol = Symbol.Directions,
                    Label = "EVENT DETAILS",
                    DestPage = typeof (EventDetails)
                },
                new NavMenuItem
                {
                    Symbol = Symbol.Camera,
                    Label = "PHOTOS",
                    DestPage = typeof (Photos)
                }
            });

        public EventMainPage()
        {
            InitializeComponent();
            navMenuList.ItemsSource = _navList;
            DataContext = App.EventModel;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        internal Rect TogglePaneButtonRect { get; set; }

        /// <summary>
        ///     Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        ///     then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        ///     Check for the conditions where the navigation pane does not occupy the space under the floating
        ///     hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            AppNavigationUtil.SplitViewPaneHandler(this, rootSplitView, TogglePaneButton);
            TogglePaneButtonRectChanged?.DynamicInvoke(this, TogglePaneButtonRect);
        }

        /// <summary>
        ///     An event to notify listeners when the hamburger button may occlude other content in the app.
        ///     The custom "PageHeader" user control is using this.
        /// </summary>
        internal event TypedEventHandler<EventMainPage, Rect> TogglePaneButtonRectChanged;

        private void NavMenu_ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            var destPage = (e.ClickedItem as NavMenuItem)?.DestPage;
            AppNavigationUtil.SetSplitViewContent(rootSplitView, destPage, true);
            rootSplitView.IsPaneOpen = false;
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            AppNavigationUtil.SetSplitViewContent(rootSplitView, null, false);
        }
    }
}