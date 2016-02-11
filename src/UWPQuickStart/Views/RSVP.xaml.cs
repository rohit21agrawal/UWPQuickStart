// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

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

            if (yesRadioButton.IsChecked == true)
            {

                // setup notification text
                string TOAST = $@"
            <toast>
              <visual>
                <binding template=""ToastGeneric"">
                  <text>Event Alarm</text>
                  <text>Check your calendar for " + App.EventModel.EventName + $@"</text>
                </binding>
              </visual>
              <actions>
                <action content = ""Done"" arguments=""cancel""/>
              </actions>
              <audio src =""ms-winsoundevent:Notification.Reminder""/>
            </toast>";

                // set when the notification should be shown or demo purposes in 10s
                var when = DateTime.Now.AddSeconds(10);
                //var when = App.EventModel.EventStartTime;

                var offset = new DateTimeOffset(when);

                Windows.Data.Xml.Dom.XmlDocument xml = new Windows.Data.Xml.Dom.XmlDocument();

                xml.LoadXml(TOAST);

                // create the notification
                ScheduledToastNotification toast = new ScheduledToastNotification(xml, offset);
                Random rnd = new Random();
                toast.Id = rnd.Next(1, 100000000).ToString();

                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            }

            var emailUri =
                new Uri("mailto:" + App.EventModel.RSVPEmail + "?subject=RSVP: " + App.EventModel.EventName + "&body=" +
                        finalResponse);
            await Launcher.LaunchUriAsync(emailUri);
        }
    }
}