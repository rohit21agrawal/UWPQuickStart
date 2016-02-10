using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Models;

namespace UWPQuickStart.Views
{
    public sealed partial class Photos : UserControl
    {
        private readonly bool _isInitialized;
        private readonly PhotoStreamModel _photoStreamModel;

        public Photos()
        {
            InitializeComponent();
            _photoStreamModel = new PhotoStreamModel();
            _photoStreamModel.PropertyChanged += PhotoStreamModel_PropertyChanged;
            _photoStreamModel.InitializePhotoCollection();
            DataContext = _photoStreamModel;
            _isInitialized = true;
            UpdateView();
        }

        private void PhotoStreamModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ViewSelectionMode")
            {
                if (_photoStreamModel.ViewSelectionMode == ViewSelectionMode.Flip)
                {
                    photoFlipViewMode.IsChecked = true;
                }
                else
                {
                    photoGridViewMode.IsChecked = true;
                }
                UpdateView();
            }
        }

        private void UpdateView()
        {
            //Null reference check
            if (!_isInitialized)
            {
                return;
            }
            ContentGrid.Children.Clear();
            if (photoFlipViewMode.IsChecked == true)
            {
                ContentGrid.Children.Add(new PhotosFlipView());
            }
            else if (photoGridViewMode.IsChecked == true)
            {
                ContentGrid.Children.Add(new PhotosGridView());
            }
        }

        private void photoGridViewMode_Checked(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }

        private void photoFlipViewMode_Checked(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
    }
}