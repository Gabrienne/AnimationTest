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
            var factor = e.NewValue.Equals(true) ? 200 : 0;
            ThicknessAnimation animation = new ThicknessAnimation(new Thickness(factor, 0, 0, 0), TimeSpan.FromMilliseconds(factor * 4));
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
            var xWeight = (pos.Y - 150) / 150;
            var yWeight = (pos.X - 250) / 250;
            var upperLeftWeight = (3 * pos.X / 5 + pos.Y - 300) / 50;
            if (easingIn)
            {
                easeIn(xWeight, yWeight);
            }
            else
            {
                Rotate(xWeight, yWeight);
            }
        }

        private void Rotate(double x, double y, double ms = 0)
        {
            var angle = Math.Max(Math.Abs(x), Math.Abs(y)) * 5 / 0.8;
            DoubleAnimation angleAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(ms));
            if (x != 0 && y != 0)
            {
                ((viewPort3d.Transform as RotateTransform3D).Rotation as AxisAngleRotation3D).Axis = new Vector3D(x, y, 0);
            }
            (viewPort3d.Transform as RotateTransform3D).Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleAnimation);
        }

        private void easeIn(double x, double y)
        {
            var angle = Math.Max(Math.Abs(x), Math.Abs(y)) * 5 / 0.8;
            //on mouse enter, ease in 200 ms
            modalGrid.MouseMove -= mouseMoved;
            DoubleAnimation angleAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(300));
            angleAnimation.Completed += new EventHandler((sender2, e2) =>
            {
                easingIn = false;
                modalGrid.MouseMove += mouseMoved;
            });
            ((viewPort3d.Transform as RotateTransform3D).Rotation as AxisAngleRotation3D).Axis = new Vector3D(x, y, 0);
            (viewPort3d.Transform as RotateTransform3D).Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleAnimation);
        }

        private void easeOut(object sender, MouseEventArgs e)
        {
            easingIn = true; //next time the mouse enters, animation should ease in
            Rotate(0, 0, 800);
        }
    }
}
