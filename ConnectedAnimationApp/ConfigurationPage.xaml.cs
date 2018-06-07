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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ConnectedAnimationApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigurationPage : Page
    {
        public ConfigurationPage()
        {
            this.InitializeComponent();

            SetupPositionConfigurations();

            RootGrid.Children.Add(CreateOrGetElement1());
            //PopupRoot.IsOpen = true;

            animationDefaultDuration = ConnectedAnimationService.GetForCurrentView().DefaultDuration;
        }

        UIElement element1;
        UIElement element2;
        TimeSpan animationDefaultDuration;

        List<Tuple<VerticalAlignment, HorizontalAlignment>> Positions = new List<Tuple<VerticalAlignment, HorizontalAlignment>>()
        {
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Top, HorizontalAlignment.Left),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Top, HorizontalAlignment.Center),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Top, HorizontalAlignment.Right),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Center, HorizontalAlignment.Left),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Center, HorizontalAlignment.Center),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Center, HorizontalAlignment.Right),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Bottom, HorizontalAlignment.Left),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Bottom, HorizontalAlignment.Center),
            new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Bottom, HorizontalAlignment.Right)
        };

        int PositionIndex = 0;
        List<Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>> PositionConfigurations = new List<Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>>();
        void SetupPositionConfigurations()
        {
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[3], Positions[5]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[0], Positions[8]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[1], Positions[7]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[2], Positions[6]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[5], Positions[3]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[8], Positions[0]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[7], Positions[1]));
            PositionConfigurations.Add(new Tuple<Tuple<VerticalAlignment, HorizontalAlignment>, Tuple<VerticalAlignment, HorizontalAlignment>>(Positions[6], Positions[2]));
        }


        UIElement CreateOrGetElement1()
        {
            if (element1 != null) return element1;
            else
            {
                element1 = new Rectangle()
                {
                    Width = 50,
                    Height = 50,
                    Fill = new SolidColorBrush(Windows.UI.Colors.Red),
                    VerticalAlignment = PositionConfigurations[PositionIndex].Item1.Item1,
                    HorizontalAlignment = PositionConfigurations[PositionIndex].Item1.Item2,
                //Shadow = new ThemeShadow(),
                //Translation = new System.Numerics.Vector3(0, 0, 15)
            };
            }

            return element1;
        }

        UIElement CreateOrGetElement2()
        {
            if (element2 != null) return element2;
            else
            {
                element2 = new Rectangle()
                {
                    Width = 75,
                    Height = 75,
                    Fill = new SolidColorBrush(Windows.UI.Colors.Blue),
                    VerticalAlignment = PositionConfigurations[PositionIndex].Item2.Item1,
                    HorizontalAlignment = PositionConfigurations[PositionIndex].Item2.Item2,
                };
            }

            return element2;
        }


        bool direction = true;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var SourceElement = direction ? CreateOrGetElement1() : CreateOrGetElement2();
            var DestinationElement = direction ? CreateOrGetElement2() : CreateOrGetElement1();

            var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("anim", SourceElement);

            ConnectedAnimationService.GetForCurrentView().DefaultDuration = SlowDurationRadioButton.IsChecked == true ? TimeSpan.FromSeconds(1) : animationDefaultDuration;



            RootGrid.Children.Clear();
            RootGrid.Children.Add(DestinationElement);


            anim.TryStart(DestinationElement);


            direction = !direction;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PositionIndex++;
            if (PositionIndex == Positions.Count - 1)
            {
                PositionIndex = 0;
            }

            CreateOrGetElement1();
            CreateOrGetElement2();

            (element1 as Rectangle).VerticalAlignment = PositionConfigurations[PositionIndex].Item1.Item1;
            (element1 as Rectangle).HorizontalAlignment = PositionConfigurations[PositionIndex].Item1.Item2;

            (element2 as Rectangle).VerticalAlignment = PositionConfigurations[PositionIndex].Item2.Item1;
            (element2 as Rectangle).HorizontalAlignment = PositionConfigurations[PositionIndex].Item2.Item2;
        }
    }
}
