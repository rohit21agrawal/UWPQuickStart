using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPQuickStart.Views
{
	public sealed partial class EventHome : UserControl
    {
        public EventHome()
        {
			this.InitializeComponent();
            this.DataContext = App.EventModel;
        }

        private void rsvpHandler(object sender, RoutedEventArgs e)
        {
            EventMainPage eventPage = (Window.Current.Content as Frame).Content as EventMainPage;
            SplitView RootSplitView = eventPage.FindName("RootSplitView") as SplitView;

            App.NavigationHistory.Push(RootSplitView.Content.GetType());
            if (App.NavigationHistory.Count > 0)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            RootSplitView.Content = new RSVP();
        }
    }
}
