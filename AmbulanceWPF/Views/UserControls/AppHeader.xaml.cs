using Microsoft.Extensions.DependencyModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        // === Dependency Properties for Commands ===


        public static readonly DependencyProperty HomeCommandProperty =
            DependencyProperty.Register("HomeCommand", typeof(ICommand), typeof(AppHeader), new PropertyMetadata(null));

        public static readonly DependencyProperty LogoutCommandProperty =
            DependencyProperty.Register("LogoutCommand", typeof(ICommand), typeof(AppHeader), new PropertyMetadata(null));

        public static readonly DependencyProperty ThemeCommandProperty =
            DependencyProperty.Register("ThemeCommand", typeof(ICommand), typeof(AppHeader), new PropertyMetadata(null));

        public static readonly DependencyProperty ViewProfileCommandProperty =
            DependencyProperty.Register("ViewProfileCommand", typeof(ICommand), typeof(AppHeader), new PropertyMetadata(null));

        public ICommand HomeCommand
        {
            get => (ICommand)GetValue(HomeCommandProperty);
            set => SetValue(HomeCommandProperty, value);
        }

        public ICommand LogoutCommand
        {
            get => (ICommand)GetValue(LogoutCommandProperty);
            set => SetValue(LogoutCommandProperty, value);
        }

        public ICommand ThemeCommand
        {
            get => (ICommand)GetValue(ThemeCommandProperty);
            set => SetValue(ThemeCommandProperty, value);
        }

        public ICommand ViewProfileCommand
        {
            get => (ICommand)GetValue(ViewProfileCommandProperty);
            set => SetValue(ViewProfileCommandProperty, value);
        }

        // === UI Logic ===
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePopup.IsOpen = !ProfilePopup.IsOpen;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            ProfilePopup.IsOpen = false;
        }

        private void ProfilePopup_Opened(object sender, System.EventArgs e)
        {
            var rotate = new DoubleAnimation(0, 180, TimeSpan.FromMilliseconds(200));
            ArrowRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotate);
        }

        private void ProfilePopup_Closed(object sender, System.EventArgs e)
        {
            var rotate = new DoubleAnimation(180, 0, TimeSpan.FromMilliseconds(200));
            ArrowRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotate);
        }

        // region === User Info Dependency Properties ===

        public static readonly DependencyProperty DoctorNameProperty =
            DependencyProperty.Register(nameof(DoctorName), typeof(string), typeof(AppHeader));

        public static readonly DependencyProperty DoctorInitialsProperty =
            DependencyProperty.Register(nameof(DoctorInitials), typeof(string), typeof(AppHeader));

        public static readonly DependencyProperty DoctorEmailProperty =
            DependencyProperty.Register(nameof(DoctorEmail), typeof(string), typeof(AppHeader));

        public static readonly DependencyProperty DoctorRoleProperty =
            DependencyProperty.Register(nameof(DoctorRole), typeof(string), typeof(AppHeader));

        public string DoctorName
        {
            get => (string)GetValue(DoctorNameProperty);
            set => SetValue(DoctorNameProperty, value);
        }

        public string DoctorInitials
        {
            get => (string)GetValue(DoctorInitialsProperty);
            set => SetValue(DoctorInitialsProperty, value);
        }

        public string DoctorEmail
        {
            get => (string)GetValue(DoctorEmailProperty);
            set => SetValue(DoctorEmailProperty, value);
        }

        public string DoctorRole
        {
            get => (string)GetValue(DoctorRoleProperty);
            set => SetValue(DoctorRoleProperty, value);
        }


    }
}