// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWPQuickStart.Utils
{
    internal class AppNavigationUtil
    {
        internal static void SetBackButtonVisibility()
        {
            if (App.NavigationHistory.Count > 0)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }

        internal static void SplitViewPaneHandler(EventMainPage page, SplitView rootSplitView,
            ToggleButton togglePaneButton)
        {
            if (rootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                rootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = togglePaneButton.TransformToVisual(page);
                var rect =
                    transform.TransformBounds(new Rect(0, 0, togglePaneButton.ActualWidth,
                        togglePaneButton.ActualHeight));
                page.TogglePaneButtonRect = rect;
            }
            else
            {
                page.TogglePaneButtonRect = new Rect();
            }
        }

        internal static void SetSplitViewContent(SplitView rootSplitView, Type destPage, bool push)
        {
            if (push)
            {
                var type = rootSplitView.Content?.GetType();
                if (destPage != type)
                {
                    AddToBackStack(type);
                    rootSplitView.Content = (UserControl) Activator.CreateInstance(destPage);
                }
            }
            else
            {
                rootSplitView.Content = (UserControl) Activator.CreateInstance(RemoveFromBackStack());
            }
        }

        internal static void AddToBackStack(Type type)
        {
            App.NavigationHistory.Push(type);
            SetBackButtonVisibility();
        }

        internal static Type RemoveFromBackStack()
        {
            var type = App.NavigationHistory.Pop();
            SetBackButtonVisibility();
            return type;
        }
    }
}