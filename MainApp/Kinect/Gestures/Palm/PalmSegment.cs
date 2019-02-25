using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MainApp.Kinect.Gestures
{
    public class PalmSegment : IRelativeGestureSegment
    {
        private int _validTimes = 15;
        private int _suceedTimes = 0;
        public GesturePartResult CheckGesture(Body bodyData)
        {
            var jointsData = bodyData.Joints;
            if (bodyData.HandRightState == HandState.Open)
            {
                if ((jointsData[JointType.ShoulderRight].Position.Z - jointsData[JointType.HandRight].Position.Z) * 1000 > 350)//Z方向伸出
                {
                    if (Math.Abs(jointsData[JointType.HandRight].Position.X - jointsData[JointType.ShoulderRight].Position.X) * 1000 < 160)//手与右肩之间平行
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
                        return GesturePartResult.Fail;
                    }
                }
                else
                {
                    return GesturePartResult.Fail;
                }
            }
            else//Hand open
            {
                return GesturePartResult.Fail;
            }
        }
    }
}
