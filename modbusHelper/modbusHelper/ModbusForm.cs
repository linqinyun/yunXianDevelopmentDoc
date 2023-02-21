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
        /// ������
        /// </summary>
        SerialPortHelper conHelper = new SerialPortHelper();
        /// <summary>
        /// �Ƿ�Ϊд��ģʽ
        /// </summary>
        private bool isWrite = false;
        /// <summary>
        /// �Ƿ��д��Ȧ
        /// </summary>
        private bool isCoil = false;
        /// <summary>
        /// �Ƿ��д����ֵ
        /// </summary>
        private bool isSingleData = true;
        /// <summary>
        /// ��дģʽ
        /// </summary>
        private Object readWriteMode = null;
        public ModbusForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModbusForm_Load(object sender, EventArgs e)
        {
            // ��ȡ����
            //string[] ports = SerialPortHelper.MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            string[] ports = SerialPortHelper.GetPortArray();
            // ���ÿ�ѡ����
            cbxPort.Items.AddRange(ports);
            //���ÿ�ѡ������
            cbxBaudRate.Items.AddRange(new object[] { 9600, 19200 });
            //���ÿ�ѡ��żУ��
            cbxParity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            //���ÿ�ѡ����λ
            cbxDataBits.Items.AddRange(new object[] { 5, 6, 7, 8 });
            //���ÿ�ѡֹͣλ
            cbxStopBits.Items.AddRange(new object[] { 1, 1.5, 2 });
            //���ö�дģʽ
            cbxMode.Items.AddRange(new object[] {
                "��ȡ�����Ȧ",
                "��ȡ��ɢ����",
                "��ȡ�����ͼĴ���",
                "��ȡ����Ĵ���",
                "д�뵥����Ȧ",
                "д������Ȧ",
                "д�뵥���Ĵ���",
                "д�����Ĵ���"
            });
            //����Ĭ��ѡ���� �ж��Ƿ���ڴ���
            if (ports.Length > 1) {
                cbxPort.SelectedIndex = 0;
            }
            cbxBaudRate.SelectedIndex = 0;
            cbxParity.SelectedIndex = 0;
            cbxDataBits.SelectedIndex = 3;
            cbxStopBits.SelectedIndex = 0;
            cbxMode.SelectedIndex = 0;
            //��ʾ����״̬
            tbxStatus.Text = conHelper.Status ? "���ӳɹ�" : "δ����";
            //��ʼ�����ö�д��ť��δ�򿪴������ӣ�
            btnRW.Enabled = false;
            btnCRC.Enabled = false;
            btnNoCRC.Enabled = false;
            // ��ʼ�����������
            cbxMode.Enabled = false;
            nudAddress.Enabled = false;
            nudCount.Enabled = false;
            nudStation.Enabled = false;
            tbxWill.Enabled = false;


            // ���ÿ��ָ���ʼ��
            tbxDnum.Text = "1";
            cbxConCom.Items.AddRange(new object[] {
                "���豸",
                "д�뵥ֵ",
                "д���ֵ",
                "�޸ĵ�ַ",
                "��ʾ���ݱ���",
                "��ʼ������ʾ"
            });
            cbxConCom.SelectedIndex= 0;
            tbxDnum.Text = "1";









            //ע�������Ϣ���¼�
            conHelper.ReceiveDataEvent += Receivedataevent;
        }

        /// <summary>
        /// ������Ϣ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Receivedataevent(object sender,ReceiveDataEventArg e)
        {
            //�ڴ�������ʾ���յ��ı���
            string msgStr = "";
            for (int i = 0; i < e.Data.Length; i++)
            {
                msgStr += e.Data[i].ToString("X2") + " ";
            }
            msgStr += "\r\n";
            rbxRecMsg.Invoke(new Action(() => { rbxRecMsg.AppendText(msgStr); }));
            //����Ƕ�ȡ���ݣ���Խ��յ�����Ϣ���н���
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
        /// �򿪻��߹رմ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCon_Click(object sender, EventArgs e)
        {
            if (!conHelper.Status)
            {
                if (cbxPort.SelectedItem.ToString() != null)
                {
                    //���ں�
                    string port = cbxPort.SelectedItem.ToString();
                    //������
                    int baudrate = (int)cbxBaudRate.SelectedItem;
                    //��żУ��
                    Parity parity = GetSelectedParity();
                    //����λ
                    int databits = (int)cbxDataBits.SelectedItem;
                    //ֹͣλ
                    StopBits stopBits = GetSelectedStopBits();
                    //�趨���ڲ���
                    conHelper.SetSerialPort(port, baudrate, parity, databits, stopBits);
                    //�򿪴���
                    conHelper.Open();

                    Thread.Sleep(200);

                    //ˢ��״̬
                    tbxStatus.Text = conHelper.Status ? "���ӳɹ�" : "δ����";

                    //���ö�д��ť
                    btnRW.Enabled = true;
                    btnCRC.Enabled = true;
                    btnNoCRC.Enabled = true;
                    //���������
                    cbxMode.Enabled = true;
                    nudAddress.Enabled = true;
                    nudCount.Enabled = true;
                    nudStation.Enabled = true;
                    tbxWill.Enabled = true;

                    btnCon.Text = "�رմ���";
                }
                
                
            }
            else
            {
                //�رմ���
                conHelper.Close();

                tbxStatus.Text = conHelper.Status ? "���ӳɹ�" : "δ����";

                //��ʼ�����ö�д��ť��δ�򿪴������ӣ�
                btnRW.Enabled = false;
                btnCRC.Enabled = false;
                btnNoCRC.Enabled = false;
                // ��ʼ�����������
                cbxMode.Enabled = false;
                nudAddress.Enabled = false;
                nudCount.Enabled = false;
                nudStation.Enabled = false;
                tbxWill.Enabled = false;

                btnCon.Text = "�򿪴���";
            }
        }

        /// <summary>
        /// ��ȡ����ѡ�е���żУ��
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
        /// ��ȡ����ѡ�е�ֹͣλ
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
            // ���ͱ���
            conHelper.SendDataMethod(str);
            // ��ʾ���ͱ���
            str += "\r\n";
            rbxSendMsg.AppendText(str);
        }

        private void btnCRC_Click(object sender, EventArgs e)
        {
            string data = tbxWill.Text;
            
            byte[] bty = FormatConversion.strToToHexByte(data);
            //������ʱ�ֽ��б�
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
            // ���ݲ������ɶ�Ӧ����
            byte[] message = null;
            //if(nudStation.Text.Trim().Length == 0|| nudAddress.Text.Trim().Length == 0 || nudCount.Text.Trim().Length == 0)
            //{
            //    // ������ʾ
            //    MessageBox.Show("��ַ��������дΪ�գ�");
            //    return;
            //}
            //else
            //{
                // ��վ��ַ
                short station = Convert.ToInt16(nudStation.Text);
                // ��ʼ��ַ
                short startAdr = Convert.ToInt16(nudAddress.Text);
                // ��д����
                short count = Convert.ToInt16(nudCount.Text);

                // �Ƿ��дģʽ
                if (isWrite) {

                    //����д�뱨��
                    WriteMode mode = (WriteMode)readWriteMode;

                    // ���ɵ�������ֵ��д�뱨��
                    if (isSingleData) {
                        //�ж��Ƿ����뵥��ֵ
                        if (tbxValue.Text.IndexOf(" ") != -1)
                        {
                            MessageBox.Show("����ֵ����");
                            return;
                        }
                        // ����д�뵥����Ȧ�ı���
                        if (isCoil)
                        {
                            //����д�뵥����Ȧ�ı���
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
                                MessageBox.Show("����ֵֻ����1��0����true��false");
                                return;
                            }
                            message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, value);
                        }
                        else {
                            // ����д�뵥���Ĵ����ı���
                            try {
                                message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, short.Parse(tbxValue.Text));
                            }
                            catch (Exception) {
                                MessageBox.Show("����ֵֻ����1��0����true��false");
                                return;
                            }
                        }
                    }
                    else
                    {
                        //����ֵ����
                        string[] arr = tbxValue.Text.Split(" ");
                        if (isCoil)
                        {
                            // ����д������Ȧ�ı���
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
                                    MessageBox.Show("����ֵֻ����1��0����true��false");
                                    return;
                                }

                                value.Add(temp);
                            }
                            message = MessageGenerationModule.MessageGeneration(station, mode, startAdr, value);
                        }
                        else {
                            // ����д�����Ĵ����ı���
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
                                MessageBox.Show("��������");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    // ���ͱ���ǰ�� ģʽѡ��
                    ReadMode mode = (ReadMode)readWriteMode;
                    message = MessageGenerationModule.GetReadMessage(station, mode, startAdr, count);
                }
                //���ͱ���
                conHelper.SendDataMethod(message);
                //�����͵ı�����ʾ�ڴ�����
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
        /// ģʽ�л�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReadWriteMode();
            // ����ı� ��ʼ�ı�
            nudStation.Text = "1";
            nudAddress.Text = "1";
            nudCount.Text = "1";
            // ���
            tbxValue.Clear();

            //��д��ť��ʾ�ı�
            btnRW.Text = isWrite ? "д��" : "��ȡ";
            //�Ƿ���޸ļ���
            nudCount.Enabled = isWrite ? false : true;
            //�Ƿ������ֵ
            tbxValue.Enabled = isWrite ? true : false;

        }
        /// <summary>
        /// ����ѡ�еĶ�дģʽ�����ֶ�ֵ
        /// </summary>
        private void GetReadWriteMode()
        {
            switch (cbxMode.SelectedItem.ToString())
            {
                case "��ȡ�����Ȧ":
                default:
                    isWrite = false;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = ReadMode.Read01;
                    break;

                case "��ȡ��ɢ����":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = ReadMode.Read02;
                    break;

                case "��ȡ�����ͼĴ���":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = false;
                    readWriteMode = ReadMode.Read03;
                    break;

                case "��ȡ����Ĵ���":
                    isWrite = false;
                    isSingleData = false;
                    isCoil = false;
                    readWriteMode = ReadMode.Read04;
                    break;

                case "д�뵥����Ȧ":
                    isWrite = true;
                    isSingleData = true;
                    isCoil = true;
                    readWriteMode = WriteMode.Write01;
                    break;

                case "д������Ȧ":
                    isWrite = true;
                    isSingleData = false;
                    isCoil = true;
                    readWriteMode = WriteMode.Write01s;
                    break;

                case "д�뵥���Ĵ���":
                    isWrite = true;
                    isSingleData = true;
                    isCoil = false;
                    readWriteMode = WriteMode.Write03;
                    break;

                case "д�����Ĵ���":
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
            // ɨ��˿�
            string[] ports = SerialPortHelper.GetPortArray();

            cbxPort.SelectedIndex = -1;
            cbxPort.Items.Clear();
            cbxPort.Text = "";
            // ���ÿ�ѡ����
            cbxPort.Items.AddRange(ports);
        }

        private void cbxConCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetComReadWriteMode();
            btnComRW.Text = isWrite ? "д��" : "��ȡ";
            tbxComValue.Clear();
            tbxComNum.Enabled = isWrite ? false : true;
            tbxComValue.Enabled= isWrite ? true : false;
        }

        /// <summary>
        /// ����ѡ�еĶ�дģʽ�����ֶ�ֵ
        /// </summary>
        private void GetComReadWriteMode()
        {
            switch (cbxConCom.SelectedItem.ToString())
            {
                case "���豸":
                default:
                    readWriteMode = ReadMode.Read03;
                    isWrite= false;
                    isSingleData= false;
                    tbxComAddr.Text = "1";
                    tbxComNum.Text = "3";
                    lbTip.Text = "";
                    break;

                case "д�뵥ֵ":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData= true;
                    tbxComAddr.Text = "0";
                    lbTip.Text = "�Ĵ��ַ0-5 ��Ӧ�����1-6";
                    break;

                case "д���ֵ":
                    readWriteMode = WriteMode.Write03s;
                    isWrite = true;
                    isSingleData= false;
                    tbxComAddr.Text = "6";
                    lbTip.Text = "";
                    break;

                case "�޸ĵ�ַ":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "10";
                    lbTip.Text = "";
                    break;

                case "��ʾ���ݱ���":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "9";
                    lbTip.Text = "0 ������ 1 ����";
                    break;

                case "��ʼ������ʾ":
                    readWriteMode = WriteMode.Write03;
                    isWrite = true;
                    isSingleData = true;
                    tbxComAddr.Text = "15";
                    lbTip.Text = "0 ������ 1 ��ʾ��ַ 2�����Զ�������ʾ";
                    break;
            }
        }

        private void btnComRW_Click(object sender, EventArgs e)
        {
            // ���ݲ������ɶ�Ӧ����
            byte[] message = null;

            string stationStr = tbxDnum.Text;
            string startAdrStr = tbxComAddr.Text;
            string countStr = tbxComNum.Text;

            short station = Convert.ToInt16(stationStr);
            short startAdr = Convert.ToInt16(startAdrStr);
            short count = Convert.ToInt16(countStr);


            if (isWrite) {
                // ��д
                
                WriteMode mode = (WriteMode)readWriteMode;
                if (isSingleData) {
                    // ��ֵ
                    
                    //�ж��Ƿ����뵥��ֵ
                    //if (tbxComValue.Text.IndexOf(" ") != -1)
                    //{
                    //    MessageBox.Show("����ֵ����");
                    //    return;
                    //}
                    // ����д�뵥���Ĵ����ı���
                    try
                    {
                        string str = null;
                        if (cbxConCom.SelectedItem.ToString() == "д�뵥ֵ") {
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
                        MessageBox.Show("����ֵֻ����1��0����true��false");
                        return;
                    }
                }
                else
                {
                    // ��ֵ
                    string tbxComValueStr = tbxComValue.Text;
                    
                    bool dr = false; // С��
                    int dp = 0;
                    string output = "";
                    string tbxComValueStrTemp = "";
                    bool pon = false; // ����
                    // �ж�����
                    if (tbxComValueStr.IndexOf("-") == -1) {
                        // �����ڸ���
                    }
                    else
                    {
                        // ȥ����
                        tbxComValueStr = tbxComValueStr.Replace("-","");


                        pon = true;
                    }
                    //MessageBox.Show(tbxComValueStr);return;
                    // �ж�С��
                    if(tbxComValueStr.IndexOf(".") == -1)
                    {
                        // ������С�� 
                        tbxComValueStrTemp = tbxComValueStr;

                    }
                    else
                    {
                        // ȡС����
                        tbxComValueStrTemp = tbxComValueStr.Replace(".", "");
                        // ��ѯС����ڼ�λ
                        dp = String.Join("", tbxComValueStr.Reverse()).IndexOf(".");
                        //MessageBox.Show(dp.ToString());

                        dr = true;
                    }
                    // ת�޷������� ת�ַ��ֽڴ�
                    int tbxComValueInt = int.Parse(tbxComValueStrTemp);
                    byte[] b_temp = BitConverter.GetBytes(tbxComValueInt).ToArray();
                    byte[] b_temps = FormatConversion.bytesTrimEnd(b_temp);
                    byte[] b_tempss = b_temps.Reverse().ToArray();
                    //byte[] noStr = System.Text.Encoding.ASCII.GetBytes(tbxComValueStrTemp);


                    List<byte> temp = new List<byte>();

                    // ƴ������
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
                        //�����͵ı�����ʾ�ڴ�����
                        string msg7 = "";
                        for (int i = 0; i < message7.Length; i++)
                        {
                            msg7 += message7[i].ToString("X2") + " ";
                        }
                        msg7 += "\r\n";
                        rbxSendMsg.AppendText(msg7);
                        // ƴ������
                        // ���� ����ָ��
                        Thread.Sleep(200);
                        byte[] messagetemp = MessageGenerationModule.MessageGeneration(station, WriteMode.Write03, 17, short.Parse("1"));
                        //���ͱ���
                        conHelper.SendDataMethod(messagetemp);
                        //�����͵ı�����ʾ�ڴ�����
                        string msgStrtmp = "";
                        for (int i = 0; i < messagetemp.Length; i++)
                        {
                            msgStrtmp += messagetemp[i].ToString("X2") + " ";
                        }
                        msgStrtmp += "\r\n";
                        rbxSendMsg.AppendText(msgStrtmp);
                        if (dr)
                        {
                            // ����С��λ
                            Thread.Sleep(200);
                            byte[] message16 = MessageGenerationModule.MessageGeneration(station, WriteMode.Write03, 16, short.Parse(dp.ToString()));
                            //���ͱ���
                            conHelper.SendDataMethod(message16);
                            //�����͵ı�����ʾ�ڴ�����
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
                        // ƴ��С��            
                        temp.Add((byte)dp);
                        //rbxSendMsg.AppendText(output);
                    }
                    else {
                        // ƴ�� 00 �޷���ƴ��
                        temp.Add((byte)0);
                    }
                    // ƴ�Ӳ�0
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
                        MessageBox.Show("��������");
                        return;
                    }

                }
            }
            else
            {
                // ���ͱ���ǰ�� ģʽѡ��
                ReadMode mode = (ReadMode)readWriteMode;
                message = MessageGenerationModule.GetReadMessage(station, mode, startAdr, count);
            }
            //���ͱ���
            conHelper.SendDataMethod(message);
            //�����͵ı�����ʾ�ڴ�����
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
            //������ʱ�ֽ��б�
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