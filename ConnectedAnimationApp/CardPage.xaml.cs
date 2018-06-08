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
    public sealed partial class CardPage : Page
    {
        int _storedItem;

        public CardPage()
        {
            this.InitializeComponent();

            var items = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                items.Add(i);
            }

            collection.ItemsSource = items;
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ca2", destinationElement);

            //SmokeGrid.Visibility = Visibility.Collapsed;


            // If the connected item appears outside the viewport, scroll it into view.
            collection.ScrollIntoView(_storedItem, ScrollIntoViewAlignment.Default);
            collection.UpdateLayout();

            // Play the second connected animation. 
            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca2");
            if (animation != null)
            {
                animation.Configuration = new DirectConnectedAnimationConfiguration();

                animation.Completed += Animation_Completed;

                await collection.TryStartConnectedAnimationAsync(animation, _storedItem, "connectedElement");
            }
            
        }

        private void Animation_Completed(ConnectedAnimation sender, object args)
        {
            SmokeGrid.Visibility = Visibility.Collapsed;
        }

        private void TipsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the collection item corresponding to the clicked item.
            var container = collection.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if (container != null)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storedItem = Convert.ToInt32(container.Content);

                // Prepare the connected animation.
                // Notice that the stored item is passed in, as well as the name of the connected element. 
                // The animation will actually start on the Detailed info page.
                var animation = collection.PrepareConnectedAnimation("ca1", _storedItem, "connectedElement");

            }

            SmokeGrid.Visibility = Visibility.Visible;

            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca1");
            if (imageAnimation != null)
            {
                
                imageAnimation.TryStart(destinationElement);
            }
        }
    }
}
