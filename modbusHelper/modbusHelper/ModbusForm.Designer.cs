namespace modbusHelper
{
    partial class ModbusForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCon = new System.Windows.Forms.Button();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.tbxStatus = new System.Windows.Forms.TextBox();
            this.cbxPort = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnRW = new System.Windows.Forms.Button();
            this.tbxValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nudAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudStation = new System.Windows.Forms.TextBox();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.responesBtn = new System.Windows.Forms.Button();
            this.requestBtn = new System.Windows.Forms.Button();
            this.rbxRecMsg = new System.Windows.Forms.TextBox();
            this.rbxSendMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCRC = new System.Windows.Forms.Button();
            this.btnNoCRC = new System.Windows.Forms.Button();
            this.tbxWill = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnInit = new System.Windows.Forms.Button();
            this.tbxComNum = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnComRW = new System.Windows.Forms.Button();
            this.tbxComValue = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxComAddr = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbxConCom = new System.Windows.Forms.ComboBox();
            this.tbxDnum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lbTip = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnCon);
            this.groupBox1.Controls.Add(this.cbxStopBits);
            this.groupBox1.Controls.Add(this.cbxDataBits);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.tbxStatus);
            this.groupBox1.Controls.Add(this.cbxPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(299, 614);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(24, 511);
            this.btnScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(112, 34);
            this.btnScan.TabIndex = 10;
            this.btnScan.Text = "扫描端口";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 428);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 9;
            this.label8.Text = "停止位";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 350);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 8;
            this.label7.Text = "数据位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 282);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 24);
            this.label6.TabIndex = 7;
            this.label6.Text = "校验位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 206);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "波特率";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "连接状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "可用串口";
            // 
            // btnCon
            // 
            this.btnCon.Location = new System.Drawing.Point(170, 511);
            this.btnCon.Margin = new System.Windows.Forms.Padding(2);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(112, 34);
            this.btnCon.TabIndex = 0;
            this.btnCon.Text = "打开连接";
            this.btnCon.UseVisualStyleBackColor = true;
            this.btnCon.Click += new System.EventHandler(this.btnCon_Click);
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(100, 425);
            this.cbxStopBits.Margin = new System.Windows.Forms.Padding(2);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(182, 32);
            this.cbxStopBits.TabIndex = 4;
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(100, 348);
            this.cbxDataBits.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(182, 32);
            this.cbxDataBits.TabIndex = 3;
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(100, 278);
            this.cbxParity.Margin = new System.Windows.Forms.Padding(2);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(182, 32);
            this.cbxParity.TabIndex = 2;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(100, 204);
            this.cbxBaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(182, 32);
            this.cbxBaudRate.TabIndex = 1;
            // 
            // tbxStatus
            // 
            this.tbxStatus.Enabled = false;
            this.tbxStatus.Location = new System.Drawing.Point(100, 133);
            this.tbxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.tbxStatus.Name = "tbxStatus";
            this.tbxStatus.Size = new System.Drawing.Size(182, 30);
            this.tbxStatus.TabIndex = 0;
            // 
            // cbxPort
            // 
            this.cbxPort.FormattingEnabled = true;
            this.cbxPort.Location = new System.Drawing.Point(100, 72);
            this.cbxPort.Margin = new System.Windows.Forms.Padding(2);
            this.cbxPort.Name = "cbxPort";
            this.cbxPort.Size = new System.Drawing.Size(182, 32);
            this.cbxPort.TabIndex = 0;
            this.cbxPort.SelectedIndexChanged += new System.EventHandler(this.cbxPort_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudCount);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnRW);
            this.groupBox2.Controls.Add(this.tbxValue);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.nudAddress);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.nudStation);
            this.groupBox2.Controls.Add(this.cbxMode);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(318, 12);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1109, 196);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读写";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(826, 70);
            this.nudCount.Margin = new System.Windows.Forms.Padding(2);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(114, 30);
            this.nudCount.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(720, 72);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 24);
            this.label13.TabIndex = 16;
            this.label13.Text = "寄存器个数";
            // 
            // btnRW
            // 
            this.btnRW.Location = new System.Drawing.Point(782, 137);
            this.btnRW.Margin = new System.Windows.Forms.Padding(2);
            this.btnRW.Name = "btnRW";
            this.btnRW.Size = new System.Drawing.Size(158, 34);
            this.btnRW.TabIndex = 10;
            this.btnRW.Text = "读取";
            this.btnRW.UseVisualStyleBackColor = true;
            this.btnRW.Click += new System.EventHandler(this.btnRW_Click);
            // 
            // tbxValue
            // 
            this.tbxValue.Location = new System.Drawing.Point(125, 139);
            this.tbxValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbxValue.Name = "tbxValue";
            this.tbxValue.Size = new System.Drawing.Size(616, 30);
            this.tbxValue.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 139);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 24);
            this.label12.TabIndex = 14;
            this.label12.Text = "寄存器值";
            // 
            // nudAddress
            // 
            this.nudAddress.Location = new System.Drawing.Point(614, 70);
            this.nudAddress.Margin = new System.Windows.Forms.Padding(2);
            this.nudAddress.Name = "nudAddress";
            this.nudAddress.Size = new System.Drawing.Size(90, 30);
            this.nudAddress.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(507, 72);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 24);
            this.label11.TabIndex = 12;
            this.label11.Text = "寄存器地址";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(325, 72);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 24);
            this.label10.TabIndex = 11;
            this.label10.Text = "485地址";
            // 
            // nudStation
            // 
            this.nudStation.Location = new System.Drawing.Point(409, 70);
            this.nudStation.Margin = new System.Windows.Forms.Padding(2);
            this.nudStation.Name = "nudStation";
            this.nudStation.Size = new System.Drawing.Size(91, 30);
            this.nudStation.TabIndex = 10;
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Location = new System.Drawing.Point(125, 67);
            this.cbxMode.Margin = new System.Windows.Forms.Padding(2);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(182, 32);
            this.cbxMode.TabIndex = 10;
            this.cbxMode.SelectedIndexChanged += new System.EventHandler(this.cbxMode_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 72);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 24);
            this.label9.TabIndex = 10;
            this.label9.Text = "指令模式";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.responesBtn);
            this.groupBox3.Controls.Add(this.requestBtn);
            this.groupBox3.Controls.Add(this.rbxRecMsg);
            this.groupBox3.Controls.Add(this.rbxSendMsg);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(318, 290);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1109, 336);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "报文";
            // 
            // responesBtn
            // 
            this.responesBtn.Location = new System.Drawing.Point(980, 29);
            this.responesBtn.Margin = new System.Windows.Forms.Padding(2);
            this.responesBtn.Name = "responesBtn";
            this.responesBtn.Size = new System.Drawing.Size(112, 34);
            this.responesBtn.TabIndex = 5;
            this.responesBtn.Text = "清空数据";
            this.responesBtn.UseVisualStyleBackColor = true;
            this.responesBtn.Click += new System.EventHandler(this.responesBtn_Click);
            // 
            // requestBtn
            // 
            this.requestBtn.Location = new System.Drawing.Point(409, 29);
            this.requestBtn.Margin = new System.Windows.Forms.Padding(2);
            this.requestBtn.Name = "requestBtn";
            this.requestBtn.Size = new System.Drawing.Size(112, 34);
            this.requestBtn.TabIndex = 4;
            this.requestBtn.Text = "清空数据";
            this.requestBtn.UseVisualStyleBackColor = true;
            this.requestBtn.Click += new System.EventHandler(this.requestBtn_Click);
            // 
            // rbxRecMsg
            // 
            this.rbxRecMsg.Location = new System.Drawing.Point(589, 70);
            this.rbxRecMsg.Margin = new System.Windows.Forms.Padding(2);
            this.rbxRecMsg.Multiline = true;
            this.rbxRecMsg.Name = "rbxRecMsg";
            this.rbxRecMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rbxRecMsg.Size = new System.Drawing.Size(503, 251);
            this.rbxRecMsg.TabIndex = 3;
            this.rbxRecMsg.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // rbxSendMsg
            // 
            this.rbxSendMsg.Location = new System.Drawing.Point(20, 70);
            this.rbxSendMsg.Margin = new System.Windows.Forms.Padding(2);
            this.rbxSendMsg.Multiline = true;
            this.rbxSendMsg.Name = "rbxSendMsg";
            this.rbxSendMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rbxSendMsg.Size = new System.Drawing.Size(503, 251);
            this.rbxSendMsg.TabIndex = 2;
            this.rbxSendMsg.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "响应报文";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "请求报文";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCRC);
            this.groupBox4.Controls.Add(this.btnNoCRC);
            this.groupBox4.Controls.Add(this.tbxWill);
            this.groupBox4.Location = new System.Drawing.Point(318, 214);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(1109, 77);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "任意指令";
            // 
            // btnCRC
            // 
            this.btnCRC.Location = new System.Drawing.Point(840, 26);
            this.btnCRC.Margin = new System.Windows.Forms.Padding(2);
            this.btnCRC.Name = "btnCRC";
            this.btnCRC.Size = new System.Drawing.Size(158, 34);
            this.btnCRC.TabIndex = 17;
            this.btnCRC.Text = "CRC发送";
            this.btnCRC.UseVisualStyleBackColor = true;
            this.btnCRC.Click += new System.EventHandler(this.btnCRC_Click);
            // 
            // btnNoCRC
            // 
            this.btnNoCRC.Location = new System.Drawing.Point(676, 26);
            this.btnNoCRC.Margin = new System.Windows.Forms.Padding(2);
            this.btnNoCRC.Name = "btnNoCRC";
            this.btnNoCRC.Size = new System.Drawing.Size(158, 34);
            this.btnNoCRC.TabIndex = 16;
            this.btnNoCRC.Text = "发送";
            this.btnNoCRC.UseVisualStyleBackColor = true;
            this.btnNoCRC.Click += new System.EventHandler(this.btnNoCRC_Click);
            // 
            // tbxWill
            // 
            this.tbxWill.Location = new System.Drawing.Point(37, 29);
            this.tbxWill.Margin = new System.Windows.Forms.Padding(2);
            this.tbxWill.Name = "tbxWill";
            this.tbxWill.Size = new System.Drawing.Size(616, 30);
            this.tbxWill.TabIndex = 16;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbTip);
            this.groupBox5.Controls.Add(this.btnInit);
            this.groupBox5.Controls.Add(this.tbxComNum);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.btnComRW);
            this.groupBox5.Controls.Add(this.tbxComValue);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.tbxComAddr);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.cbxConCom);
            this.groupBox5.Controls.Add(this.tbxDnum);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(12, 634);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(1414, 178);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "快捷指令";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(24, 88);
            this.btnInit.Margin = new System.Windows.Forms.Padding(2);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(112, 34);
            this.btnInit.TabIndex = 29;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // tbxComNum
            // 
            this.tbxComNum.Enabled = false;
            this.tbxComNum.Location = new System.Drawing.Point(814, 43);
            this.tbxComNum.Margin = new System.Windows.Forms.Padding(2);
            this.tbxComNum.Name = "tbxComNum";
            this.tbxComNum.Size = new System.Drawing.Size(86, 30);
            this.tbxComNum.TabIndex = 26;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(725, 47);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 24);
            this.label15.TabIndex = 25;
            this.label15.Text = "寄存器数";
            // 
            // btnComRW
            // 
            this.btnComRW.Location = new System.Drawing.Point(1190, 41);
            this.btnComRW.Margin = new System.Windows.Forms.Padding(2);
            this.btnComRW.Name = "btnComRW";
            this.btnComRW.Size = new System.Drawing.Size(112, 34);
            this.btnComRW.TabIndex = 24;
            this.btnComRW.Text = "写入";
            this.btnComRW.UseVisualStyleBackColor = true;
            this.btnComRW.Click += new System.EventHandler(this.btnComRW_Click);
            // 
            // tbxComValue
            // 
            this.tbxComValue.Location = new System.Drawing.Point(959, 42);
            this.tbxComValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbxComValue.Name = "tbxComValue";
            this.tbxComValue.Size = new System.Drawing.Size(225, 30);
            this.tbxComValue.TabIndex = 23;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(925, 46);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 24);
            this.label19.TabIndex = 22;
            this.label19.Text = "值";
            // 
            // tbxComAddr
            // 
            this.tbxComAddr.Location = new System.Drawing.Point(618, 42);
            this.tbxComAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tbxComAddr.Name = "tbxComAddr";
            this.tbxComAddr.Size = new System.Drawing.Size(86, 30);
            this.tbxComAddr.TabIndex = 21;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(529, 46);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 24);
            this.label18.TabIndex = 20;
            this.label18.Text = "寄存地址";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(277, 46);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 24);
            this.label17.TabIndex = 19;
            this.label17.Text = "功能";
            // 
            // cbxConCom
            // 
            this.cbxConCom.FormattingEnabled = true;
            this.cbxConCom.Location = new System.Drawing.Point(330, 42);
            this.cbxConCom.Margin = new System.Windows.Forms.Padding(2);
            this.cbxConCom.Name = "cbxConCom";
            this.cbxConCom.Size = new System.Drawing.Size(180, 32);
            this.cbxConCom.TabIndex = 18;
            this.cbxConCom.SelectedIndexChanged += new System.EventHandler(this.cbxConCom_SelectedIndexChanged);
            // 
            // tbxDnum
            // 
            this.tbxDnum.Location = new System.Drawing.Point(165, 42);
            this.tbxDnum.Margin = new System.Windows.Forms.Padding(2);
            this.tbxDnum.Name = "tbxDnum";
            this.tbxDnum.Size = new System.Drawing.Size(86, 30);
            this.tbxDnum.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 46);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 24);
            this.label14.TabIndex = 10;
            this.label14.Text = "设备号485地址";
            // 
            // lbTip
            // 
            this.lbTip.AutoSize = true;
            this.lbTip.Location = new System.Drawing.Point(959, 16);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(0, 24);
            this.lbTip.TabIndex = 30;
            // 
            // ModbusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 823);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ModbusForm";
            this.Text = "Modebus助手_0.0.1";
            this.Load += new System.EventHandler(this.ModbusForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox rbxRecMsg;
        private TextBox rbxSendMsg;
        private Label label2;
        private Label label1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button btnCon;
        private ComboBox cbxStopBits;
        private ComboBox cbxDataBits;
        private ComboBox cbxParity;
        private ComboBox cbxBaudRate;
        private TextBox tbxStatus;
        private ComboBox cbxPort;
        private Button btnRW;
        private TextBox tbxValue;
        private Label label12;
        private TextBox nudAddress;
        private Label label11;
        private Label label10;
        private TextBox nudStation;
        private ComboBox cbxMode;
        private Label label9;
        private GroupBox groupBox4;
        private Button btnCRC;
        private Button btnNoCRC;
        private TextBox tbxWill;
        private TextBox nudCount;
        private Label label13;
        private Button responesBtn;
        private Button requestBtn;
        private GroupBox groupBox5;
        private TextBox tbxDnum;
        private Label label14;
        private Button btnScan;
        private TextBox tbxComNum;
        private Label label15;
        private Button btnComRW;
        private TextBox tbxComValue;
        private Label label19;
        private TextBox tbxComAddr;
        private Label label18;
        private Label label17;
        private ComboBox cbxConCom;
        private Button btnInit;
        private Label lbTip;
    }
}