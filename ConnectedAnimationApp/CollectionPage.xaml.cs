using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace ConnectedAnimationApp
{
    public sealed partial class CollectionPage : Page
    {
        CustomDataObject _storeditem;

        public CollectionPage()
        {
            this.InitializeComponent();

            // Ensure that the MainPage is only created once, and cached during navigation.
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            collection.ItemsSource = CustomDataObject.GetDataObjects();
        }

        private void collection_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the collection item corresponding to the clicked item.
            var container = collection.ContainerFromItem(e.ClickedItem) as ListViewItem;
            if (container != null)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storeditem = container.Content as CustomDataObject;

                // Prepare the connected animation.
                // Notice that the stored item is passed in, as well as the name of the connected element. 
                // The animation will actually start on the Detailed info page.
                var animation = collection.PrepareConnectedAnimation("ca1", _storeditem, "connectedElement");

            }

            // Navigate to the DetailedInfoPage.
            Frame.Navigate(typeof(DetailedInfoPage), _storeditem, new SuppressNavigationTransitionInfo());
        }



        private async void collection_Loaded(object sender, RoutedEventArgs e)
        {
            if (_storeditem != null)
            {
                // If the connected item appears outside the viewport, scroll it into view.
                collection.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.Default);
                collection.UpdateLayout();

                // Play the second connected animation. 
                ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca2");
                if (animation != null)
                {
                    await collection.TryStartConnectedAnimationAsync(animation, _storeditem, "connectedElement");
                }
            }
        }

        // Setup methods...

        private void AddItemsToCollection(int numberOfItems)
        {
            List<CustomDataObject> items = new List<CustomDataObject>();
            for (int i = 0; i < numberOfItems; i++)
            {
                items.Add(new CustomDataObject() { Title = "Item " + i, ImageLocation=((i%3) + 1).ToString() + ".jpg" });
            }

            collection.ItemsSource = items;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ShowBackArrow();           
        }

        void ShowBackArrow()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;

            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }
       
    }
}
