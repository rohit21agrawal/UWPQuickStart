using UWPQuickStart.Models;
using Windows.UI.Xaml.Controls;

namespace UWPQuickStart.Views
{
	public sealed partial class PhotosGridView : UserControl
	{
		public PhotosGridView()
		{
			this.InitializeComponent();
		}

		private void GridView_ItemClick(object sender, ItemClickEventArgs e)
		{
			PhotoStreamModel model = this.DataContext as PhotoStreamModel;
			if (model != null)
			{
				model.SelectedItem = e.ClickedItem as PhotoModel;
				model.ViewSelectionMode = ViewSelectionMode.Flip;
			}
		}
	}
}
