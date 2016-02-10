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
        bool isInitialized = false;
        private PhotoStreamModel photoStreamModel = new PhotoStreamModel();

        public Photos()
        {
            photoStreamModel.PropertyChanged += PhotoStreamModel_PropertyChanged;
            photoStreamModel.InitializePhotoCollection();
            this.InitializeComponent();
            this.DataContext = photoStreamModel;
            isInitialized = true;
            UpdateView();
        }

        private void PhotoStreamModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ViewSelectionMode")
            {
                if (photoStreamModel.ViewSelectionMode == ViewSelectionMode.Flip)
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
            if (!isInitialized) return;

            if (photoFlipViewMode.IsChecked == true)
            {
                this.ContentGrid.Children.Clear();
                this.ContentGrid.Children.Add(new PhotosFlipView());
            }
            if (photoGridViewMode.IsChecked == true)
            {
                this.ContentGrid.Children.Clear();
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


        private async void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            
            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            BitmapImage bmp = new BitmapImage();
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            bmp.SetSource(stream);

            bool toSave = false;
            var cd = new ContentDialog();
            var grid = new Grid();
            grid.Children.Add(new Image
            {
                Source = bmp,
                Stretch = Stretch.UniformToFill
            });

            cd.Content = grid;
            cd.Title = "";
            cd.PrimaryButtonText = "Add to Stream";
            cd.PrimaryButtonClick += delegate
            {
                toSave = true;
            };
            cd.SecondaryButtonText = "Cancel";

            await cd.ShowAsync();
            if (toSave)
            {
                //Add to stream code
                //await photoStreamModel.AddPhoto(stream.AsStream());
            }
        }

        private async void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();

            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".gif");
            filePicker.FileTypeFilter.Add(".tiff");

            StorageFile storageFile = await filePicker.PickSingleFileAsync();

            if (storageFile != null)
            {
                BitmapImage bmp = new BitmapImage();
                IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
                bmp.SetSource(stream);

                bool toSave = false;
                var cd = new ContentDialog();
                var grid = new Grid();
                grid.Children.Add(new Image
                {
                    Source = bmp,
                    Stretch = Stretch.UniformToFill
                });

                cd.Content = grid;
                cd.Title = "";
                cd.PrimaryButtonText = "Add to Stream";
                cd.PrimaryButtonClick += delegate
                {
                    toSave = true;
                };
                cd.SecondaryButtonText = "Cancel";

                await cd.ShowAsync();
                if (toSave)
                {
                    //Add to stream code
                    //await photoStreamModel.AddPhoto(stream.AsStream());
                }
            }
        }
    }
}
