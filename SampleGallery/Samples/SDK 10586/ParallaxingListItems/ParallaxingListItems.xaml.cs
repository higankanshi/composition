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
using SamplesCommon;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;


namespace CompositionSampleGallery
{
    public sealed partial class ParallaxingListItems : SamplePage
    {
        private ExpressionAnimation _parallaxExpression;
        private CompositionPropertySet _scrollProperties;

        public ParallaxingListItems()
        {
            Model = new LocalDataSource();
            this.InitializeComponent();
        }

        public static string       StaticSampleName     { get { return "Parallaxing ListView Items"; } }
        public override string     SampleName           { get { return StaticSampleName; } }
        public override string     SampleDescription    { get { return "Demonstrates how to apply a parallax effect to each item in a ListView control. As you scroll the ListView control watch as each ListView item translates at a different rate in comparison to the ListView's scroll position."; } }
        public override string     SampleCodeUri        { get { return "http://go.microsoft.com/fwlink/p/?LinkID=761169"; } }

        public LocalDataSource Model { set; get; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Compositor compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            // Get scrollviewer
            ScrollViewer myScrollViewer = ThumbnailList.GetFirstDescendantOfType<ScrollViewer>();
            _scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(myScrollViewer);

            // Setup the expression
            _parallaxExpression = compositor.CreateExpressionAnimation();
            _parallaxExpression.SetScalarParameter("StartOffset", 0.0f);
            _parallaxExpression.SetScalarParameter("ParallaxValue", 0.5f);
            _parallaxExpression.SetScalarParameter("ItemHeight", 0.0f);
            _parallaxExpression.SetReferenceParameter("ScrollManipulation", _scrollProperties);
            _parallaxExpression.Expression = "(ScrollManipulation.Translation.Y + StartOffset - (0.5 * ItemHeight)) * ParallaxValue - (ScrollManipulation.Translation.Y + StartOffset - (0.5 * ItemHeight))";

            ThumbnailList.ItemsSource = Model.Items;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_parallaxExpression != null)
            {
                _parallaxExpression.Dispose();
                _parallaxExpression = null;
            }

            if (_scrollProperties != null)
            {
                _scrollProperties.Dispose();
                _scrollProperties = null;
            }
        }

        private void ThumbanilList_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            Thumbnail thumbnail = args.Item as Thumbnail;
            Image image = args.ItemContainer.ContentTemplateRoot.GetFirstDescendantOfType<Image>();

            Visual visual = ElementCompositionPreview.GetElementVisual(image);
            visual.Size = new Vector2(960f, 960f);

            if (_parallaxExpression != null)
            {
                _parallaxExpression.SetScalarParameter("StartOffset", (float)args.ItemIndex * visual.Size.Y / 4.0f);
                visual.StartAnimation("Offset.Y", _parallaxExpression);
            }
        }
    }
}
