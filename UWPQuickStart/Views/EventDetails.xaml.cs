using System;
using Windows.ApplicationModel.Appointments;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace UWPQuickStart.Views
{
	public sealed partial class EventDetails : UserControl
	{
		public EventDetails()
		{
			this.InitializeComponent();
			this.DataContext = App.EventModel;
			this.InitializeMap();
		}

		private async void InitializeMap()
		{
			BasicGeoposition queryHintGeoPosition = new BasicGeoposition();
			queryHintGeoPosition.Latitude = 47.643;
			queryHintGeoPosition.Longitude = -122.131;
			MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(App.EventModel.EventAddress, new Geopoint(queryHintGeoPosition));
			if (result != null && result.Locations.Count != 0)
			{
				await this.mapControl.TrySetViewAsync(result.Locations[0].Point, 16, 0, 0, MapAnimationKind.None);
			}

			RandomAccessStreamReference mapIconStreamReference = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
			MapIcon mapIcon = new MapIcon();
			mapIcon.Location = this.mapControl.Center;
			mapIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
			mapIcon.Title = "Event location";
			mapIcon.Image = mapIconStreamReference;
			mapIcon.ZIndex = 0;
			this.mapControl.MapElements.Add(mapIcon);
		}

		private async void addEventToCalendar(object sender, RoutedEventArgs e)
		{
			var appointment = new Windows.ApplicationModel.Appointments.Appointment();
			appointment.Subject = App.EventModel.EventName;
			appointment.StartTime = App.EventModel.EventStartTime;
			appointment.Duration = App.EventModel.EventDuration;
			appointment.Details = @"<html><body><div><p>" + App.EventModel.EventInviteText + @"</p>";
			appointment.Details = appointment.Details + @"<p>Driving directions: <a href='bingmaps:?rtp=~adr." + App.EventModel.EventAddress + @"'>" + App.EventModel.EventAddress + @"</a></p></div></body></html>";
			appointment.DetailsKind = Windows.ApplicationModel.Appointments.AppointmentDetailsKind.Html;

			// Get the selection rect of the button pressed to add this appointment 
			var rect = EventDetails.GetElementRect(sender as FrameworkElement);
			String appointmentId = await AppointmentManager.ShowAddAppointmentAsync(appointment, rect, Windows.UI.Popups.Placement.Default);
		}

		public static Rect GetElementRect(Windows.UI.Xaml.FrameworkElement element)
		{
			Windows.UI.Xaml.Media.GeneralTransform transform = element.TransformToVisual(null);
			Point point = transform.TransformPoint(new Point());
			return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
		}

		private async void getDirections(object sender, RoutedEventArgs e)
		{
			var directionsUri = new Uri("bingmaps:?rtp=~adr." + App.EventModel.EventAddress);
			await Windows.System.Launcher.LaunchUriAsync(directionsUri);
		}
	}
}
