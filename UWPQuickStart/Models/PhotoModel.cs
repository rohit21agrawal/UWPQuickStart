// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;

namespace UWPQuickStart.Models
{
    //Object to represent the photos used in the photos control in the app. 
    internal class PhotoModel : INotifyPropertyChanged
    {
        private const double _imageSize = 128;
        private Uri _imageUri;

        public PhotoModel(Uri photoLink)
        {
            ImageUri = photoLink;
        }

        public Uri ImageUri
        {
            get { return _imageUri; }
            set
            {
                _imageUri = value;
                OnPropertyChanged("ImageUri");
            }
        }

        public double ImageSize => _imageSize;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}