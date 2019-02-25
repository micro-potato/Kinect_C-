using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class SwipeRightSegment1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Body bodyData)
        {
            var jointsData = bodyData.Joints;
            if (jointsData[JointType.HandLeft].Position.X <= jointsData[JointType.ElbowLeft].Position.X)
            {
                return GesturePartResult.Suceed;
            }
            else
            {
                return GesturePartResult.Fail;
            }
        }
    }
}
