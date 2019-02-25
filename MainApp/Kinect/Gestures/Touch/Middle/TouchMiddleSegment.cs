using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Kinect.Gestures
{
    public class TouchMiddleSegment:IRelativeGestureSegment
    {
        private int _validTimes = 5;
        private int _suceedTimes = 0;
        public GesturePartResult CheckGesture(Body bodyData)
        {
            int validLength = 150;
            var jointsData = bodyData.Joints;
            if ((jointsData[JointType.ShoulderRight].Position.Z - jointsData[JointType.HandRight].Position.Z) * 1000 > 350)//Z方向伸出
            {
                if (bodyData.HandRightState == HandState.Open)
                {
                    //Utils.LogHelper.GetInstance().ShowMsg("右手距中心位置：" + Math.Abs(jointsData[JointType.HandRight].Position.X - jointsData[JointType.SpineMid].Position.X) * 1000);
                    var handSpineDis = (jointsData[JointType.HandRight].Position.X - jointsData[JointType.SpineMid].Position.X) * 1000;
                    if (handSpineDis>-50&& handSpineDis< validLength)
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
