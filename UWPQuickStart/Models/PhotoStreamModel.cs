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
		private ViewSelectionMode viewSelectionMode;
		public ViewSelectionMode ViewSelectionMode
		{ 
			get
			{
				return viewSelectionMode;
			}
			set
			{
				viewSelectionMode = value;
				this.OnPropertyChanged("ViewSelectionMode");
			}
		}

		private PhotoModel selectedItem = null;
		public PhotoModel SelectedItem
		{
			get
			{
				if (selectedItem == null && _streamItems.Count != 0)
				{
					return _streamItems[0];
				}
				return selectedItem;
			}
			set { this.selectedItem = value; }
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

