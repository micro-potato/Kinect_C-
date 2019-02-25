using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Kinect
{
    public interface IRelativeGestureSegment
    {
        GesturePartResult CheckGesture(Body bodyData);
    }
}
