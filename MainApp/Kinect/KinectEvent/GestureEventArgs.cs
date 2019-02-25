using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Kinect.KinectEvent
{
    public class GestureEventArgs:EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GestureEventArgs"/> class.
        /// </summary>
        /// <param name="type">The gesture type.</param>
        /// <param name="trackingID">The tracking ID.</param>
        /// <param name="userID">The user ID.</param>
        public GestureEventArgs(GestureType type, ulong trackingID)
        {
            this.TrackingID = trackingID;
            this.GestureType = type;
        }

        /// <summary>
        /// Gets or sets the type of the gesture.
        /// </summary>
        /// <value>
        /// The type of the gesture.
        /// </value>
        public GestureType GestureType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tracking ID.
        /// </summary>
        /// <value>
        /// The tracking ID.
        /// </value>
        public ulong TrackingID
        {
            get;
            set;
        }
    }
}
