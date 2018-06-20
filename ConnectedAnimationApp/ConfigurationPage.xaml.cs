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

            animationDefaultDuration = ConnectedAnimationService.GetForCurrentView().DefaultDuration;

            sharedShadow.Receivers.Add(BackgroundGrid);
        }

        UIElement element1;
        UIElement element2;
        TimeSpan animationDefaultDuration;
        ThemeShadow sharedShadow = new ThemeShadow();

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
                    Shadow = sharedShadow,
                    TranslationTransition = new Vector3Transition() { Duration = TimeSpan.FromSeconds(1) }
                };
                //element1 = new TextBlock()
                //{
                //    Text = "I am text!",
                //    FontSize = 30,
                //    Foreground = new SolidColorBrush(Windows.UI.Colors.Black)
                //};


                UpdateElement1Position();
            }

            return element1;
        }

        void UpdateElement1Position()
        {
            (element1 as FrameworkElement).VerticalAlignment = PositionConfigurations[PositionIndex].Item1.Item1;
            (element1 as FrameworkElement).HorizontalAlignment = PositionConfigurations[PositionIndex].Item1.Item2;
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
                    Shadow = sharedShadow,
                    TranslationTransition = new Vector3Transition() { Duration = TimeSpan.FromSeconds(1) }
                };

                UpdateElement2Position();
            }

            return element2;
        }

        void UpdateElement2Position()
        {
            (element2 as FrameworkElement).VerticalAlignment = PositionConfigurations[PositionIndex].Item2.Item1;
            (element2 as FrameworkElement).HorizontalAlignment = PositionConfigurations[PositionIndex].Item2.Item2;

        }


        bool direction = true;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var SourceElement = direction ? CreateOrGetElement1() : CreateOrGetElement2();
            var DestinationElement = direction ? CreateOrGetElement2() : CreateOrGetElement1();

            var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("anim", SourceElement);

            ConnectedAnimationService.GetForCurrentView().DefaultDuration = SlowDurationRadioButton.IsChecked == true ? TimeSpan.FromSeconds(1) : animationDefaultDuration;

            var config = GetSelectedConfiguration();
            if (config != null)
            {
                anim.Configuration = config;
            }

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

            UpdateElement1Position();
            UpdateElement2Position();
        }

        private ConnectedAnimationConfiguration GetSelectedConfiguration()
        {
            List<RadioButton> buttons = new List<RadioButton>();
            foreach (UIElement e in ConfigurationStackPanel.Children)
            {
                if ((e as RadioButton) != null && (e as RadioButton).IsChecked == true)
                {
                    return GetConfigurationFromRadioButton(e as RadioButton);
                }
            }

            return null;
        }

        private ConnectedAnimationConfiguration GetConfigurationFromRadioButton(RadioButton b)
        {
            switch (b.Content as String)
            {
                case "Default":
                    return null;

                case "Gravity":
                    return new GravityConnectedAnimationConfiguration();

                case "Direct":
                    return new DirectConnectedAnimationConfiguration();

                case "Basic":
                    return new BasicConnectedAnimationConfiguration();
            }
            return null;
        }

        int GetZDepth()
        {
            int ZDepthValue = 32;
            bool b = Int32.TryParse(ZDepthTextBox.Text, out ZDepthValue);
            if (!b) return 32;
            ZDepthTextBox.Text = ZDepthValue.ToString();
            return ZDepthValue;
        }

        private void Element1ZCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CreateOrGetElement1().Translation = new System.Numerics.Vector3(0, 0, GetZDepth());
        }
        private void Element2ZCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CreateOrGetElement2().Translation = new System.Numerics.Vector3(0, 0, GetZDepth());
        }
        private void Element1ZCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CreateOrGetElement1().Translation = new System.Numerics.Vector3(0);
        }
        private void Element2ZCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CreateOrGetElement2().Translation = new System.Numerics.Vector3(0);
        }
    }
}
