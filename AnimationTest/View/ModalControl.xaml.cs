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
using AnimationTest.Utils;

namespace AnimationTest
{
    /// <summary>
    /// Interaction logic for ModalControl.xaml
    /// </summary>
    public partial class ModalControl : UserControl
    {
        public ModalControl()
        {
            InitializeComponent();
        }

        private void AnimateOut(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            var duration = TimeSpan.FromMilliseconds(1000);

            ExpandStoryBoard.Completed += new EventHandler((sender2, e2) =>
            {
                PopupStoryboard.Completed += new EventHandler((sender3, e3) =>
                {
                    this.Visibility = Visibility.Hidden; //to hide the poster and trigger focus on main menu
                });

                //modal control expansion
                PopupStoryboard.Reverse(defaultDuration);
            });

            ExpandStoryBoard.Reverse(defaultDuration);
        }

        internal void AnimateIn(ListBoxItem movieListBoxItem, MainWindow mainWindow)
        {
            this.DataContext = movieListBoxItem.Content as MovieItem;

            Storyboard popUp = createPopupStoryboard(movieListBoxItem, mainWindow);
            popUp.Completed += new EventHandler((sender2, e2) =>
            {
                //modal control expansion
                var expandStory = createExpandStoryboard();
                expandStory.Completed += new EventHandler((sender3, e3) =>
                {
                    //ensure initial keyboard focus
                    btn_Close.Focus();                   
                });
                expandStory.Begin();
            });

            popUp.Begin();
            this.Visibility = Visibility.Visible;
        }

        private Storyboard createPopupStoryboard(ListBoxItem movieListBoxItem, MainWindow mainWindow)
        {
            Point fromPoint = movieListBoxItem.TransformToAncestor(mainWindow).Transform(new Point(0, 0));
            Point toPoint = new Point((mainWindow.ActualWidth - posterWidth) / 2, (mainWindow.ActualHeight - poster.Height) / 2);
            double scaleYFactor = movieListBoxItem.ActualHeight / posterHeight;
            double scaleXFactor = movieListBoxItem.ActualWidth / posterWidth;


            var popupStory = new Storyboard();
            Storyboard.SetTarget(popupStory, this);
            popupStory.AddDoubleAnimation(fromPoint.X, toPoint.X, defaultDuration, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.X)"));
            popupStory.AddDoubleAnimation(fromPoint.Y, toPoint.Y, defaultDuration, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.Y)"));
            popupStory.AddDoubleAnimation(scaleXFactor, 1, defaultDuration, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleX)"));
            popupStory.AddDoubleAnimation(scaleYFactor, 1, defaultDuration, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleY)"));

            PopupStoryboard = popupStory.Clone();
            return popupStory;
        }

        private Storyboard createExpandStoryboard()
        {
            var expandStory = new Storyboard();
            Storyboard.SetTarget(expandStory, this);
            expandStory.AddDoubleAnimation(posterWidth, modalWidth, defaultDuration, new PropertyPath(Control.WidthProperty));
            expandStory.AddDoubleAnimation(translateTransform.X, translateTransform.X - posterWidth / 2, defaultDuration, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.X)"));

            ExpandStoryBoard = expandStory.Clone();
            return expandStory;
        }

        #region Members
        Storyboard PopupStoryboard = null;
        Storyboard ExpandStoryBoard = null;
        TimeSpan defaultDuration = TimeSpan.FromMilliseconds(1000);  //animations duration
        int posterWidth = 300;
        int posterHeight = 450;
        int modalWidth = 600;
        #endregion
    }
}
