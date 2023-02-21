using linuxSerialPortDemo.SerialPortTest;
using linuxSerialPortDemo.SystemInfo;

namespace linuxSerialPortDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 主线程  Main跑日志
            // 线程一  tcp通信
            // 线程一  串口
            // 线程二  串口

            //Console.WriteLine("Hello, World!");
            //flyfire.CustomSerialPort 使用跨平台串口类
            // 系统信息
            SystemPlatformInfo.showWelcome();
            // 串口列表
            string[] ports = SerialPortFily.GetPortArray();
            Console.WriteLine("串口列表：");
            foreach (string p in ports)
            {
                Console.WriteLine(p);
            }
            // 串口测试
            Console.WriteLine("输入串口：");
            string input = Console.ReadLine();
            // 开启串口
            // 发送数据
            // 持续接受数据



            // socket 连接 ip 端口进行长连接
        }
    }
}