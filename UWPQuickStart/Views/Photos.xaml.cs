using System;
using System.ComponentModel;
using UWPQuickStart.Models;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPQuickStart.Views
{
	public sealed partial class Photos : UserControl
    {
        private bool _isInitialized = false;
	    private PhotoStreamModel _photoStreamModel;

        public Photos()
        {
            this.InitializeComponent();
            _photoStreamModel = new PhotoStreamModel();
            _photoStreamModel.PropertyChanged += PhotoStreamModel_PropertyChanged;
            _photoStreamModel.InitializePhotoCollection();
            this.DataContext = _photoStreamModel;
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
                this.UpdateView();
            }
        }

        private void UpdateView()
        {
            //Null reference check
            if (!_isInitialized){return;}
            this.ContentGrid.Children.Clear();
            if (photoFlipViewMode.IsChecked == true)
            {
                this.ContentGrid.Children.Add(new PhotosFlipView());
            }
            else if (photoGridViewMode.IsChecked == true)
            {
                this.ContentGrid.Children.Add(new PhotosGridView());
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
