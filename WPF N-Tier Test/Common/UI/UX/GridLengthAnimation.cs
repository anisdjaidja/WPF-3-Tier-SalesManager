using System.Windows;
using System.Windows.Media.Animation;

namespace WPF_N_Tier_Test.Common.UI.UX
{
    public class GridLengthAnimation : AnimationTimeline
    {
        
        /// <summary>
        /// Returns the type of object to animate
        /// </summary>
        public override Type TargetPropertyType => typeof(GridLength);

        /// <summary>
        /// Creates an instance of the animation object
        /// </summary>
        /// <returns>Returns the instance of the GridLengthAnimation</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        /// <summary>
        /// Dependency property for the From property
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(GridLength),
          typeof(GridLengthAnimation));

        /// <summary>
        /// CLR Wrapper for the From depenendency property
        /// </summary>
        public GridLength From
        {
            get
            {
                return (GridLength)GetValue(FromProperty);
            }
            set
            {
                SetValue(FromProperty, value);
            }
        }
        /// <summary>
        /// Dependency property for the To property
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(GridLength),
          typeof(GridLengthAnimation));

        /// <summary>
        /// CLR Wrapper for the To property
        /// </summary>
        public GridLength To
        {
            get
            {
                return (GridLength)GetValue(ToProperty);
            }
            set
            {
                SetValue(ToProperty, value);
            }
        }

        /// <summary>
        /// Animates the grid let set
        /// </summary>
        /// <param name="defaultOriginValue">The original value to animate</param>
        /// <param name="defaultDestinationValue">The final value</param>
        /// <param name="animationClock">The animation clock (timer)</param>
        /// <returns>Returns the new grid length to set</returns>
        public override object GetCurrentValue(object defaultOriginValue,
              object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromVal = ((GridLength)GetValue(FromProperty)).Value;

            double toVal = ((GridLength)GetValue(ToProperty)).Value;

            //check that from was set from the caller
            //if (fromVal == 1)
            //  //set the from as the actual value
            //  fromVal = ((GridLength)defaultDestinationValue).Value;

            double progress = animationClock.CurrentProgress.Value;

            IEasingFunction easingFunction = EasingFunction;
            if (easingFunction != null)
            {
                progress = easingFunction.Ease(progress);
            }


            if (fromVal > toVal)
            {
                return new GridLength((1 - progress) * (fromVal - toVal) + toVal, GridUnitType.Pixel);
            }

            return new GridLength(progress * (toVal - fromVal) + fromVal, GridUnitType.Pixel);
        }

        /// <summary>
        /// The <see cref="EasingFunction" /> dependency property's name.
        /// </summary>
        public const string EasingFunctionPropertyName = "EasingFunction";

        /// <summary>
        /// Gets or sets the value of the <see cref="EasingFunction" />
        /// property. This is a dependency property.
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get
            {
                return (IEasingFunction)GetValue(EasingFunctionProperty);
            }
            set
            {
                SetValue(EasingFunctionProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="EasingFunction" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
          EasingFunctionPropertyName,
          typeof(IEasingFunction),
          typeof(GridLengthAnimation),
          new UIPropertyMetadata(null));
    }
}
