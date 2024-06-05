using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;
using WPF_N_Tier_Test.Common.UI.UX;

namespace WPF_N_Tier_Test.Modules.Helpers
{
    public static class AnimationHelper
    {
        public static void ExpandOperationsRow(RowDefinition row, double height)
        {
            Storyboard storyboard = new();
            GridLengthAnimation gridLengthAnimation = new GridLengthAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                From = row.Height,
                To = new GridLength(height, GridUnitType.Pixel),
            };
            storyboard.Children.Add(gridLengthAnimation);
            Storyboard.SetTarget(gridLengthAnimation, row);
            Storyboard.SetTargetProperty(gridLengthAnimation, new PropertyPath(RowDefinition.HeightProperty));
            storyboard.Begin();
        }
        public static void CollapsOperationsRow(RowDefinition row, Border childBorder, TranslateTransform translateTransform, bool success = false)
        {
            Duration duration = new(TimeSpan.FromMilliseconds(300));
            CircleEase ease = new() { EasingMode = EasingMode.EaseOut };


            Storyboard Firststoryboard = new();
            Storyboard storyboard = new();


            DoubleAnimation doubleAnimation = new();
            doubleAnimation.Duration = duration;
            doubleAnimation.EasingFunction = ease;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            Firststoryboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation, childBorder);
            if (success)
            {
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                doubleAnimation.From = 0;
                doubleAnimation.To = 50;
            }
            else
            {
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                doubleAnimation.From = 0;
                doubleAnimation.To = -50;
            }


