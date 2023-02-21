using modbusHelper.Communication;
using modbusHelper.Message;
using modbusHelper.Utils;
using RJCP.IO.Ports;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using static modbusHelper.Communication.SerialPortHelper;
using static System.Collections.Specialized.BitVector32;

namespace modbusHelper
{
    public partial class ModbusForm : Form
    {
        /// <summary>
        /// 串口类
        /// </summary>
        SerialPortHelper conHelper = new SerialPortHelper();
        /// <summary>
        /// 是否为写入模式
        /// </summary>
        private bool isWrite = false;
        /// <summary>
        /// 是否读写线圈
        /// </summary>
        private bool isCoil = false;
        /// <summary>
        /// 是否读写单个值
        /// </summary>
        private bool isSingleData = true;
        /// <summary>
        /// 读写模式
        /// </summary>
        private Object readWriteMode = null;
        public ModbusForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModbusForm_Load(object sender, EventArgs e)
        {
            // 获取串口
            //string[] ports = SerialPortHelper.MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            string[] ports = SerialPortHelper.GetPortArray();
            // 设置可选串口
            cbxPort.Items.AddRange(ports);
            //设置可选波特率
            cbxBaudRate.Items.AddRange(new object[] { 9600, 19200 });
            //设置可选奇偶校验
            cbxParity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            //设置可选数据位
            cbxDataBits.Items.AddRange(new object[] { 5, 6, 7, 8 });
            //设置可选停止位
            cbxStopBits.Items.AddRange(new object[] { 1, 1.5, 2 });
            //设置读写模式
            cbxMode.Items.AddRange(new object[] {
                "读取输出线圈",
                "读取离散输入",
                "读取保持型寄存器",
                "读取输入寄存器",
                "写入单个线圈",
                "写入多个线圈",
                "写入单个寄存器",
                "写入多个寄存器"
            });
            //设置默认选中项 判断是否存在串口
            if (ports.Length > 1) {
                cbxPort.SelectedIndex = 0;
            }
            cbxBaudRate.SelectedIndex = 0;
            cbxParity.SelectedIndex = 0;
            cbxDataBits.SelectedIndex = 3;
            cbxStopBits.SelectedIndex = 0;
            cbxMode.SelectedIndex = 0;
            //显示连接状态
            tbxStatus.Text = conHelper.Status ? "连接成功" : "未连接";
            //初始化禁用读写按钮（未打开串口连接）
            btnRW.Enabled = false;
            btnCRC.Enabled = false;
            btnNoCRC.Enabled = false;
            // 初始化禁用输入框
            cbxMode.Enabled = false;
            nudAddress.Enabled = false;
            nudCount.Enabled = false;
            nudStation.Enabled = false;
            tbxWill.Enabled = false;


            // 设置快捷指令初始化
            tbxDnum.Text = "1";
            cbxConCom.Items.AddRange(new object[] {
                "读设备",
                "写入单值",
                "写入多值",
                "修改地址",
                "显示内容保存",
                "初始内容显示"
            });
            cbxConCom.SelectedIndex= 0;
            tbxDnum.Text = "1";









            //注册接受消息的事件
            conHelper.ReceiveDataEvent += Receivedataevent;
        }

