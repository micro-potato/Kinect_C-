using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class TwoPalmsSegment : IRelativeGestureSegment
    {
        private int _validTimes = 5;
        private int _suceedTimes = 0;
        public GesturePartResult CheckGesture(Body bodyData)
        {
            var jointsData = bodyData.Joints;
            if (bodyData.HandLeftState == HandState.Open && bodyData.HandRightState == HandState.Open)//Both palm open
            {
                if (jointsData[JointType.HandRight].Position.Y > jointsData[JointType.SpineMid].Position.Y && jointsData[JointType.HandLeft].Position.Y > jointsData[JointType.SpineMid].Position.Y)//双手伸出，手在上半身
                {
                    _suceedTimes++;
                    if (_suceedTimes >= _validTimes)
                    {
                        _suceedTimes = 0;
                        return GesturePartResult.Suceed;
                    }
                    else return GesturePartResult.Pass;
                }
                else
                {
                    return GesturePartResult.Pausing;
                }
            }
            else
            {
                return GesturePartResult.Pausing;
            }
        }
    }
}
