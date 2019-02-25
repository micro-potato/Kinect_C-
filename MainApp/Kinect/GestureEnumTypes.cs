using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Kinect
{
    public enum GesturePartResult
    {
        /// <summary>
        /// Gesture part fail
        /// </summary>
        Fail,

        /// <summary>
        /// Gesture part suceed
        /// </summary>
        Suceed,

        /// <summary>
        /// Gesture part result undetermined
        /// </summary>
        Pausing,

        /// <summary>
        /// Gesture part suceed once,need more to stable
        /// </summary>
        Pass
    }

    /// <summary>
    /// The gesture type
    /// </summary>
    public enum GestureType
    {
        SwipeLeft,
        SwipeRight,
        PalmHold,
        FistHold,
        TwoPalmsOPen,
        TouchLeft,
        TouchRight,
        TouchMiddle
    }
}
