using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWPQuickStart.Utils
{
    class AppNavigationUtil
    {
        internal static void SetBackButtonVisibility()
        {
            if(App.NavigationHistory.Count > 0)
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

        internal static void SplitViewPaneHandler(EventMainPage page, SplitView rootSplitView, ToggleButton togglePaneButton)
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
                if (destPage != rootSplitView.Content.GetType())
                {
                    AddToBackStack(rootSplitView.Content.GetType());
                    rootSplitView.Content = (UserControl) Activator.CreateInstance(destPage);
                }
            }
            else
            {
                rootSplitView.Content = (UserControl)Activator.CreateInstance(RemoveFromBackStack());
            }

        }

        internal static void AddToBackStack(Type type)
        {
            App.NavigationHistory.Push(type);
            SetBackButtonVisibility();
        }

        internal static Type RemoveFromBackStack()
        {
            Type type = App.NavigationHistory.Pop();
            SetBackButtonVisibility();
            return type;
        }
    }
}
