using System.Net.Sockets;
using System.Net;
using System.Text;

namespace tcpWinMaster
{
    internal class Program
    {
        // 多线程服务2
        static string ip="192.168.10.145";
        static Socket serverSocket;
        static int port = 7788;
        // 多线程服务3  多客户端访问
        // 创建一个和客户端通信的套接字
        static Socket socketwatch = null;
        //定义一个集合，存储客户端信息
        static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };
        static void Main(string[] args)
        {
            // 主机
            tcpServerSy3();
        }
        // 多线程服务3
        public static void tcpServerSy3()
        {
            // 定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
             socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息需要一个IP地址和端口号  
            IPAddress address = IPAddress.Parse(ip);
            //将IP地址和端口号绑定到网络节点point上  
            IPEndPoint point = new IPEndPoint(address, port);
            //此端口专门用来监听的  

            //监听绑定的网络节点  
            socketwatch.Bind(point);
            //将套接字的监听队列长度限制为20  
            socketwatch.Listen(20);
            //负责监听客户端的线程:创建一个监听线程  
            Thread threadwatch = new Thread(watchconnecting);

            //将窗体线程设置为与后台同步，随着主线程结束而结束  
            threadwatch.IsBackground = true;
            //启动线程     
            threadwatch.Start();
            Console.WriteLine("开启监听。。。");
            Console.WriteLine("点击输入任意数据回车退出程序。。。");
            Console.ReadKey();
            Console.WriteLine("退出监听，并关闭程序。");
        }
        //监听客户端发来的请求  
        static void watchconnecting()
        {
            Socket connection = null;
            //持续不断监听客户端发来的请求     
            while (true)
            {
                try
                {
                    connection = socketwatch.Accept();
                }
                catch (Exception ex)
                {
                    //提示套接字监听异常     
                    Console.WriteLine(ex.Message);
                    break;
                }

                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                //让客户显示"连接成功的"的信息  
                string sendmsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                connection.Send(arrSendMsg);

                //客户端网络结点号  
                string remoteEndPoint = connection.RemoteEndPoint.ToString();
                //显示与客户端连接情况
                Console.WriteLine("成功与" + remoteEndPoint + "客户端建立连接！\t\n");
                //添加客户端信息  
                clientConnectionItems.Add(remoteEndPoint, connection);
                //IPEndPoint netpoint = new IPEndPoint(clientIP,clientPort); 
                IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;
                //创建一个通信线程      
                ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
                Thread thread = new Thread(pts);
                //设置为后台线程，随着主线程退出而退出 
                thread.IsBackground = true;
                //启动线程     
                thread.Start(connection);
            }
        }
        /// <summary>
        /// 接收客户端发来的信息，客户端套接字对象
        /// </summary>
        /// <param name="socketclientpara"></param>    
        static void recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;
            while (true)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度    
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);
                    //将机器接受到的字节数组转换为人可以读懂的字符串     
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    //将发送的字符串信息附加到文本框txtMsg上     
                    Console.WriteLine("客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n\n");

                    socketServer.Send(Encoding.UTF8.GetBytes("测试server 是否可以发送数据给client "));
                }
                catch (Exception ex)
                {
                    clientConnectionItems.Remove(socketServer.RemoteEndPoint.ToString());
                    Console.WriteLine("Client Count:" + clientConnectionItems.Count);
                    //提示套接字监听异常  
                    Console.WriteLine("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
                    //关闭之前accept出来的和客户端进行通信的套接字 
                    socketServer.Close();
                    break;
                }
            }
        }
        ///      
        /// 获取当前系统时间的方法    
        /// 当前时间     
        static DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
        // 多线程服务2
        public static void tcpServerSy2()
        {
            Console.WriteLine("服务端:");
            //实例化socket类
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("192.168.10.145");
            //标识网络地址
            EndPoint point = new IPEndPoint(ip, port);             //绑定IP地址和端口号
            serverSocket.Bind(point);

            //开始监听客户端的连接
            serverSocket.Listen(10);
            Console.WriteLine("等待客户端连接...");

            Thread myThread = new Thread(ListenClientSocket);
            myThread.Start();


            Console.ReadKey();
        }
        /// <summary>
        /// 监听客户端
        /// </summary>
        static void ListenClientSocket()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();//接受客户端的连接
                Console.WriteLine("客户端连接成功");

                string message = "2023 link";
                byte[] data = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(data);

                Thread receive = new Thread(receiveSocket);//receiveSocket 被传递的方法
                receive.Start(clientSocket); //clientSocket 被传递的参数
            }

        }

        /// <summary>
        /// 接收来自客户端的消息
        /// </summary>
        static void receiveSocket(object clientSocket)  //////  这里的参数是线程中的参数，参数类型必须是object类型
        {
            Socket myClientSocket = (Socket)clientSocket;  //// 将object类型的参数转换成socket类型  使用参数来启动线程，执行后面的代码
            while (true)
            {
                byte[] data = new byte[1024];
                int length = myClientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, length);
                Console.WriteLine("接收到来自客户端的消息:" + message);
            }
        }

        // socket 同步服务
        public static void tcpServerSy()
        {
            try
            {

                int port = 7788;

                string host = "192.168.10.145";
                //string host = "127.0.0.1";

                IPAddress ip = IPAddress.Parse(host);

                IPEndPoint ipe = new IPEndPoint(ip, port);

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket类

                s.Bind(ipe);//绑定7788端口

                s.Listen(0);//开始监听

                Console.WriteLine("Wait for connect");

                Socket temp = s.Accept();//为新建连接创建新的Socket。

                Console.WriteLine("Get a connect");

                string recvStr = "";

                byte[] recvBytes = new byte[1024];

                int bytes;

                bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//从客户端接受信息

                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

                Console.WriteLine("Server Get Message:{0}", recvStr);//把客户端传来的信息显示出来

                string sendStr = "Ok!Client Send Message Sucessful!";

                byte[] bs = Encoding.ASCII.GetBytes(sendStr);

                temp.Send(bs, bs.Length, 0);//返回客户端成功信息

                temp.Close();

                s.Close();

            }

            catch (ArgumentNullException e)
            {

                Console.WriteLine("ArgumentNullException: {0}", e);
            }

            catch (SocketException e)
            {

                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("Press Enter to Exit");

            Console.ReadLine();

        }
        // tcp 异步服务
        public class TcpServer
        {
            //服务器侦听对象
            public TcpListener listener;
            //客户端列表
            public static TcpClient[] ClientList = new TcpClient[5];
            //已连接的客户端数
            private int ClientsNum = 0;
            //构造方法
            public TcpServer(string ip, int port)
            {
                this.listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
                listener.Start();
            }
            //异步侦听
            public void DoBeginAccept()
            {

                //开始从客户端监听连接
                Console.WriteLine("Waitting for a connection");
                //接收连接
                //开始准备接入新的连接，一旦有新连接尝试则调用回调函数DoAcceptTcpCliet
                listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClient), listener);


            }
            //处理客户端的连接
            public void DoAcceptTcpClient(IAsyncResult iar)
            {
                //还原原始的TcpListner对象
                this.listener = (TcpListener)iar.AsyncState;
                //完成连接的动作，并返回新的TcpClient
                TcpClient client = this.listener.EndAcceptTcpClient(iar);
                Console.WriteLine(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString() + "连接成功");
                //更新链接入的客户端数量，将客户端保存至已连接客户端列表中
                ClientList[ClientsNum++] = client;
                //获取输入输出流
                NetworkStream iostream = client.GetStream();
                //返回hello world
                string info = "hello world";
                byte[] infobyte = new byte[info.Length];
                for (int i = 0; i < infobyte.Length; i++)
                {
                    infobyte[i] = Convert.ToByte(info[i]);
                    //Console.WriteLine(infobyte[i]);
                }
                iostream.Write(infobyte, 0, infobyte.Length);
            }
        }
        // tcp 同步服务
        public static void synchronizationTcp()
        {
            TcpListener server = null;
            try
            {
                // 设置服务器端口号为 13000，IP地址为127.0.0.1.
                Int32 port = 7788;
                IPAddress localAddr = IPAddress.Parse("192.168.10.145");

                // 实例化一个服务器侦听端口的对象
                server = new TcpListener(localAddr, port);

                // 开始侦听
                server.Start();

                // 进入侦听循环
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // 阻塞式的侦听
                    // 得到侦听到的TCP客户端
                    TcpClient client = server.AcceptTcpClient();     //AcceptTcpClient()这个函数为阻塞式
                                                                     //得到客户端的基本信息
                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                    //将其打印在控制台
                    Console.WriteLine(clientIP + ":" + clientPort + " Connected!");
                    //得到这个客户端的输入输出流
                    NetworkStream iostream = client.GetStream();
                    //返回hello world
                    string info = "hello world";
                    byte[] infobyte = new byte[info.Length];
                    for (int i = 0; i < infobyte.Length; i++)
                    {
                        infobyte[i] = Convert.ToByte(info[i]);
                        //Console.WriteLine(infobyte[i]);
                    }
                    iostream.Write(infobyte, 0, infobyte.Length);

                }


            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }
    }
}