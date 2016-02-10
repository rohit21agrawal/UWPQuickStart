using System;
using System.Collections.Generic;
using UWPQuickStart.Views;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPQuickStart
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class EventMainPage : Page
    {
        //Declare the top level nav items
        private List<NavMenuItem> navList = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    Symbol = Symbol.Home,
                    Label = "HOME",
                    DestPage = typeof(EventHome)
                },
				new NavMenuItem()
				{
					Symbol = Symbol.Directions,
					Label = "EVENT DETAILS",
					DestPage = typeof(EventDetails)
				},
				new NavMenuItem()
                {
                    Symbol = Symbol.Camera,
                    Label = "PHOTOS",
                    DestPage = typeof(Photos)
                },
            });

        public EventMainPage()
        {
            this.InitializeComponent();
            navMenuList.ItemsSource = navList;
            this.DataContext = App.EventModel;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        /// <summary>
        /// Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            if (this.RootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                this.RootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = this.TogglePaneButton.TransformToVisual(this);
                var rect = transform.TransformBounds(new Rect(0, 0, this.TogglePaneButton.ActualWidth, this.TogglePaneButton.ActualHeight));
                this.TogglePaneButtonRect = rect;
            }
            else
            {
                this.TogglePaneButtonRect = new Rect();
            }

            var handler = this.TogglePaneButtonRectChanged;
            if (handler != null)
            {
                // handler(this, this.TogglePaneButtonRect);
                handler.DynamicInvoke(this, this.TogglePaneButtonRect);
            }
        }

        public Rect TogglePaneButtonRect
        {
            get;
            private set;
        }


        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<EventMainPage, Rect> TogglePaneButtonRectChanged;

        private void ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            Type destPage = (e.ClickedItem as NavMenuItem).DestPage;  
            
            if (!(destPage == RootSplitView.Content.GetType()))
            {                
                App.NavigationHistory.Push(RootSplitView.Content.GetType());
                RootSplitView.Content = (UserControl)Activator.CreateInstance(destPage);
            }

            if(App.NavigationHistory.Count > 0)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }

			RootSplitView.IsPaneOpen = false;
        }


        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            RootSplitView.Content = (UserControl)Activator.CreateInstance(App.NavigationHistory.Pop());

            if(App.NavigationHistory.Count == 0)
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }
    }
}
