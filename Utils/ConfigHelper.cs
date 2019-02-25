using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Utils
{
    public class ConfigHelper
    {
        public string Point1 { get; set; }
        public string Point2 { get; set; }
        public string Point3 { get; set; }
        public string ComPort { get; set; }
        public string PointLeave { get; set; }
        public int BundRate { get; set; }
        public int Port { get; set; }

        private static ConfigHelper _configHelper;
        private ConfigHelper()
        {

        }

        public static ConfigHelper GetInstance()
        {
            if (_configHelper == null)
            {
                _configHelper = new ConfigHelper();
            }
            return _configHelper;
        }

        public void ResolveConfig(string configPath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configPath);
            this.Point1= xmlDocument.SelectSingleNode("Data/Point1").InnerText;
            this.Point2 = xmlDocument.SelectSingleNode("Data/Point2").InnerText;
            this.Point3 = xmlDocument.SelectSingleNode("Data/Point3").InnerText;
            this.PointLeave = xmlDocument.SelectSingleNode("Data/PointLeave").InnerText;
            this.ComPort = xmlDocument.SelectSingleNode("Data/ComPort").InnerText;
            this.BundRate = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/BundRate").InnerText);
            this.Port = Convert.ToInt32(xmlDocument.SelectSingleNode("Data/Port").InnerText);
        }
    }
}
