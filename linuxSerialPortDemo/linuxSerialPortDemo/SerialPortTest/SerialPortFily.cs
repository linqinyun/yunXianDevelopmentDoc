using flyfire.IO.Ports;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linuxSerialPortDemo.SerialPortTest
{
    internal class SerialPortFily
    {
        // 串口列表
        // 串口状态
        // 打开串口
        // 关闭串口
        // 读取
        // 写入
        private CustomSerialPort serialPort;

        public SerialPortFily(CustomSerialPort serialPort)
        {
            this.serialPort = serialPort;
        }
        /// <summary>
        /// 串口状态
        /// </summary>
        public bool Status
        {
            get => serialPort.IsOpen;
        }
        /// <summary>
        /// 获取当前计算机所有的串行端口号
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortArray()
        {
            return CustomSerialPort.GetPortNames();
        }
        public void setSerialPort(string portName, int baudrate, Parity parity, int databits, StopBits stopBits)
        {
            //端口名
            serialPort.PortName = portName;

            //波特率
            serialPort.BaudRate = baudrate;

            //奇偶校验
            serialPort.Parity = parity;

            //数据位
            serialPort.DataBits = databits;

            //停止位
            serialPort.StopBits = stopBits;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            //打开串口
            try
            {
                serialPort.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("串口被占用");
            }

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                serialPort.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("关闭错误");
            }
        }
    }
}
