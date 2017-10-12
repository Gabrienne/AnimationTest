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
            var upperLeft = new Point(toPoint.X - 250, toPoint.Y - 150); //toPoint is window center, we need the upper left corner of the final window
            var duration = TimeSpan.FromMilliseconds(500);
            var startup = new Thickness(mainWindowAnchor.X, mainWindowAnchor.Y, 0, 0);
            var finish = new Thickness(upperLeft.X, upperLeft.Y, 0, 0);

            DoubleAnimation widthAnimation = new DoubleAnimation(160, 500, duration);
            DoubleAnimation heightAnimation = new DoubleAnimation(260, 300, duration);
            ThicknessAnimation posterTranslation = new ThicknessAnimation(startup, finish, duration);
            ThicknessAnimation descriptionPopUp = new ThicknessAnimation(new Thickness(200, 0, 0, 0), duration);

            descriptionPopUp.Completed += new EventHandler((sender2, e2) =>
            {
                //ensure initial keyboard focus
                btn_Close.Focus();
            });

            posterTranslation.Completed += new EventHandler((sender2, e2) =>
            {
                descriptions.BeginAnimation(Control.MarginProperty, descriptionPopUp);
            });

            this.BeginAnimation(Control.MarginProperty, posterTranslation);
            this.BeginAnimation(Control.WidthProperty, widthAnimation);
            this.BeginAnimation(Control.HeightProperty, heightAnimation);
            this.Visibility = Visibility.Visible;
        }


        private void AnimateOut(object sender, RoutedEventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds(500);
            var finish = new Thickness(mainWindowAnchor.X, mainWindowAnchor.Y, 0, 0);

            DoubleAnimation widthAnimation = new DoubleAnimation(160, duration);
            DoubleAnimation heightAnimation = new DoubleAnimation(260, duration);
            ThicknessAnimation posterTranslation = new ThicknessAnimation(finish, duration);
            ThicknessAnimation descriptionPopOut = new ThicknessAnimation(new Thickness(0, 0, 0, 0), duration);

            descriptionPopOut.Completed += new EventHandler((sender2, e2) =>
            {
                this.BeginAnimation(Control.MarginProperty, posterTranslation);
                this.BeginAnimation(Control.WidthProperty, widthAnimation);
                this.BeginAnimation(Control.HeightProperty, heightAnimation);
            });

            posterTranslation.Completed += new EventHandler((sender2, e2) =>
            {
                this.Visibility = Visibility.Hidden;
            });

            descriptions.BeginAnimation(Control.MarginProperty, descriptionPopOut);
        }
    }
}
