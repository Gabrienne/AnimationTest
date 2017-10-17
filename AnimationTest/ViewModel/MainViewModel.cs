using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AnimationTest
{
    public class MainViewModel : ViewModelBase
    {
        private List<MovieItem> _movies = new List<MovieItem>();
        public List<MovieItem> Movies
        {
            get
            {
                return _movies;
            }
        }

        private MovieItem _selectedMovie;
        public MovieItem SelectedMovie
        {
            get
            {
                if (_selectedMovie == null)
                {
                    _selectedMovie = Movies[0];
                }
                return _selectedMovie;
            }
            set
            {
                _selectedMovie = value;
                NotifyPropertyChanged(nameof(SelectedMovie));
            }
        }

        public Uri Play
        {
            get
            {
                return new Uri("pack://application:,,,/Resources/movie_play.png");
            }
        }

        public MainViewModel()
        {
            Movies.Add(new MovieItem("Titanic"));
            Movies.Add(new MovieItem("Cinderella"));
            Movies.Add(new MovieItem("Wonderwoman"));
            Movies.Add(new MovieItem("HarryPotter"));
            Movies.Add(new MovieItem("Movie5"));
            Movies.Add(new MovieItem("Movie6"));
            Movies.Add(new MovieItem("Movie7"));
            Movies.Add(new MovieItem("Movie8"));
            Movies.Add(new MovieItem("Movie9"));
            Movies.Add(new MovieItem("Movie10"));
        }
    }
}
