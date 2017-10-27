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
        public static Uri NOT_FOUND = new Uri("pack://application:,,,/Resources/ImgNotFound.jpg");

        #region Constructor

        public MovieItem(string title)
        {
            Title = title;
        }
        #endregion


        #region Properties
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
                        _posterUri = NOT_FOUND;
                    }
                }
                return _posterUri;
            }
        }

        private Uri _backgroundUri;
        public Uri BackgroundUri
        {
            get
            {
                if (_backgroundUri == null)
                {
                    _backgroundUri = new Uri("pack://application:,,,/Resources/background.jpg");
                    try
                    {
                        var stream = App.GetResourceStream(_backgroundUri);
                    }
                    catch
                    {
                        _backgroundUri = NOT_FOUND;
                    }
                }
                return _backgroundUri;
            }
        }

        private Uri _videoFile;
        public Uri VideoFile
        {
            get
            {
                if (_videoFile == null)
                {
                    _videoFile = new Uri(@"C:\Users\gabriela\Downloads\H.264\Rogue One - A Star Wars Story - Trailer.mp4");
                }
                return _videoFile;
            }
        }


        #endregion

    }
}
