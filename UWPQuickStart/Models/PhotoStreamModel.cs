using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWPQuickStart.Models
{
    internal enum ViewSelectionMode
    {
        Flip,
        Grid
    }

    internal class PhotoStreamModel : INotifyPropertyChanged
    {
        private PhotoModel _selectedItem;

        private ViewSelectionMode _viewSelectionMode;

        public ViewSelectionMode ViewSelectionMode
        {
            get { return _viewSelectionMode; }
            set
            {
                _viewSelectionMode = value;
                OnPropertyChanged("ViewSelectionMode");
            }
        }

        public PhotoModel SelectedItem
        {
            get
            {
                if (_selectedItem == null && StreamItems.Count != 0)
                {
                    return StreamItems[0];
                }
                return _selectedItem;
            }
            set { _selectedItem = value; }
        }

        public ObservableCollection<PhotoModel> StreamItems { get; set; } = new ObservableCollection<PhotoModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializePhotoCollection()
        {
            StreamItems.Clear();
            for (var i = 0; i < 26; i++)
            {
                StreamItems.Add(new PhotoModel(new Uri("ms-appx:///SamplePhotos/SamplePhoto.jpg")));
            }
        }
    }
}