using System;
using System.ComponentModel;

namespace UWPQuickStart.Models
{
	class PhotoModel : INotifyPropertyChanged
    {
        private double imageSize = 128;
        private Uri _imageUri;
        public Uri ImageUri
        {
            get
            {
                return _imageUri;
            }
            set
            {
                _imageUri = value;
                OnPropertyChanged("ImageUri");
            }
        }

		public double ImageSize
		{
			get
			{
				return imageSize;
			}
		}

		public PhotoModel(Uri photoLink)
		{
			this.ImageUri = photoLink;
		}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
