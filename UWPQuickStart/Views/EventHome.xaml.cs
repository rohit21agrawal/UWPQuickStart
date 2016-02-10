using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Utils;

namespace UWPQuickStart.Views
{
	public sealed partial class EventHome : UserControl
    {
        public EventHome()
        {
			this.InitializeComponent();
            this.DataContext = App.EventModel;
        }

        private void rsvpButtonHandler(object sender, RoutedEventArgs e)
        {
            EventMainPage eventPage = (Window.Current.Content as Frame).Content as EventMainPage;
            SplitView rootSplitView = eventPage.FindName("rootSplitView") as SplitView;

            AppNavigationUtil.SetSplitViewContent(rootSplitView, typeof(RSVP), true);
        }
    }
}
