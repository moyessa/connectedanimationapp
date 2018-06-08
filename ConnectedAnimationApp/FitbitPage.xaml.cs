using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ConnectedAnimationApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FitbitPage : Page
    {
        public FitbitPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        UIElement _storedElement; 

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _storedElement = (sender as Grid).Children[0];
            var animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ca1", _storedElement);

            Frame.Navigate(typeof(FullSizeImagePage), null, new SuppressNavigationTransitionInfo());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca2");
            if (imageAnimation != null && _storedElement != null)
            {
                // Baseline connected animation:
                imageAnimation.TryStart(_storedElement);
            }
        }
    }
}