        /// <summary>
        /// 接送消息的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Receivedataevent(object sender,ReceiveDataEventArg e)
        {
            //在窗体上显示接收到的报文
            string msgStr = "";
            for (int i = 0; i < e.Data.Length; i++)
            {
                msgStr += e.Data[i].ToString("X2") + " ";
            }
            msgStr += "\r\n";
            rbxRecMsg.Invoke(new Action(() => { rbxRecMsg.AppendText(msgStr); }));
            //如果是读取数据，则对接收到的消息进行解析
            if (!isWrite)
            {
                string result = "";

                List<short> list = AnalysisMessage.GetRegister(e.Data);

                list.ForEach(m => { result += m.ToString() + ","; });

                tbxValue.Invoke(new Action(() => { tbxValue.Text = result.Remove(result.LastIndexOf(","), 1); }));
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打开或者关闭串口连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCon_Click(object sender, EventArgs e)
        {
            if (!conHelper.Status)
            {
                if (cbxPort.SelectedItem.ToString() != null)
                {
                    //串口号
                    string port = cbxPort.SelectedItem.ToString();
                    //波特率
                    int baudrate = (int)cbxBaudRate.SelectedItem;
                    //奇偶校验
                    Parity parity = GetSelectedParity();
                    //数据位
                    int databits = (int)cbxDataBits.SelectedItem;
                    //停止位
                    StopBits stopBits = GetSelectedStopBits();
                    //设定串口参数
                    conHelper.SetSerialPort(port, baudrate, parity, databits, stopBits);
                    //打开串口
                    conHelper.Open();

                    Thread.Sleep(200);

                    //刷新状态
                    tbxStatus.Text = conHelper.Status ? "连接成功" : "未连接";

                    //启用读写按钮
                    btnRW.Enabled = true;
                    btnCRC.Enabled = true;
                    btnNoCRC.Enabled = true;
                    //启用输入框
                    cbxMode.Enabled = true;
                    nudAddress.Enabled = true;
                    nudCount.Enabled = true;
                    nudStation.Enabled = true;
                    tbxWill.Enabled = true;

                    btnCon.Text = "关闭串口";
                }
                
                
            }
            else
            {
                //关闭串口
                conHelper.Close();

                tbxStatus.Text = conHelper.Status ? "连接成功" : "未连接";

                //初始化禁用读写按钮（未打开串口连接）
                btnRW.Enabled = false;
                btnCRC.Enabled = false;
                btnNoCRC.Enabled = false;
                // 初始化禁用输入框
                cbxMode.Enabled = false;
                nudAddress.Enabled = false;
                nudCount.Enabled = false;
                nudStation.Enabled = false;
                tbxWill.Enabled = false;

                btnCon.Text = "打开串口";
            }
        }

        /// <summary>
        /// 获取窗体选中的奇偶校验
        /// </summary>
        /// <returns></returns>
        private Parity GetSelectedParity()
        {
            switch (cbxParity.SelectedItem.ToString())
            {
                case "Odd":
                    return Parity.Odd;
                case "Even":
                    return Parity.Even;
                case "Mark":
                    return Parity.Mark;
                case "Space":
                    return Parity.Space;
                case "None":
                default:
                    return Parity.None;
            }
        }
        /// <summary>
        /// 获取窗体选中的停止位
        /// </summary>
        /// <returns></returns>
        private StopBits GetSelectedStopBits()
        {
            switch (Convert.ToDouble(cbxStopBits.SelectedItem))
            {
                case 1:
                    return StopBits.One;
                case 1.5:
                    return StopBits.One5;
                case 2:
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }

        private void btnNoCRC_Click(object sender, EventArgs e)
        {
            String str = tbxWill.Text;
            // 发送报文
            conHelper.SendDataMethod(str);
            // 显示发送报文
            str += "\r\n";
            rbxSendMsg.AppendText(str);
        }

        private void btnCRC_Click(object sender, EventArgs e)
        {
            string data = tbxWill.Text;
            
            byte[] bty = FormatConversion.strToToHexByte(data);
            //定义临时字节列表
            List<byte> temp = new List<byte>();
            for (int i = 0;i< bty.Length;i++)
            {
                temp.Add(bty[i]);
            }
            temp.AddRange(CheckSum.CRC16(bty));
            conHelper.SendDataMethod(temp.ToArray());
            string tempData = BitConverter.ToString(temp.ToArray()).Replace("-", " ");
            tempData += "\r\n";
            rbxSendMsg.AppendText(tempData);
        }
        

        private void btnRW_Click(object sender, EventArgs e)
        {
            // 根据参数生成对应报文
            byte[] message = null;
            //if(nudStation.Text.Trim().Length == 0|| nudAddress.Text.Trim().Length == 0 || nudCount.Text.Trim().Length == 0)
            //{
            //    // 弹窗提示
            //    MessageBox.Show("地址或数量填写为空！");
            //    return;
            //}
            //else
            //{
                // 从站地址
                short station = Convert.ToInt16(nudStation.Text);
                // 起始地址
                short startAdr = Convert.ToInt16(nudAddress.Text);
                // 读写数量
                short count = Convert.ToInt16(nudCount.Text);

                // 是否读写模式
                if (isWrite) {

                    //生成写入报文
                    WriteMode mode = (WriteMode)readWriteMode;

                    // 生成单个或多个值的写入报文
                    if (isSingleData) {
                        //判断是否输入单个值
                        if (tbxValue.Text.IndexOf(" ") != -1)
                        {
                            MessageBox.Show("输入值过多");
                            return;
                        }
                        // 生成写入单个线圈的报文
                        if (isCoil)
                        {
                            //生成写入单个线圈的报文
                            bool value = false;
                            if (string.Equals(tbxValue.Text, "True", StringComparison.OrdinalIgnoreCase) || tbxValue.Text == "1")
                            {
                                value = true;
                            }
                            else if (string.Equals(tbxValue.Text, "False", StringComparison.OrdinalIgnoreCase) || tbxValue.Text == "0")
                            {
                                value = false;
                            }
                            else
                            {
                                MessageBox.Show("输入值只能是1、0或者true、false");
                                return;
                            }
                            message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, value);
                        }
                        else {
                            // 生成写入单个寄存器的报文
                            try {
                                message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, short.Parse(tbxValue.Text));
                            }
                            catch (Exception) {
                                MessageBox.Show("输入值只能是1、0或者true、false");
                                return;
                            }
                        }
                    }
                    else
                    {
                        //输入值数组
                        string[] arr = tbxValue.Text.Split(" ");
                        if (isCoil)
                        {
                            // 生成写入多个线圈的报文
                            List<bool> value = new List<bool>();
                            for (int i = 0; i < arr.Length; i++)
                            {
                                bool temp = false;
                                if (string.Equals(arr[i], "True", StringComparison.OrdinalIgnoreCase) || arr[i] == "1")
                                {
                                    temp = true;
                                }
                                else if (string.Equals(tbxValue.Text, "False", StringComparison.OrdinalIgnoreCase) || arr[i] == "0")
                                {
                                    temp = false;
                                }
                                else
                                {
                                    MessageBox.Show("输入值只能是1、0或者true、false");
                                    return;
                                }

                                value.Add(temp);
                            }
                            message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, value);
                        }
                        else {
                            // 生成写入多个寄存器的报文
                            try {
                            //    List<short> value = new List<short>();
                            //    for (int i = 0; i < arr.Length; i++)
                            //    {
                            //        value.Add(short.Parse(arr[i]));
                            //    }

                            //message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, value);
                            message = MessageGenerationModule.GetArrayDataWriteMessage(station, startAdr, tbxValue.Text);
                        }
                            catch (Exception)
                            {
                                MessageBox.Show("输入有误");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    // 发送报文前置 模式选择
                    ReadMode mode = (ReadMode)readWriteMode;
                    message = MessageGenerationModule.GetReadMessage(station, mode, startAdr, count);
                }
                //发送报文
                conHelper.SendDataMethod(message);
                //将发送的报文显示在窗体中
                string msgStr = "";
                for (int i = 0; i < message.Length; i++)
                {
                    msgStr += message[i].ToString("X2") + " ";
                }
                msgStr += "\r\n";
                rbxSendMsg.AppendText(msgStr);
            //}
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReadWriteMode();
            // 清空文本 初始文本
            nudStation.Text = "1";
            nudAddress.Text = "1";
            nudCount.Text = "1";
            // 清空
            tbxValue.Clear();

            //读写按钮显示文本
            btnRW.Text = isWrite ? "写入" : "读取";
            //是否可修改计数
            nudCount.Enabled = isWrite ? false : true;
            //是否可输入值
            tbxValue.Enabled = isWrite ? true : false;

        }
        /// <summary>
        /// 根据选中的读写模式更新字段值
        /// </summary>
        private void GetReadWriteMode()
        {
            switch (cbxMode.SelectedItem.ToString())
            {
                case "读取输出线圈":
                default:
                    isWrite = false;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = ReadMode.Read01;
                    break;

                case "读取离散输入":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = ReadMode.Read02;
                    break;

                case "读取保持型寄存器":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = false;
                    readWriteMode = ReadMode.Read03;
                    break;

                case "读取输入寄存器":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = false;
                    readWriteMode = ReadMode.Read04;
                    break;

                case "写入单个线圈":
                    isWrite = true;
                    isSingleData = true;
                    isCoil = true;
                    readWriteMode = WriteMode.Write01;
                    break;

                case "写入多个线圈":
                    isWrite = true;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = WriteMode.Write01s;
                    break;

                case "写入单个寄存器":
                    isWrite = true;
                    isSingleData = true;
                    isCoil = false;
                    readWriteMode = WriteMode.Write03;
                    break;

                case "写入多个寄存器":
                    isWrite = true;
                    isSingleData = false;
                    isCoil = false;
                    readWriteMode = WriteMode.Write03s;
                    break;
            }
        }

        private void requestBtn_Click(object sender, EventArgs e)
        {
            rbxSendMsg.Clear();
        }

        private void responesBtn_Click(object sender, EventArgs e)
        {
            rbxRecMsg.Clear();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            // 扫描端口
            string[] ports = SerialPortHelper.GetPortArray();

            cbxPort.SelectedIndex = -1;
            cbxPort.Items.Clear();
            cbxPort.Text = "";
            // 设置可选串口
            cbxPort.Items.AddRange(ports);
        }

        private void cbxConCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetComReadWriteMode();
            btnComRW.Text = isWrite ? "写入" : "读取";
            tbxComValue.Clear();
            tbxComNum.Enabled = isWrite ? false : true;
            tbxComValue.Enabled= isWrite ? true : false;
        }

        /// <summary>
        /// 根据选中的读写模式更新字段值
        /// </summary>
        private void GetComReadWriteMode()
        {
            switch (cbxConCom.SelectedItem.ToString())
            {
                case "读设备":
                default:
                    readWriteMode = ReadMode.Read03;
                    isWrite= false;
                    isSingleData= false;
                    tbxComAddr.Text = "1";
                    tbxComNum.Text = "3";
                    lbTip.Text = "";
                    break;

                case "写入单值":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData= true;
                    tbxComAddr.Text = "0";
                    lbTip.Text = "寄存地址0-5 对应显像管1-6";
                    break;

                case "写入多值":
                    readWriteMode = WriteMode.Write03s;
                    isWrite = true;
                    isSingleData= false;
                    tbxComAddr.Text = "6";
                    lbTip.Text = "";
                    break;

                case "修改地址":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "10";
                    lbTip.Text = "";
                    break;

                case "显示内容保存":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "9";
                    lbTip.Text = "0 不保存 1 保存";
                    break;

                case "初始内容显示":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "15";
                    lbTip.Text = "0 不保存 1 显示地址 2保存自定内容显示";
                    break;
            }
        }

        private void btnComRW_Click(object sender, EventArgs e)
        {
            // 根据参数生成对应报文
            byte[] message = null;

            string stationStr = tbxDnum.Text;
            string startAdrStr = tbxComAddr.Text;
            string countStr = tbxComNum.Text;

            short station = Convert.ToInt16(stationStr);
            short startAdr = Convert.ToInt16(startAdrStr);
            short count = Convert.ToInt16(countStr);


            if (isWrite) {
                // 读写
                
                WriteMode mode = (WriteMode)readWriteMode;
                if (isSingleData) {
                    // 单值
                    
                    //判断是否输入单个值
                    //if (tbxComValue.Text.IndexOf(" ") != -1)
                    //{
                    //    MessageBox.Show("输入值过多");
                    //    return;
                    //}
                    // 生成写入单个寄存器的报文
                    try
                    {
                        string str = null;
                        if (cbxConCom.SelectedItem.ToString() == "写入单值") {
                           str = FormatConversion.GetAscllByString(tbxComValue.Text);
                        }
                        else
                        {
                            str = tbxComValue.Text;
                        }
                        
                        message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, short.Parse(str));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("输入值只能是1、0或者true、false");
                        return;
                    }
                }
                else
                {
                    // 多值
                    string tbxComValueStr = tbxComValue.Text;
                    
                    bool dr = false; // 小数
                    int dp = 0;
                    string output = "";
                    string tbxComValueStrTemp = "";
                    bool pon = false; // 正负
                    // 判断正负
                    if (tbxComValueStr.IndexOf("-") == -1) {
                        // 不存在负号
                    }
                    else
                    {
                        // 去负号
                        tbxComValueStr = tbxComValueStr.Replace("-","");


                        pon = true;
                    }
                    //MessageBox.Show(tbxComValueStr);return;
                    // 判断小数
                    if(tbxComValueStr.IndexOf(".") == -1)
                    {
                        // 不存在小数 
                        tbxComValueStrTemp = tbxComValueStr;

                    }
                    else
                    {
                        // 取小数点
                        tbxComValueStrTemp = tbxComValueStr.Replace(".", "");
                        // 查询小数点第几位
                        dp = String.Join("", tbxComValueStr.Reverse()).IndexOf(".");
                        //MessageBox.Show(dp.ToString());

                        dr = true;
                    }
                    // 转无符号整数 转字符字节串
                    int tbxComValueInt = int.Parse(tbxComValueStrTemp);
                    byte[] b_temp = BitConverter.GetBytes(tbxComValueInt).ToArray();
                    byte[] b_temps = FormatConversion.bytesTrimEnd(b_temp);
                    byte[] b_tempss = b_temps.Reverse().ToArray();
                    //byte[] noStr = System.Text.Encoding.ASCII.GetBytes(tbxComValueStrTemp);


                    List<byte> temp = new List<byte>();

                    // 拼接正负
                    if (pon)
                    {
                        // 7 16 17
                        //String tempvalue =Convert.ToString(tbxComValueInt, 16);
                        //List<string> tempValue7 = new List<string>();

                        //MessageBox.Show(tempvalue);return;
                        string output7 = "";
                        if (b_tempss.Length == 1) {
                            output7 = "00 ";
                        }
                        foreach (byte b in b_tempss)
                        {
                            output7 += b.ToString("X2") + " ";
                        }
                        //MessageBox.Show(output7); return;
                        byte[] message7 = MessageGenerationModule.GetArrayDataWriteMessage(station, (short)(startAdr + 1), output7);
                        conHelper.SendDataMethod(message7);
                        //将发送的报文显示在窗体中
                        string msg7 = "";
                        for (int i = 0; i < message7.Length; i++)
                        {
                            msg7 += message7[i].ToString("X2") + " ";
                        }
                        msg7 += "\r\n";
                        rbxSendMsg.AppendText(msg7);
                        // 拼接正负
                        // 发送 符号指令
                        Thread.Sleep(200);
                        byte[] messagetemp = MessageGenerationModule.MessageGeneration(station, WriteMode.Write03, 17, short.Parse("1"));
                        //发送报文
                        conHelper.SendDataMethod(messagetemp);
                        //将发送的报文显示在窗体中
                        string msgStrtmp = "";
                        for (int i = 0; i < messagetemp.Length; i++)
                        {
                            msgStrtmp += messagetemp[i].ToString("X2") + " ";
                        }
                        msgStrtmp += "\r\n";
                        rbxSendMsg.AppendText(msgStrtmp);
                        if (dr)
                        {
                            // 发送小数位
                            Thread.Sleep(200);
                            byte[] message16 = MessageGenerationModule.MessageGeneration(station, WriteMode.Write03, 16, short.Parse(dp.ToString()));
                            //发送报文
                            conHelper.SendDataMethod(message16);
                            //将发送的报文显示在窗体中
                            string msgStrtmp16 = "";
                            for (int i = 0; i < message16.Length; i++)
                            {
                                msgStrtmp16 += message16[i].ToString("X2") + " ";
                            }
                            msgStrtmp16 += "\r\n";
                            rbxSendMsg.AppendText(msgStrtmp16);
                            return;
                        }
                        else {
                            return;
                        }
                        
                    }

                    if (dr)
                    {
                        // 拼接小数            
                        temp.Add((byte)dp);
                        //rbxSendMsg.AppendText(output);
                    }
                    else {
                        // 拼接 00 无符号拼接
                        temp.Add((byte)0);
                    }
                    // 拼接补0
                    int joinCount = 3 - b_tempss.Length;
                    for (int i = 0; i < joinCount; i++)
                    {
                        temp.Add((byte)0);
                    }
                    temp.AddRange(b_tempss);

                    foreach (byte b in temp)
                    {
                        output += b.ToString("X2") + " ";
                    }
                    try
                    {
                        message = MessageGenerationModule.GetArrayDataWriteMessage(station, startAdr, output);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("输入有误");
                        return;
                    }

                }
            }
            else
            {
                // 发送报文前置 模式选择
                ReadMode mode = (ReadMode)readWriteMode;
                message = MessageGenerationModule.GetReadMessage(station, mode, startAdr, count);
            }
            //发送报文
            conHelper.SendDataMethod(message);
            //将发送的报文显示在窗体中
            string msgStr = "";
            for (int i = 0; i < message.Length; i++)
            {
                msgStr += message[i].ToString("X2") + " ";
            }
            msgStr += "\r\n";
            rbxSendMsg.AppendText(msgStr);


        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x10, 0x00, 0x00, 0x00, 0x07, 0x0E, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30 };
            //定义临时字节列表
            List<byte> temp = new List<byte>();
            temp.Add((byte)int.Parse(tbxDnum.Text));
            temp.AddRange(data);
            byte[] bty = temp.ToArray();            
            temp.AddRange(CheckSum.CRC16(bty));
            conHelper.SendDataMethod(temp.ToArray());
            string tempData = BitConverter.ToString(temp.ToArray()).Replace("-", " ");
            tempData += "\r\n";
            rbxSendMsg.AppendText(tempData);

        }
    }
}