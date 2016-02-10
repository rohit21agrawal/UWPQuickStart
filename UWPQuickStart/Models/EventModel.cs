using System;

namespace UWPQuickStart.Models
{
    public class EventModel
    {
        public EventModel()
        {
            EventName = "John's Band Debut";
            EventAddress = "Pike Place Market, Seattle";
            EventStartTime = new DateTime(2016, 3, 4, 20, 0, 0);
            EventDuration = new TimeSpan(4, 0, 0);
            EventInviteText = "It's a Friday night and I feel like jamming. Join me for some good music!";
            EventStartTimeFriendly = EventStartTime.ToString("dddd, MMMM dd, yyyy") + " at " +
                                     EventStartTime.ToString("h:mm tt");
            RSVPEmail = "myemail@myemailprovider.com";
        }

        public string EventName { get; set; }
        public string EventAddress { get; set; }
        public DateTime EventStartTime { get; set; }
        public TimeSpan EventDuration { get; set; }
        public string EventInviteText { get; set; }
        public string EventStartTimeFriendly { get; set; }
        public string RSVPEmail { get; set; }
    }
}