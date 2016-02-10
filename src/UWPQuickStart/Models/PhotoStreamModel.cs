// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWPQuickStart.Models
{
    /// <summary>
    ///     Defines the two view modes for photos control. Flip view lets users flip through each photo individually. Grid view
    ///     lets users view the photos in a grid.
    /// </summary>
    internal enum ViewSelectionMode
    {
        Flip,
        Grid
    }

    internal class PhotoStreamModel : INotifyPropertyChanged
    {
        private PhotoModel _selectedItem;

        private ViewSelectionMode _viewSelectionMode;

        /// <summary>
        ///     Represents the current view selection mode (either Flip or Grid).
        /// </summary>
        public ViewSelectionMode ViewSelectionMode
        {
            get { return _viewSelectionMode; }
            set
            {
                _viewSelectionMode = value;
                OnPropertyChanged("ViewSelectionMode");
            }
        }

        /// <summary>
        ///     Enables the XAML to pass the SelectedItem information from the Grid view mode to the Flip view mode. This will make
        ///     it so the picture that is clicked on in Grid view is the first one visible in Flip view.
        /// </summary>
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

        /// <summary>
        ///     This object will be passed to the Photos control to specify all of the photos to be displayed in the view.
        /// </summary>
        public ObservableCollection<PhotoModel> StreamItems { get; set; } = new ObservableCollection<PhotoModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        ///     Initialize the photo collection. In this example, we just used the same photo many times. Add your own photos to
        ///     represent your event!
        /// </summary>
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