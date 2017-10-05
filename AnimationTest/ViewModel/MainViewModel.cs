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

        public MainViewModel()
        {
            Movies.Add(new MovieItem() { Title = "Cinderella" } );
            Movies.Add(new MovieItem() { Title = "HarryPotter" });
            Movies.Add(new MovieItem() { Title = "Titanic" });
            Movies.Add(new MovieItem() { Title = "Wonderwoman" });
        }
    }
}
