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
                ReverseStoryBoard(PopupStoryboard);
            });

            ReverseStoryBoard(ExpandStoryBoard);
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
            DoubleAnimation posterXTranslation = createDoubleAnimation(fromPoint.X, toPoint.X, defaultDuration);
            DoubleAnimation posterYTranslation = createDoubleAnimation(fromPoint.Y, toPoint.Y, defaultDuration);
            double scaleYFactor = movieListBoxItem.ActualHeight / posterHeight;
            DoubleAnimation scaleYAnimation = createDoubleAnimation(scaleYFactor, 1, defaultDuration);
            double scaleXFactor = movieListBoxItem.ActualWidth / posterWidth;
            DoubleAnimation scaleXAnimation = createDoubleAnimation(scaleXFactor, 1, defaultDuration);

            var popupStory = new Storyboard();
            Storyboard.SetTarget(popupStory, this);
            popupStory.Children.Add(posterXTranslation);
            popupStory.Children.Add(posterYTranslation);
            Storyboard.SetTargetProperty(posterXTranslation, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.X)"));
            Storyboard.SetTargetProperty(posterYTranslation, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.Y)"));
            popupStory.Children.Add(scaleXAnimation);
            popupStory.Children.Add(scaleYAnimation);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleY)"));
            PopupStoryboard = popupStory.Clone();
            return popupStory;
        }

        private Storyboard createExpandStoryboard()
        {
            var expandStory = new Storyboard();
            Storyboard.SetTarget(expandStory, this);
            DoubleAnimation descriptionPopUp = createDoubleAnimation(posterWidth, modalWidth, defaultDuration);
            DoubleAnimation posterXTranslation = createDoubleAnimation(translateTransform.X, translateTransform.X - posterWidth / 2, defaultDuration);
            expandStory.Children.Add(descriptionPopUp);
            expandStory.Children.Add(posterXTranslation);
            Storyboard.SetTargetProperty(descriptionPopUp, new PropertyPath(Control.WidthProperty));
            Storyboard.SetTargetProperty(posterXTranslation, new PropertyPath("RenderTransform.Children[1].(TranslateTransform.X)"));
            ExpandStoryBoard = expandStory.Clone();
            return expandStory;
        }

        private DoubleAnimation createDoubleAnimation(double from, double to, TimeSpan duration, EasingMode easeMode = EasingMode.EaseOut)
        {
            return new DoubleAnimation(from, to, duration) { EasingFunction = new CubicEase() { EasingMode = easeMode } };
        }

        private void ReverseStoryBoard(Storyboard anim)
        {
            anim.AutoReverse = true;
            anim.Begin();
            anim.Pause();
            anim.Seek(defaultDuration);
            anim.Resume();
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
