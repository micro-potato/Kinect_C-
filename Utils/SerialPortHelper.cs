using System;
using System.Text;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Utils
{
    public class SerialPortHelper
    {
        SerialPort serialPort;
        private delegate void DelSend(string msg);
        public delegate void Datain(string data);
        public event Datain DataIn;
        
        public SerialPortHelper(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName);
            serialPort.BaudRate = baudRate;
            if(!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            serialPort.DataReceived += Sp_DataReceived;
        }

        string recordData = "";
        string endString = "E";
        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string recData = sp.ReadExisting().Replace(" ", "");
                //LogHelper.GetInstance().ShowMsg("Recieve Comm:"+recData);
                string spliceData = recordData + recData;
                string dealData = string.Empty;
                if (spliceData.IndexOf(endString) < 0)
                {
                    recordData = spliceData;
                }
                else
                {
                    dealData = spliceData.Substring(0, spliceData.LastIndexOf(endString) + 1);
                    recordData = spliceData.Substring(spliceData.LastIndexOf(endString) + 1, spliceData.Length - spliceData.LastIndexOf(endString) - 1);


                }
                if (!String.IsNullOrEmpty(dealData)&& dealData != "")
                {
                    if (DataIn != null)
                        DataIn(dealData);
                }
                //Thread.Sleep(500);
            }
            catch
            {
                recordData = "";
            }
        }

        public void BeginSend0x(string message)
        {
            DelSend delSend = new DelSend(Send0x);
            delSend.BeginInvoke(message, new AsyncCallback(Send0xComplete), null);
        }

        public void Send0x(string message)
        {
            //处理数字转换
            string[] strArray = Messageto0x(message);
            Send0x(strArray);
            LogHelper.GetInstance().ShowMsg("send to com port: " + message);
        }

        private void Send0xComplete(IAsyncResult result)
        {
            AsyncResult _result = (AsyncResult)result;
            DelSend delSend = (DelSend)_result.AsyncDelegate;
            delSend.EndInvoke(_result);
        }

        public void SendString(string message)
        {
            //处理数字转换
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            serialPort.Write(message);
        }

        private void Send0x(string[] strArray)
        {
            int byteBufferLength = strArray.Length;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == "")
                {
                    byteBufferLength--;
                }
            }
            // int temp = 0;
            byte[] byteBuffer = new byte[byteBufferLength];
            int ii = 0;
            for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
            {

                Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);

                int decNum = 0;
                if (strArray[i] == "")
                {
                    //ii--;     //加上此句是错误的，下面的continue以延缓了一个ii，不与i同步
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                }

                try    //防止输错，使其只能输入一个字节的字符
                {
                    byteBuffer[ii] = Convert.ToByte(decNum);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                ii++;
            }
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            serialPort.Write(byteBuffer, 0, byteBuffer.Length);
        }

        private string[] Messageto0x(string message)
        {
            string sendBuf = message;
            string sendnoNull = sendBuf.Trim();
            string sendNOComma = sendnoNull.Replace(',', ' ');    //去掉英文逗号
            string sendNOComma1 = sendNOComma.Replace('，', ' '); //去掉中文逗号
            string strSendNoComma2 = sendNOComma1.Replace("0x", "");   //去掉0x
            strSendNoComma2.Replace("0X", "");   //去掉0X
            return strSendNoComma2.Split(' ');
        }
    }
}
