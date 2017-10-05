using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AnimationTest
{
    public class MovieItem : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string _description = "This is a description for the movie bla bla njnfdsfm jfdf sdkf sfdsf sffffffkkkk kkk gndfffff dffflll nvmd";
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        private Uri _posterUri;
        public Uri PosterUri
        {
            get
            {
                if (_posterUri == null)
                {
                    _posterUri = new Uri("pack://application:,,,/Resources/" + Title + ".jpg");
                }
                return _posterUri;
            }
        }
    }
}
