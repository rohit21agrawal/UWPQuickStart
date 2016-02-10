// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Utils;

namespace UWPQuickStart.Views
{
    public sealed partial class EventHome : UserControl
    {
        public EventHome()
        {
            InitializeComponent();
            DataContext = App.EventModel;
        }

        private void rsvpButtonHandler(object sender, RoutedEventArgs e)
        {
            var eventPage = (Window.Current.Content as Frame)?.Content as EventMainPage;
            var rootSplitView = eventPage?.FindName("rootSplitView") as SplitView;

            AppNavigationUtil.SetSplitViewContent(rootSplitView, typeof (RSVP), true);
        }
    }
}