using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace ConnectedAnimationApp
{
    public sealed partial class DetailedInfoPage : Page
    {
        public CustomDataObject DetailedObject { get; set; }
        public DetailedInfoPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Store the item to be used in binding to UI
            DetailedObject = e.Parameter as CustomDataObject;
            ShowBackArrow();

            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca1");
            if (imageAnimation != null)
            {
                // Baseline connected animation:
                //imageAnimation.TryStart(detailedImage);

                // Connected animation + coordinated animation
                //imageAnimation.TryStart(detailedImage, new UIElement[] { coordinatedPanel });

                // Connected animation + coordinated animation + choreographed animation 
                // Chain an animation after the connected animation completes using Implicit show animations
                CreateImplicitAnimations();
                imageAnimation.Completed += ImageAnimation_Completed;
                imageAnimation.TryStart(detailedImage, new UIElement[] { coordinatedPanel });

            }
        }



        // Create connected animation back to collection page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ca2", detailedImage);
        }



        // Choreographed animations:
        Windows.UI.Composition.Compositor _compositor = null;
        void FetchCompositor()
        {
            if (_compositor == null)
                _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
        }

        void CreateImplicitAnimations()
        {
            FetchCompositor();
            moreInfoPanel.Visibility = Visibility.Collapsed;

            // Animate the header background scale when it first shows.
            var scaleHeaderAnimation = _compositor.CreateScalarKeyFrameAnimation();
            scaleHeaderAnimation.InsertKeyFrame(0, .5f);
            scaleHeaderAnimation.InsertKeyFrame(1, 1f);
            scaleHeaderAnimation.Duration = TimeSpan.FromSeconds(.25);
            scaleHeaderAnimation.Target = "Scale.Y";

            ElementCompositionPreview.SetImplicitShowAnimation(headerBackground, scaleHeaderAnimation);

            // Animate the "more info" panel when it first shows.
            var fadeMoreInfoPanel = _compositor.CreateScalarKeyFrameAnimation();
            fadeMoreInfoPanel.InsertKeyFrame(0, 0f);
            fadeMoreInfoPanel.InsertKeyFrame(1, 1f);
            fadeMoreInfoPanel.Duration = TimeSpan.FromSeconds(.5);
            fadeMoreInfoPanel.Target = "Opacity";

            ElementCompositionPreview.SetImplicitShowAnimation(moreInfoPanel, fadeMoreInfoPanel);
        }

        // Toggle the panel to visible after the connected animation completes. 
        private void ImageAnimation_Completed(ConnectedAnimation sender, object args)
        {
            moreInfoPanel.Visibility = Visibility.Visible;
        }


        // Setup methods...
        void ShowBackArrow()
        {
            //Frame rootFrame = Window.Current.Content as Frame;

            //if (rootFrame.CanGoBack)
            //{
            //    // Show UI in title bar if opted-in and in-app backstack is not empty.
            //    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            //        AppViewBackButtonVisibility.Visible;
            //}
            //else
            //{
            //    // Remove the UI from the title bar if in-app back stack is empty.
            //    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            //        AppViewBackButtonVisibility.Collapsed;
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
