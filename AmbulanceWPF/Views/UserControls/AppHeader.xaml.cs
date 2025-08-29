using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AmbulanceWPF.Views.UserControls
{
    public partial class AppHeader : UserControl
    {
        public AppHeader()
        {
            InitializeComponent();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the popup
            ProfilePopup.IsOpen = !ProfilePopup.IsOpen;

            // Animate the dropdown arrow
            var rotateAnimation = new DoubleAnimation
            {
                To = ProfilePopup.IsOpen ? 180 : 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase()
            };

            ArrowRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup when a menu item is clicked
            ProfilePopup.IsOpen = false;

            // Reset arrow animation
            var rotateAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase()
            };

            ArrowRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }
    }
}