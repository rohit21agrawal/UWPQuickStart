using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPQuickStart.Views
{
    public sealed partial class RSVP : UserControl
    {
        public RSVP()
        {
            InitializeComponent();
        }

        private async void sendButtonHandler(object sender, RoutedEventArgs e)
        {
            string finalResponse = null;
            if (yesRadioButton.IsChecked == true)
            {
                finalResponse = "I'm looking forward to seeing you there!";
            }
            else if (noRadioButton.IsChecked == true)
            {
                finalResponse = "I would love to, but I can't make it this time :(.";
            }
            else if (maybeRadioButton.IsChecked == true)
            {
                finalResponse = "I will try my best.";
            }

            var emailUri =
                new Uri("mailto:" + App.EventModel.RSVPEmail + "?subject=RSVP: " + App.EventModel.EventName + "&body=" +
                        finalResponse);
            await Launcher.LaunchUriAsync(emailUri);
        }
    }
}