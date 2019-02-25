using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class SwipeLeftSegment1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Body bodyData)
        {
            var jointsData = bodyData.Joints;
            if (jointsData[JointType.HandRight].Position.X>= jointsData[JointType.ElbowRight].Position.X)
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
