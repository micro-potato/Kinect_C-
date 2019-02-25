using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class BaseStandSegment:IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Body bodyData)
        {
            int offsetLength = 80;
            var jointsData = bodyData.Joints;
            if (jointsData[JointType.HandRight].Position.Y < jointsData[JointType.SpineBase].Position.Y && jointsData[JointType.HandLeft].Position.Y < jointsData[JointType.SpineBase].Position.Y)//双手下垂
            {
                if ((Math.Abs(jointsData[JointType.HandRight].Position.X - jointsData[JointType.ElbowRight].Position.X)) * 1000 < offsetLength && (Math.Abs(jointsData[JointType.HandLeft].Position.X - jointsData[JointType.ElbowLeft].Position.X)) * 1000 < offsetLength)//手与手肘间X方向距小
                {
                    return GesturePartResult.Suceed;
                }
                else
                {
                    return GesturePartResult.Fail;
                }
            }
            else
            {
                return GesturePartResult.Fail;
            }
        }
    }
}
