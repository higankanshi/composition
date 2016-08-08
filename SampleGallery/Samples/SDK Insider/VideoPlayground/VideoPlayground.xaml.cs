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

using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace CompositionSampleGallery
{
    public sealed partial class VideoPlayground : SamplePage
    {
        // Private members
        private VideoPlaygroundViewModel _viewModel;

        public VideoPlayground()
        {
            this.InitializeComponent();
            
            // Initialize our view model and set it as our DataContext
            _viewModel = new VideoPlaygroundViewModel(
                ElementCompositionPreview.GetElementVisual(this).Compositor,
                VideoContentGrid);

            this.DataContext = _viewModel;
        }

        public static string        StaticSampleName    { get { return "Video Playground"; } }
        public override string      SampleName          { get { return StaticSampleName; } }
        public override string      SampleDescription   { get { return "Add lighting and effects to video"; } }


        /// <summary>
        /// Informs the view model when there is a tapp to the AddLightPromptGrid.
        /// </summary>
        private void AddLightPromptGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var point = e.GetPosition(sender as UIElement);

            _viewModel.AddLight((float)point.X, (float)point.Y);
        }

        /// <summary>
        /// Informs the view model when there is a tapp to the RemoveLightPromptGrid.
        /// </summary>
        private void RemoveLightPromptGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var point = e.GetPosition(sender as UIElement);

            _viewModel.TryRemoveLight((float)point.X, (float)point.Y);
        }

        /// <summary>
        /// Unload our resources.
        /// </summary>
        private void SamplePage_Unloaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Cleanup();
        }
    }
}
