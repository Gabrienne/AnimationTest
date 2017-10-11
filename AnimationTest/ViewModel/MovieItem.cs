using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AnimationTest
{
    public class MovieItem : ViewModelBase
    {
        public static Uri POSTER_NOT_FOUND = new Uri("pack://application:,,,/Resources/ImgNotFound.jpg");

        public MovieItem(string title)
        {
            Title = title;
        }

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

        private string _description;
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    _description = "This is a description for the item " + Title;
                }
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
                    try
                    {
                        var stream = App.GetResourceStream(_posterUri);
                    }
                    catch
                    {
                        _posterUri = POSTER_NOT_FOUND;
                    }
                }
                return _posterUri;
            }
        }
    }
}
