using System;
using System.Text;
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
			BasicGeoposition queryHintGeoPosition = new BasicGeoposition()
			{
                Latitude = 47.643,
                Longitude = -122.131
            };
			MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(App.EventModel.EventAddress, new Geopoint(queryHintGeoPosition));
			if (result != null && result.Locations.Count != 0)
			{
				await this.mapControl.TrySetViewAsync(result.Locations[0].Point, 16, 0, 0, MapAnimationKind.None);
			}

			RandomAccessStreamReference mapIconStreamReference = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
			MapIcon mapIcon = new MapIcon()
			{
                Location = this.mapControl.Center,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Title = "Event location",
                Image = mapIconStreamReference,
                ZIndex = 0
            };
			
			this.mapControl.MapElements.Add(mapIcon);
		}

		private async void AddEventToCalendar(object sender, RoutedEventArgs e)
		{
		    var appointment = new Appointment
		    {
		        Subject = App.EventModel.EventName,
		        StartTime = App.EventModel.EventStartTime,
		        Duration = App.EventModel.EventDuration,
		        Details = 
		            @"<html><body><div><p>" + App.EventModel.EventInviteText + @"</p>" +
		            @"<p>Driving directions: <a href='bingmaps:?rtp=~adr." + App.EventModel.EventAddress + @"'>" +
		            App.EventModel.EventAddress + @"</a></p></div></body></html>",
		        DetailsKind = AppointmentDetailsKind.Html
		    };

			// Get the selection rect of the button pressed to add this appointment 
			var rect = GetElementRect(sender as FrameworkElement);
			await AppointmentManager.ShowAddAppointmentAsync(appointment, rect, Windows.UI.Popups.Placement.Default);
		}

		public static Rect GetElementRect(FrameworkElement element)
		{
			Windows.UI.Xaml.Media.GeneralTransform transform = element.TransformToVisual(null);
			Point point = transform.TransformPoint(new Point());
			return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
		}

		private async void GetDirections(object sender, RoutedEventArgs e)
		{
			var directionsUri = new Uri("bingmaps:?rtp=~adr." + App.EventModel.EventAddress);
			await Windows.System.Launcher.LaunchUriAsync(directionsUri);
		}
	}
}
