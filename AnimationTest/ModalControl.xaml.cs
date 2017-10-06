using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace AnimationTest
{
    /// <summary>
    /// Interaction logic for Modal.xaml
    /// </summary>
    public partial class ModalControl : UserControl
    {
        private MovieItem _movie;
        public MovieItem Movie
        {
            get
            {
                return _movie;
            }
            set
            {
                _movie = value;
                this.DataContext = _movie;
            }
        }
        public ModalControl()
        {
            InitializeComponent();
            IsVisibleChanged += DoStartupAnimation;
        }

        private void DoStartupAnimation(object sender, DependencyPropertyChangedEventArgs e)
        {
            ThicknessAnimation animation = new ThicknessAnimation(new Thickness(200, 0, 0, 0), TimeSpan.FromMilliseconds(700));
            descriptions.BeginAnimation(Control.MarginProperty, animation);
        }

        public ModalControl(MovieItem movie)
        {
            InitializeComponent();
            this.DataContext = movie;
        }

        private void closing(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        public bool easingIn = true;

        private void mouseMoved(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            var upperWeight = (3 * pos.X / 5 + pos.Y - 300) / 50;
            if (easingIn)
            {
                easeIn(upperWeight);
            }
            else
            {
                //Debug.WriteLine("Mousie move!! " + pos.X + " " + pos.Y);
                Rotate(upperWeight);
            }
        }

        private void Rotate(double angle, double ms = 0)
        {
            DoubleAnimation angleResetAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(ms));
            (viewPort3d.Transform as RotateTransform3D).Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleResetAnimation);
        }

        private void easeIn(Double angle)
        {
            //on mouse enter, ease in 200 ms
            modalGrid.MouseMove -= mouseMoved;
            DoubleAnimation angleAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(200));
            angleAnimation.Completed += new EventHandler((sender2, e2) =>
            {
                easingIn = false;
                modalGrid.MouseMove += mouseMoved;
            });
            (viewPort3d.Transform as RotateTransform3D).Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleAnimation);
        }

        private void easeOut(object sender, MouseEventArgs e)
        {
            easingIn = true; //next time the mouse enters, animation should ease in
           // var currentRotation = (int) ((viewPort3d.Transform as RotateTransform3D).Rotation as AxisAngleRotation3D).Angle;
            Rotate(0, 800);
        }
    }
}
