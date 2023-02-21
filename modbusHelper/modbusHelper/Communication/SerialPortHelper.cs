using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace modbusHelper.Communication
{
    /// <summary>
    /// 自定义串口消息接收事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ReceiveDataEventHandler(object sender, ReceiveDataEventArg e);
    class SerialPortHelper
    {
        /// <summary>
        /// 自定义串口消息接收事件
        /// </summary>
        public event ReceiveDataEventHandler ReceiveDataEvent;

        private SerialPortStream serialPort;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialPortHelper() {
            serialPort= new SerialPortStream();
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
            return SerialPortStream.GetPortNames();
        }
        public void SetSerialPort(string portName, int baudrate, Parity parity, int databits, StopBits stopBits) {
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
            //串口接收数据事件
            serialPort.DataReceived += ReceiveDataMethod;
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            //打开串口
            try {
                serialPort.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("串口被占用");
            }
            
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            serialPort.Close();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        public void SendDataMethod(byte[] data)
        {
            //获取串口状态，true为已打开，false为未打开
            bool isOpen = serialPort.IsOpen;

            if (!isOpen)
            {
                Open();
            }

            //发送字节数组
            //参数1：包含要写入端口的数据的字节数组。
            //参数2：参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
            //参数3：要写入的字节数。 
            serialPort.Write(data, 0, data.Length);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        public void SendDataMethod(string data)
        {
            //获取串口状态，true为已打开，false为未打开
            bool isOpen = serialPort.IsOpen;

            if (!isOpen)
            {
                Open();
            }

            //直接发送字符串
            serialPort.Write(data);
        }
        /// <summary>
        /// 串口接收到数据触发此方法进行数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiveDataMethod(object sender, SerialDataReceivedEventArgs e)
        {
            ReceiveDataEventArg arg = new ReceiveDataEventArg();

            //读取串口缓冲区的字节数据
            arg.Data = new byte[serialPort.BytesToRead];
            serialPort.Read(arg.Data, 0, serialPort.BytesToRead);

            //触发自定义消息接收事件，把串口数据发送出去
            if (ReceiveDataEvent != null && arg.Data.Length != 0)
            {
                ReceiveDataEvent.Invoke(null, arg);
            }
        }

        /// <summary>        
        /// 枚举win32 api
        /// </summary>
        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }

        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        Console.WriteLine(hardInfo);
                        //if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                        //{
                        //    strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        //}

                    }
                    
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
    }
}
