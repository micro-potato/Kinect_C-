using MainApp.Kinect;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace MainApp
{
    public partial class Form1 : Form,ILog
    {
        private ConfigHelper _configs;

        private delegate void DelDataDeal(int index, string ip, byte[] SocketData);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitHotKeys();
            LogHelper.GetInstance().RegLog(this);
            this.WindowState = FormWindowState.Maximized;

            InitKinect();
        }

        #region
        KinectSensor _sensor;
        private BodyFrameReader _bodyFrameReader = null;
        private Body[] bodies = null;
        private GestureController _gestureController;
        private void InitKinect()
        {
            _gestureController = new GestureController();
            _gestureController.SupportedGestures = "SwipeLeft+SwipeRight+PalmHold+FistHold+TwoPalmsOPen+TouchLeft+TouchRight+TouchMiddle";
            _gestureController.GestureRecognised += _gestureController_GestureRecognised;
            _sensor =KinectSensor.GetDefault();
            if (_sensor != null)
            {
                // open the reader for the body frames
                _bodyFrameReader = this._sensor.BodyFrameSource.OpenReader();
                _sensor.Open();
                if (this._bodyFrameReader != null)
                {
                    this._bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
                }
            }
        }

        private void _gestureController_GestureRecognised(object sender, Kinect.KinectEvent.GestureEventArgs e)
        {
            var gestureType = e.GestureType.ToString();
            LogHelper.GetInstance().ShowMsg("Getsture:" + gestureType);
        }

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                       bodies = new Body[bodyFrame.BodyCount];
                    }
                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                var trackedBodies = bodies.Where(b => b.IsTracked == true);
                if (trackedBodies == null || trackedBodies.Count() == 0) return;
                var activeBody = trackedBodies.OrderBy(b => b.Joints[JointType.FootLeft].Position.Z).First();
                if (activeBody!=null) _gestureController.UpdateAllGestures(activeBody);

                //foreach (Body body in this.bodies)
                //{
                //    if (body.IsTracked)
                //    {
                //        //LogHelper.GetInstance().ShowMsg(body.Joints[JointType.HandLeft].Position.X + "," + body.Joints[JointType.ElbowLeft].Position.X);
                //        _gestureController.UpdateAllGestures(body);
                //    }
                //}
            }
        }
        #endregion

        #region HotKey
        private void InitHotKeys()
        {
            //注册热键
            //按Ctrl+Alt+F1组合键弹出或隐藏调试窗口)
            try
            {
                if (HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Alt | HotKey.KeyModifiers.Ctrl, Keys.F1))
                {

                }
                else
                {
                    HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt | HotKey.KeyModifiers.Ctrl, Keys.F8);
                }
            }
            catch
            {
                ShowLog("注册热键错误");
            }
        }
       

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键    
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 101:
                        case 102:
                            rtb_data.Visible = !rtb_data.Visible;
                            return;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        public void ShowLog(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { this.rtb_data.AppendText(msg + "\r\n"); }));
            }
            else
            {
                this.rtb_data.AppendText(msg + "\r\n");
                this.rtb_data.ScrollToCaret();
                if (rtb_data.TextLength > 10000)
                {
                    rtb_data.Clear();
                }
            }

            //this.Invoke(new Action(() => { this.rtb_data.Text = msg; }));
        }
    }
}
