using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class TouchLeftSegment : IRelativeGestureSegment
    {
        private int _validTimes = 5;
        private int _suceedTimes = 0;
        public GesturePartResult CheckGesture(Body bodyData)
        {
            int validLength = 50;
            var jointsData = bodyData.Joints;
            if ((jointsData[JointType.ShoulderRight].Position.Z - jointsData[JointType.HandRight].Position.Z) * 1000 > 350)//Z方向伸出
            {
                if (bodyData.HandRightState == HandState.Open)
                {
                    if ((jointsData[JointType.ShoulderLeft].Position.X-jointsData[JointType.HandRight].Position.X)*1000 >validLength*-1)
                    {
                        _suceedTimes++;
                        if (_suceedTimes >= _validTimes)
                        {
                            _suceedTimes = 0;
                            return GesturePartResult.Suceed;
                        }
                        else return GesturePartResult.Pass;
                    }
                    else return PauseOnce();
                }
                else return PauseOnce();
            }
            else return PauseOnce();
        }

        private GesturePartResult PauseOnce()
        {
            _suceedTimes = 0;
            return GesturePartResult.Pausing;
        }
    }
}
