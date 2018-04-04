using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ConnectedAnimationApp
{
    public sealed partial class MainPage : Page
    {
        CustomDataObject _storeditem;

        public MainPage()
        {
            this.InitializeComponent();

            // Ensure that the MainPage is only created once, and cached during navigation.
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            AddItemsToCollection(100);
        }

        private void collection_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get the GridViewItem corresponding tothe clicked item.
            var container = collection.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if (container != null)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storeditem = container.Content as CustomDataObject;

                // Prepare the connected animation.
                // Notice that the stored item is passed in, as well as the name of the connected element. 
                // The animation will actually start on the Detailed info page.
                //collection.PrepareConnectedAnimation("ca1", _storeditem, "connectedElement");
            }

            // Add a fade out effect
            Transitions = new TransitionCollection();
            Transitions.Add(new ContentThemeTransition());

            // Navigate to the DetailedInfoPage.
            Frame.Navigate(typeof(DetailedInfoPage), _storeditem);
        }



        private async void collection_Loaded(object sender, RoutedEventArgs e)
        {
            //if (_storeditem != null)
            //{
            //    collection.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.Default);
            //    collection.UpdateLayout();

            //    ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ca2");
            //    if (animation != null)
            //    {
            //        //await collection.TryStartConnectedAnimationAsync(animation, _storeditem, "connectedElement");
            //    }
            //}
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ShowBackArrow();

            // Don't use vertical entrance animation with connected animation
            if (e.NavigationMode == NavigationMode.Back)
            {
                EntranceTransition.FromVerticalOffset = 0;
            }
        }

        // Setup methods...

        private void AddItemsToCollection(int numberOfItems)
        {
            List<CustomDataObject> items = new List<CustomDataObject>();
            for (int i = 0; i < numberOfItems; i++)
            {
                items.Add(new CustomDataObject() { Title = "Item " + i });
            }

            collection.ItemsSource = items;
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
