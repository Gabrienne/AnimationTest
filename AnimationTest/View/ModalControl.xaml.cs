using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimationTest
{
    /// <summary>
    /// Interaction logic for ModalControl.xaml
    /// </summary>
    public partial class ModalControl : UserControl
    {
        Point mainWindowAnchor = new Point(0, 0);

        public ModalControl()
        {
            InitializeComponent();
        }

        public void AnimateIn(MovieItem movie, Point fromPoint, Point toPoint)
        {
            this.DataContext = movie;
            mainWindowAnchor = fromPoint;
            var upperLeft = new Point(toPoint.X - 300, toPoint.Y - 275); //we need the upper left corner of the final window
            var duration = TimeSpan.FromMilliseconds(800);
            var startup = new Thickness(mainWindowAnchor.X, mainWindowAnchor.Y, 0, 0);
            var finish = new Thickness(upperLeft.X, upperLeft.Y, 0, 0);

            DoubleAnimation scaleAnimation = new DoubleAnimation(0.66, 1, duration);
            ThicknessAnimation posterTranslation = new ThicknessAnimation(startup, finish, duration);
            DoubleAnimation descriptionPopUp = new DoubleAnimation(300, 600, duration);

            posterTranslation.Completed += new EventHandler((sender2, e2) =>
            {
               
                descriptionPopUp.Completed += new EventHandler((sender3, e3) =>
                {
                    //ensure initial keyboard focus
                    btn_Close.Focus();
                });

                this.BeginAnimation(Control.WidthProperty, descriptionPopUp);
                descriptions.Visibility = Visibility.Visible;

            });

            this.BeginAnimation(Control.MarginProperty, posterTranslation);
            scaleTranform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTranform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            this.Visibility = Visibility.Visible;

        }


        private void AnimateOut(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            var duration = TimeSpan.FromMilliseconds(1000);
            var finish = new Thickness(mainWindowAnchor.X, mainWindowAnchor.Y, 0, 0);

            DoubleAnimation descriptionPopOut = new DoubleAnimation(300, duration);
            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 0.66, duration);
            ThicknessAnimation posterTranslation = new ThicknessAnimation(finish, duration);

            descriptionPopOut.Completed += new EventHandler((sender2, e2) =>
            {
                descriptions.Visibility = Visibility.Visible;
                scaleTranform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                scaleTranform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                this.BeginAnimation(Control.MarginProperty, posterTranslation);
            });

            posterTranslation.Completed += new EventHandler((sender2, e2) =>
            {
                this.Visibility = Visibility.Hidden;
            });

            this.BeginAnimation(Control.WidthProperty, descriptionPopOut);
        }
    }
}
