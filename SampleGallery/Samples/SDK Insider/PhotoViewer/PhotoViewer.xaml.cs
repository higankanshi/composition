﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH 
// THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//*********************************************************

using CompositionSampleGallery.Shared;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CompositionSampleGallery
{
    public sealed partial class PhotoViewer : SamplePage
    {
        public PhotoViewer()
        {
            Model = new LocalDataSource();
            this.InitializeComponent();
        }

        public static string        StaticSampleName    { get { return "Photo Popup Viewer"; } }
        public override string      SampleName          { get { return StaticSampleName; } }
        public override string      SampleDescription   { get { return "Demonstrates how use ListView and Effects to create a dynamic basic photo viewing experience. Click on any thumbnail and notice the smooth transition and color shifting in the blurred background."; } }
        public override string      SampleCodeUri       { get { return "http://go.microsoft.com/fwlink/p/?LinkID=761180"; } }

        public LocalDataSource Model { set; get; }

        private void LoadData()
        {
            Func<object, bool, Uri> getImageForThumbnail = (o, large) =>
            {
                return new Uri(((Thumbnail)o).ImageUrl);
            };

            ImagePopupViewer.Show(Model.Items[0].ImageUrl, Model.Items, getImageForThumbnail, new Thickness(50, 50, 50, 50), this);
        }

        private void SamplePage_Loading(Windows.UI.Xaml.FrameworkElement sender, object args)
        {
            LoadData();
        }
    }
}