            DoubleAnimation fadeAnimation = new();
            fadeAnimation.Duration = duration;
            fadeAnimation.EasingFunction = ease;
            Firststoryboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(fadeAnimation, childBorder);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Border.Opacity)"));
            fadeAnimation.From = 1;
            fadeAnimation.To = 0.1;
            fadeAnimation.FillBehavior = FillBehavior.Stop;



            GridLengthAnimation gridLengthAnimation = new();
            gridLengthAnimation.Duration = duration;
            gridLengthAnimation.EasingFunction = ease;
            storyboard.Children.Add(gridLengthAnimation);
            Storyboard.SetTarget(gridLengthAnimation, row);
            Storyboard.SetTargetProperty(gridLengthAnimation, new PropertyPath("(RowDefinition.Height)"));
            gridLengthAnimation.From = row.Height;
            gridLengthAnimation.To = new GridLength(0, GridUnitType.Pixel);

            storyboard.Completed += (s, o) => { translateTransform.X = 0; translateTransform.Y = 0; childBorder.Opacity = 1; childBorder.Visibility = Visibility.Visible; };
            Firststoryboard.Completed += (s, o) => {
                //childBorder.Child = null;
                childBorder.Visibility = Visibility.Hidden;
                TranslateTransform myTranslate = new TranslateTransform { X = 0, Y = 0 };
                childBorder.RenderTransform = myTranslate;
                storyboard.Begin();
            };
            Firststoryboard.Begin();

        }
        
        public enum SlidDirection
        {
            Up,
            Left, 
            Right,
            Down,
        }
        public static void SlideFadePopup(Popup pop, bool IsOpen, SlidDirection slidDirection)
        {
            if (pop.IsOpen == IsOpen) { return; }
            if (!IsOpen) { pop.IsOpen = false; return; }
            Duration duration = new(TimeSpan.FromMilliseconds(200));
            CircleEase ease = new() { EasingMode = EasingMode.EaseOut };

            Storyboard Firststoryboard = new();

            DoubleAnimation doubleAnimation = new()
            {
                Duration = duration,
                EasingFunction = ease,
                FillBehavior = FillBehavior.Stop,
                From = 0
            };

            DoubleAnimation fadeAnimation = new()
            {
                Duration = duration,
                EasingFunction = ease,
                FillBehavior = FillBehavior.Stop
            };

            if (IsOpen)
            {
                fadeAnimation.From = 0.1;
                fadeAnimation.To = 1;
            }
            else {
                fadeAnimation.From = 1;
                fadeAnimation.To = 0.1;
            }

            Firststoryboard.Children.Add(doubleAnimation);
            Firststoryboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(doubleAnimation, pop.Child as Border);
            switch (slidDirection)
            {
                case SlidDirection.Up:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    if (IsOpen) { doubleAnimation.From = 10; doubleAnimation.To = 0;}
                    else { doubleAnimation.From = 0; doubleAnimation.To = 10;}

                    break;
                case SlidDirection.Down:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    if (IsOpen) { doubleAnimation.From = -10; doubleAnimation.To = 0; }
                    else { doubleAnimation.From = 0; doubleAnimation.To = -10; }
                    break;

                case SlidDirection.Left:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    if (IsOpen) { doubleAnimation.From = 10; doubleAnimation.To = 0; }
                    else { doubleAnimation.From = 0; doubleAnimation.To = 10; }
                    break;
                case SlidDirection.Right:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    if (IsOpen) { doubleAnimation.From = -10; doubleAnimation.To = 0; }
                    else { doubleAnimation.From = 0; doubleAnimation.To = -10; }
                    break;
            }
            Storyboard.SetTarget(fadeAnimation, pop.Child as Border);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Border.Opacity)"));


            if (IsOpen) { 
                pop.IsOpen = true;
                Firststoryboard.Begin();
            }
            else
            {
                Firststoryboard.Completed += (s, o) => {
                    pop.IsOpen = false;
                };

                Firststoryboard.Begin();
            }
            
        }
        public static void SlideFadeBorder(Border border, SlidDirection slidDirection, int transitionDistance = 5, double timeSpan = 250, EasingMode easing = EasingMode.EaseIn)
        {
            Duration duration = new(TimeSpan.FromMilliseconds(timeSpan));
            CircleEase ease = new() { EasingMode = easing };

            Storyboard Firststoryboard = new();

            DoubleAnimation doubleAnimation = new()
            {
                Duration = duration,
           
                FillBehavior = FillBehavior.Stop,
                From = 0
            };

            DoubleAnimation fadeAnimation = new()
            {
                Duration = duration,
                EasingFunction = ease,
                FillBehavior = FillBehavior.Stop 
            };

            fadeAnimation.From = 0.5;
            fadeAnimation.To = 1;


            switch (slidDirection)
            {
                case SlidDirection.Up:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    { doubleAnimation.From = transitionDistance; doubleAnimation.To = 0; }


                    break;
                case SlidDirection.Down:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    { doubleAnimation.From = -transitionDistance; doubleAnimation.To = 0; }

                    break;

                case SlidDirection.Left:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    { doubleAnimation.From = transitionDistance; doubleAnimation.To = 0; }
                    break;
                case SlidDirection.Right:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    { doubleAnimation.From = -transitionDistance; doubleAnimation.To = 0; }
                    break;
            }
            Firststoryboard.Children.Add(doubleAnimation);
            Firststoryboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(fadeAnimation, border);
            Storyboard.SetTarget(doubleAnimation, border);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Border.Opacity)"));
            Firststoryboard.Begin();


        }
        public static void SlideFadeOffBorder(Border border, SlidDirection slidDirection, int transitionDistance = 5, double timeSpan = 250, EasingMode easing = EasingMode.EaseIn)
        {
            Duration duration = new(TimeSpan.FromMilliseconds(timeSpan));
            CircleEase ease = new() { EasingMode = easing };

            Storyboard Firststoryboard = new();

            DoubleAnimation doubleAnimation = new()
            {
                Duration = duration,
           
                FillBehavior = FillBehavior.Stop,
                From = 0
            };

            DoubleAnimation fadeAnimation = new()
            {
                Duration = duration,
                EasingFunction = ease,
                FillBehavior = FillBehavior.Stop 
            };

            fadeAnimation.From = 0.8;
            fadeAnimation.To = 0.3;


            switch (slidDirection)
            {
                case SlidDirection.Up:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    { doubleAnimation.From = transitionDistance; doubleAnimation.To = 0; }


                    break;
                case SlidDirection.Down:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Y"));
                    { doubleAnimation.From = -transitionDistance; doubleAnimation.To = 0; }

                    break;

                case SlidDirection.Left:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    { doubleAnimation.From = transitionDistance; doubleAnimation.To = 0; }
                    break;
                case SlidDirection.Right:
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.X"));
                    { doubleAnimation.From = -transitionDistance; doubleAnimation.To = 0; }
                    break;
            }
            Firststoryboard.Children.Add(doubleAnimation);
            Firststoryboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(fadeAnimation, border);
            Storyboard.SetTarget(doubleAnimation, border);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("(Border.Opacity)"));
            Firststoryboard.Completed += (s, o) => border.Visibility = Visibility.Hidden;
            Firststoryboard.Begin();


        }
        /// <summary>
        /// Turning blur on
        /// </summary>
        /// <param name="element">bluring element</param>
        /// <param name="blurRadius">blur radius</param>
        /// <param name="duration">blur animation duration</param>
        /// <param name="beginTime">blur animation delay</param>
        public static void BlurApply(this UIElement element,
            double blurRadius, TimeSpan duration, TimeSpan beginTime)
        {
            BlurEffect blur = new BlurEffect() { Radius = 0 };
            DoubleAnimation blurEnable = new DoubleAnimation(0, blurRadius, duration)
            { BeginTime = beginTime };
            element.Effect = blur;
            blur.BeginAnimation(BlurEffect.RadiusProperty, blurEnable);
        }
        /// <summary>
        /// Turning blur off
        /// </summary>
        /// <param name="element">bluring element</param>
        /// <param name="duration">blur animation duration</param>
        /// <param name="beginTime">blur animation delay</param>
        public static void BlurDisable(this UIElement element, TimeSpan duration, TimeSpan beginTime)
        {
            BlurEffect blur = element.Effect as BlurEffect;
            if (blur == null || blur.Radius == 0)
            {
                return;
            }
            DoubleAnimation blurDisable = new DoubleAnimation(blur.Radius, 0, duration) { BeginTime = beginTime };
            blur.BeginAnimation(BlurEffect.RadiusProperty, blurDisable);
        }
    }
}
