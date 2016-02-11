// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.IO;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPQuickStart.Models;
using UWPQuickStart.Utils;

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

        private async void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
        }

        private async void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();

            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".jpg");

            StorageFile photo = await filePicker.PickSingleFileAsync();
            SavePhoto(photo);
        }

        private async void SavePhoto(StorageFile photo)
        {
            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            await PhotoStreamUtil.AddImage(stream.AsStream(), _photoStreamModel);
        }
    }
}