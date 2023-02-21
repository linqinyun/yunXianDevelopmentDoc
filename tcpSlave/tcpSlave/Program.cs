using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpSlave
{
    internal class Program
    {

        static Socket clientSocket;
        static int port = 7788;
        static void Main(string[] args)
        {
            Console.WriteLine("客户端:");




            //接受数据
            //byte[] data = new byte[1024];
            //int length = clientSocket.Receive(data);
            //string message = Encoding.UTF8.GetString(data, 0, length);

            //Console.WriteLine("接受到来自服务端的响应:" + message);
            //负责监听客户端的线程:创建一个监听线程  
            Thread threadwatch = new Thread(watch);

            //将窗体线程设置为与后台同步，随着主线程结束而结束  
            threadwatch.IsBackground = true;
            //启动线程     
            threadwatch.Start();

            //向服务端发送数居   独立线程
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);//使该线程暂停(休眠)2s
                string message2 = "Fighting!!"; //可以写成string message2=Console.ReadLine(); 
                byte[] data2 = Encoding.UTF8.GetBytes(message2);
                clientSocket.Send(data2);
                Console.WriteLine("向服务端发送消息:{0}", message2);
            }
            Console.ReadKey();

        }
        static void watch()
        {
            //实例化一个socket类
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("192.168.10.145");

            try
            {
                //客户端请求与服务端的连接
                clientSocket.Connect(ip, port);
                Console.WriteLine("服务器连接成功.");
            }
            catch (Exception e)
            {
                Console.WriteLine("服务器连接失败.");
            }
            // 接受数据
            //创建一个通信线程      
            ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
            Thread thread = new Thread(pts);
            thread.IsBackground = true;
            thread.Start(clientSocket);
        }
        static void recv(object socketServerPara)
        {
            Socket sockClient = socketServerPara as Socket;
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                byte[] arrRecvmsg = new byte[1024 * 1024];
                try
                {
                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = sockClient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if(x == 1)
                    {
                        Console.WriteLine("服务器:" + sockClient.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + strRevMsg + "\r\n\n");
                    }
                    else
                    {
                        Console.WriteLine(strRevMsg);
                        x =1;
                    }
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("远程服务器已经中断连接" + "\r\n");
                    sockClient.Close();
                    break;
                }
            }
        }
        //获取当前系统时间  
        static DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
    }
}