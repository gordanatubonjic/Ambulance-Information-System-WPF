using AmbulanceWPF.ViewModels;
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
        public AppHeader(DoctorHomePageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

                 public ICommand HomeCommand
        {
            get { return (ICommand)GetValue(HomeCommandProperty); }
            set { SetValue(HomeCommandProperty, value); }
        }

        public static readonly DependencyProperty HomeCommandProperty =
            DependencyProperty.Register("HomeCommand", typeof(ICommand), typeof(AppHeader));

                 public ICommand LogoutCommand
        {
            get { return (ICommand)GetValue(LogoutCommandProperty); }
            set { SetValue(LogoutCommandProperty, value); }
        }

        public static readonly DependencyProperty LogoutCommandProperty =
            DependencyProperty.Register("LogoutCommand", typeof(ICommand), typeof(AppHeader));

                 public ICommand ThemeCommand
        {
            get { return (ICommand)GetValue(ThemeCommandProperty); }
            set { SetValue(ThemeCommandProperty, value); }
        }

        public static readonly DependencyProperty ThemeCommandProperty =
            DependencyProperty.Register("ThemeCommand", typeof(ICommand), typeof(AppHeader));

                 public ICommand ViewProfileCommand
        {
            get { return (ICommand)GetValue(ViewProfileCommandProperty); }
            set { SetValue(ViewProfileCommandProperty, value); }
        }

        public static readonly DependencyProperty ViewProfileCommandProperty =
            DependencyProperty.Register("ViewProfileCommand", typeof(ICommand), typeof(AppHeader));

                 public string DoctorName
        {
            get { return (string)GetValue(DoctorNameProperty); }
            set { SetValue(DoctorNameProperty, value); }
        }

        public static readonly DependencyProperty DoctorNameProperty =
            DependencyProperty.Register("DoctorName", typeof(string), typeof(AppHeader));

                 public string DoctorInitials
        {
            get { return (string)GetValue(DoctorInitialsProperty); }
            set { SetValue(DoctorInitialsProperty, value); }
        }

        public static readonly DependencyProperty DoctorInitialsProperty =
            DependencyProperty.Register("DoctorInitials", typeof(string), typeof(AppHeader));

                 public string DoctorEmail
        {
            get { return (string)GetValue(DoctorEmailProperty); }
            set { SetValue(DoctorEmailProperty, value); }
        }

        public static readonly DependencyProperty DoctorEmailProperty =
            DependencyProperty.Register("DoctorEmail", typeof(string), typeof(AppHeader));

                 public string DoctorRole
        {
            get { return (string)GetValue(DoctorRoleProperty); }
            set { SetValue(DoctorRoleProperty, value); }
        }

        public static readonly DependencyProperty DoctorRoleProperty =
            DependencyProperty.Register("DoctorRole", typeof(string), typeof(AppHeader));

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePopup.IsOpen = !ProfilePopup.IsOpen;
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
            ProfilePopup.IsOpen = false;
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