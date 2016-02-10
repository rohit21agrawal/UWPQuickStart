using System;
using System.Collections.Generic;
using UWPQuickStart.Views;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Utils;

namespace UWPQuickStart
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class EventMainPage : Page
    {
        //Declare the top level nav items
        private List<NavMenuItem> _navList = new List<NavMenuItem>(
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
            navMenuList.ItemsSource = _navList;
            this.DataContext = App.EventModel;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
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
            AppNavigationUtil.SplitViewPaneHandler(this, this.rootSplitView, this.TogglePaneButton);
            this.TogglePaneButtonRectChanged?.DynamicInvoke(this, this.TogglePaneButtonRect);
        }

	    
	    internal Rect TogglePaneButtonRect{get;set;}
        
        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        internal event TypedEventHandler<EventMainPage, Rect> TogglePaneButtonRectChanged;

        private void NavMenu_ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            Type destPage = (e.ClickedItem as NavMenuItem).DestPage;  
            AppNavigationUtil.SetSplitViewContent(rootSplitView, destPage, true);
            rootSplitView.IsPaneOpen = false;
        }


        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            AppNavigationUtil.SetSplitViewContent(rootSplitView, null, false);
        }
    }
}
