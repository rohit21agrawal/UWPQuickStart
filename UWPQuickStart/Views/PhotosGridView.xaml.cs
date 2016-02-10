using Windows.UI.Xaml.Controls;
using UWPQuickStart.Models;

namespace UWPQuickStart.Views
{
    public sealed partial class PhotosGridView : UserControl
    {
        public PhotosGridView()
        {
            InitializeComponent();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var model = DataContext as PhotoStreamModel;
            if (model != null)
            {
                model.SelectedItem = e.ClickedItem as PhotoModel;
                model.ViewSelectionMode = ViewSelectionMode.Flip;
            }
        }
    }
}