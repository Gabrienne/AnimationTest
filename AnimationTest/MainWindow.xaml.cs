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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.Loaded += setFocus;
        }

        private void setFocus(object sender, RoutedEventArgs e)
        {
            getSelectedMovieItemOrDefault().Focus();
        }

        private ListBoxItem getSelectedMovieItemOrDefault()
        {
            var index = movieGrid.SelectedIndex >= 0 ? movieGrid.SelectedIndex : 0;
            return getMovieFromIndex(index);
        }

        private ListBoxItem getMovieFromIndex(int index)
        {
            return movieGrid.ItemContainerGenerator.ContainerFromIndex(index) as ListBoxItem;
        }

        

        private void movieSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            handleBackground();

            //pre selection animation
            var preSelectionAnimation = new DoubleAnimation(10, TimeSpan.FromMilliseconds(400));
            preSelectionAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

            var deselectionAnimation = new DoubleAnimation(30, TimeSpan.FromMilliseconds(400));
            deselectionAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

            var selectionAnimation = new DoubleAnimation(0, TimeSpan.FromMilliseconds(400));
            deselectionAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

            var crt_index = movieGrid.SelectedIndex;
            try
            {
                ApplyRenderTransformYToPrevious(crt_index - 1, deselectionAnimation);
                ApplyRenderTransformYToPrevious(crt_index, preSelectionAnimation);
                ApplyRenderTransformYToNext(crt_index, preSelectionAnimation);
                ApplyRenderTransformYToNext(crt_index + 1, deselectionAnimation);
                ApplyRenderTransformYToNext(crt_index - 1, selectionAnimation);
            }
            catch { }
        }

        private void ApplyRenderTransformYToPrevious(int index, DoubleAnimation animation)
        {
            if (index > 0)
            {
                ApplyRenderTransformY(index - 1, animation);
            }
        }

        private void ApplyRenderTransformYToNext(int index, DoubleAnimation animation)
        {
            if (index < movieGrid.Items.Count - 1)
            {
                ApplyRenderTransformY(index + 1, animation);
            }
        }

        private void ApplyRenderTransformY(int index, DoubleAnimation animation)
        {
            var item = getMovieFromIndex(index);
            if (item.RenderTransform.IsFrozen)
            {
                item.RenderTransform = item.RenderTransform.CloneCurrentValue();
            }
            item.RenderTransform.BeginAnimation(TranslateTransform.YProperty, animation);
        }

        private void handleBackground()
        {
            var fadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
            fadeOutAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            fadeOutAnimation.Completed += new EventHandler((sender2, e2) =>
            {
                img_background.ImageSource = new BitmapImage((movieGrid.SelectedItem as MovieItem).BackgroundUri);
                var fadeInAnimation = new DoubleAnimation(1, TimeSpan.FromMilliseconds(500));
                fadeInAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
                img_background.BeginAnimation(Brush.OpacityProperty, fadeInAnimation);
            });
            img_background.BeginAnimation(Brush.OpacityProperty, fadeOutAnimation);
        }

        private void click_play(object sender, RoutedEventArgs e)
        {
            DoEnterAction();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    DoEnterAction();
                    break;
                case Key.Escape:
                    //TODO warning?
                    this.Close();
                    break;
            }
        }

        private void DoEnterAction()
        {
            try
            {
                Keyboard.ClearFocus();

                var movieListBoxItem = getSelectedMovieItemOrDefault();
                var movie = movieListBoxItem.Content as MovieItem;
                movieListBoxItem.Visibility = Visibility.Hidden;
                Point fromPoint = movieListBoxItem.TransformToAncestor(this).Transform(new Point(0, 0));
                if (movie != null)
                {
                    modal.AnimateIn(movieListBoxItem, this);
                }
            }
            catch { }
        }

        private void click_trailer(object sender, RoutedEventArgs e)
        {
            //implement trailer action
        }

        private void movieGridVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false))
            {
                (movieGrid.ItemContainerGenerator.ContainerFromItem(modal.DataContext) as ListBoxItem).Visibility = Visibility.Visible;
                setFocus(sender, new RoutedEventArgs());
            }
        }
    }
}
