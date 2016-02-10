using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWPQuickStart.Models
{
	enum ViewSelectionMode
	{
		Flip,
		Grid
	}

    class PhotoStreamModel : INotifyPropertyChanged
    {
		private ViewSelectionMode _viewSelectionMode;
		public ViewSelectionMode ViewSelectionMode
		{ 
			get
			{
				return _viewSelectionMode;
			}
			set
			{
				_viewSelectionMode = value;
				this.OnPropertyChanged("ViewSelectionMode");
			}
		}

		private PhotoModel _selectedItem = null;
		public PhotoModel SelectedItem
		{
			get
			{
				if (_selectedItem == null && _streamItems.Count != 0)
				{
					return _streamItems[0];
				}
				return _selectedItem;
			}
			set { this._selectedItem = value; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private ObservableCollection<PhotoModel> _streamItems = new ObservableCollection<PhotoModel>();
		public ObservableCollection<PhotoModel> StreamItems
        {
            get
            {
                return _streamItems;
            }

            set
            {
                _streamItems = value;
            }
        }

        public void InitializePhotoCollection()
        {
            StreamItems.Clear();
            for(int i = 0; i < 26; i++)
            {
                StreamItems.Add(new PhotoModel(new Uri("ms-appx:///SamplePhotos/SamplePhoto.jpg")));
            }

        }
    }
}

