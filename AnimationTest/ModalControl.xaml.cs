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

        bool allowMoveAnimation = true;

        private void mouseMoved(object sender, MouseEventArgs e)
        {
            if (allowMoveAnimation)
            {
                allowMoveAnimation = false;
                var pos = e.GetPosition(this);
                Debug.WriteLine("Mousie move!! " + pos.X + " " + pos.Y);
                doRollover(pos);
                allowMoveAnimation = true;
            }
            else
            {
                Debug.WriteLine("blocked");
            }
        }

        private void doRollover(Point pos)
        {
            //  var ipot = Math.Pow(pos.X, 2) + Math.Pow(pos.Y, 2);
            var upperWeight = pos.X + pos.Y - 280;
            //if (upperWeight > 0)
            {
                // Debug.WriteLine("Upper weight : " + upperWeight);
                //var skew = new SkewTransform(0 ,upperWeight / 50);
                //modalGrid.RenderTransform = skew;
               // var rotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 1, 0), upperWeight / 50));
                //model3d.Transform = rotate;

            }
            
        }
    }
}
