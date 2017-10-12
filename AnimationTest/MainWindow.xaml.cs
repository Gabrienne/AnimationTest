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
            var movie = movieGrid.SelectedItem as MovieItem;
            var movieListBoxItem = movieGrid.ItemContainerGenerator.ContainerFromItem(movie) as ListBoxItem;
            Point fromPoint = movieListBoxItem.TransformToAncestor(this).Transform(new Point(0, 0));
            if (movie != null)
            {
                modal.AnimateIn(movie, fromPoint, new Point(this.ActualWidth/2, this.ActualHeight/2));
            }
        }

        private void movieGridVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false))
            {
                //does not work..
                (movieGrid.SelectedItem as ListBoxItem)?.Focus();
            }
        }
    }
}
