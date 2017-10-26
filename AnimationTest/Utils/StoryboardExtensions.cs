using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace AnimationTest.Utils
{
    static class StoryboardExtensions
    {
        public static void Reverse(this Storyboard anim, TimeSpan duration)
        {
            anim.AutoReverse = true;
            anim.Begin();
            anim.Pause();
            anim.Seek(duration);
            anim.Resume();
        }
    }
}
