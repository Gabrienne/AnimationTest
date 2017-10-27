using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AnimationTest.Utils
{
    static class StoryboardExtensions
    {
        public static void Reverse(this Storyboard sb, TimeSpan duration)
        {
            sb.AutoReverse = true;
            sb.Begin();
            sb.Pause();
            sb.Seek(duration);
            sb.Resume();
        }

        public static void AddDoubleAnimation(this Storyboard sb, double from, double to, TimeSpan duration, PropertyPath targetProperty, EasingMode easeMode = EasingMode.EaseOut)
        {
            var animation = new DoubleAnimation(from, to, duration) { EasingFunction = new CubicEase() { EasingMode = easeMode } };
            Storyboard.SetTargetProperty(animation, targetProperty);
            sb.Children.Add(animation);
        }
    }
}
