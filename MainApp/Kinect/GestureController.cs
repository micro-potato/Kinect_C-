using MainApp.Kinect.KinectEvent;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainApp.Kinect.Gestures;

namespace MainApp.Kinect
{
    public class GestureController
    {
        public event EventHandler<GestureEventArgs> GestureRecognised;
        private List<Gesture> _gesturesToCheck = new List<Gesture>();
        public GestureController()
        {
            
        }

        public string SupportedGestures
        {
            set
            {
                var gestureList = value.Split('+').ToList();
                foreach(string gesture in gestureList)
                {
                    try
                    {
                        GestureType gestureType = (GestureType)Enum.Parse(typeof(GestureType), gesture);
                        AddSupportedGesture(gestureType);
                    }
                    catch(Exception e)
                    {
                        continue;
                        throw new Exception(e.Message);
                    }
                    
                }
            }
        }

        private void AddSupportedGesture(GestureType gestureType)
        {
            switch (gestureType)
            {
                case GestureType.FistHold:
                    AddGesture(GestureType.FistHold, new IRelativeGestureSegment[] { new FistSegment() });
                    break;
                case GestureType.PalmHold:
                    AddGesture(GestureType.PalmHold, new IRelativeGestureSegment[] { new PalmSegment() });
                    break;
                case GestureType.SwipeLeft:
                    AddGesture(GestureType.SwipeLeft, new IRelativeGestureSegment[] { new BaseStandSegment(), new SwipeLeftSegment1(), new SwipeLeftSegment2() });
                    break;
                case GestureType.SwipeRight:
                    AddGesture(GestureType.SwipeRight, new IRelativeGestureSegment[] { new BaseStandSegment(), new SwipeRightSegment1(), new SwipeRightSegment2() });
                    break;
                case GestureType.TouchLeft:
                    AddGesture(GestureType.TouchLeft, new IRelativeGestureSegment[] { new BaseStandSegment(), new TouchLeftSegment() });
                    break;
                case GestureType.TouchMiddle:
                    AddGesture(GestureType.TouchMiddle, new IRelativeGestureSegment[] { new BaseStandSegment(), new TouchMiddleSegment() });
                    break;
                case GestureType.TouchRight:
                    AddGesture(GestureType.TouchRight, new IRelativeGestureSegment[] { new BaseStandSegment(), new TouchRightSegment() });
                    break;
                case GestureType.TwoPalmsOPen:
                    AddGesture(GestureType.TwoPalmsOPen, new IRelativeGestureSegment[] { new BaseStandSegment(), new TwoPalmsSegment() });
                    break;
            }
        }

        private void InitGestures()
        {
            AddGesture(GestureType.FistHold, new IRelativeGestureSegment[] { new FistSegment() });
            AddGesture(GestureType.PalmHold, new IRelativeGestureSegment[] { new PalmSegment() });
            AddGesture(GestureType.SwipeLeft, new IRelativeGestureSegment[] { new SwipeLeftSegment1(), new SwipeLeftSegment2() });
            AddGesture(GestureType.SwipeRight, new IRelativeGestureSegment[] { new SwipeRightSegment1(), new SwipeRightSegment2() });
        }

        /// <summary>
        /// Adds the gesture.
        /// </summary>
        /// <param name="type">The gesture type.</param>
        /// <param name="gestureDefinition">The gesture definition.</param>
        public void AddGesture(GestureType type, IRelativeGestureSegment[] gestureDefinition)
        {
            Gesture gesture = new Gesture(type, gestureDefinition);
            gesture.GestureRecognised += new EventHandler<GestureEventArgs>(this.Gesture_GestureRecognised);
            _gesturesToCheck.Add(gesture);
        }

        internal void UpdateAllGestures(Body bodyData)
        {
            foreach (Gesture gesture in this._gesturesToCheck)
            {
                gesture.UpdateGesture(bodyData);
            }
        }

        /// <summary>
        /// Handles the GestureRecognised event of the g control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KinectSkeltonTracker.GestureEventArgs"/> instance containing the event data.</param>
        private void Gesture_GestureRecognised(object sender, GestureEventArgs e)
        {
            if (this.GestureRecognised != null)
            {
                this.GestureRecognised(this, e);
            }

            foreach (Gesture g in _gesturesToCheck)
            {
                g.Reset();
            }
        }
    }
}
