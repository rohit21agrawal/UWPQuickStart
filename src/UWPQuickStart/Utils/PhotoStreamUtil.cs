// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UWPQuickStart.Models;

namespace UWPQuickStart.Utils
{
    internal class PhotoStreamUtil
    {
        private const string ImgurApiClientId = "ENTER IMGUR API CLIENT ID HERE";
        private const string AlbumDeleteHash = "ENTER ALBUM DELETE HASH HERE";
        private const string AlbumId = "ENTER ALBUM ID HERE";
        private static HttpClient _imgurClient;

        /// <summary>
        ///     Initializes the imgur httpClient with the right authentication header values to call the appropriate anoynmous REST
        ///     APIs
        /// </summary>
        private static void InitializeImgurClient()
        {
            if (_imgurClient == null)
            {
                _imgurClient = new HttpClient();
                _imgurClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID",
                    ImgurApiClientId);
            }
        }

        /// <summary>
        ///     Converts stream s to base 64 data to be uploaded to imgur
        /// </summary>
        private static string ConvertStreamToBase64String(Stream s)
        {
            s.Position = 0;
            var memoryStream = new MemoryStream();
            s.CopyTo(memoryStream);

            var data = memoryStream.ToArray();
            return Convert.ToBase64String(data);
        }

        /// <summary>
        ///     Gets the image link information for all images in our specific anonymous album
        /// </summary>
        internal static async Task GetImagesFromImgur(PhotoStreamModel photoStreamModel)
        {
            InitializeImgurClient();
            photoStreamModel.StreamItems.Clear();
            var response = await _imgurClient.GetAsync("https://api.imgur.com/3/album/" + AlbumId + "/images");
            var content = await response.Content.ReadAsStringAsync();

            dynamic friendlyContent = JsonConvert.DeserializeObject<dynamic>(content);
            if (friendlyContent.data.Count == null)
            {
                return;
            }
            var imageLinksArray = new string[friendlyContent.data.Count];
            for (var i = 0; i < friendlyContent.data.Count; i++)
            {
                imageLinksArray[i] = friendlyContent.data[i].link;
            }
            foreach (var s in imageLinksArray)
            {
                photoStreamModel.StreamItems.Add(new PhotoModel(new Uri(s)));
            }
        }

        /// <summary>
        ///     Calls helper methods to add an image to imgur and then add it to our album
        /// </summary>
        internal static async Task AddImage(Stream s, PhotoStreamModel photoStreamModel)
        {
            await UploadImageToImgur(s, photoStreamModel);
        }

        /// <summary>
        ///     Uploads the image to imgur
        /// </summary>
        private static async Task UploadImageToImgur(Stream s, PhotoStreamModel photoStreamModel)
        {
            InitializeImgurClient();
            var uploadContent = new StringContent(ConvertStreamToBase64String(s));

            var response = await _imgurClient.PostAsync("https://api.imgur.com/3/upload", uploadContent);
            var content = await response.Content.ReadAsStringAsync();

            dynamic friendlyContent = JsonConvert.DeserializeObject<dynamic>(content);
            string imageLink = friendlyContent.data.link;
            string imageId = friendlyContent.data.id;
            photoStreamModel.StreamItems.Add(new PhotoModel(new Uri(imageLink)));
            await AddImageToAlbum(imageId);
        }

        /// <summary>
        ///     Adds the image to our album
        /// </summary>
        private static async Task AddImageToAlbum(string id)
        {
            InitializeImgurClient();
            var response =
                await
                    _imgurClient.PutAsync("https://api.imgur.com/3/album/" + AlbumDeleteHash + "/add",
                        new StringContent("ids[]=" + id));
            var content = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        ///     Creates an anonymous imgur album. Only needs to happen once - make sure you record the album ID and album delete
        ///     hash in the variables at the top of this class
        /// </summary>
        internal static async Task CreateAlbum()
        {
            InitializeImgurClient();

            var response = await _imgurClient.PostAsync("https://api.imgur.com/3/album", null);
            var content = await response.Content.ReadAsStringAsync();
        }
    }
}