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

        private void enterMovie(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Enter:
                    ShowModal();
                    break;
            }
        }

        private void ShowModal()
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
                    modal.AnimateIn(movie, fromPoint, new Point(this.ActualWidth / 2, this.ActualHeight / 2));
                }
            }
            catch { }
        }

        private void movieGridVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false))
            {
                (movieGrid.ItemContainerGenerator.ContainerFromItem(modal.DataContext) as ListBoxItem).Visibility = Visibility.Visible;
                setFocus(sender, new RoutedEventArgs());
            }
        }

        private ListBoxItem getSelectedMovieItemOrDefault()
        {
            var index = movieGrid.SelectedIndex >= 0 ? movieGrid.SelectedIndex : 0;
            return movieGrid.ItemContainerGenerator.ContainerFromIndex(index) as ListBoxItem;
        }
    }
}
