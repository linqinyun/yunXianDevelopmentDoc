#include "uart.h"

uint8 idata SER_BUFFER[64]; 
uint8 idata SER_BUFFER1[10]; 


/*********************************************************************************************************
** 函数名称:        uchar Send_Data(void)
** 功能描述: 将数据缓冲区的数据发送给TX模块
             定时器2作为串行口看门狗定时器。发送STX后，启动定时器2为10毫秒溢出，
             若定时器2溢出(10ms没有接收到TX模块的ACK，则本次数据链建立失败。
**
** 输　入: 
**         
**
** 输　出: 无
**
** 函数返回:错误类型
**           
** 全局变量: 无
********************************************************************************************************/


uint8 Send_Data(void)
{
	uint8 send_len;              //发送数据长度
	uint8 i;
  P15 = 1;
	ES=0;                        //关闭串行I／O中断
	REN=0;               //串口不允许接收
  TI = 0;
	SBUF=STX;            //将起始符STX放到串口数据缓冲器
	while(TI==0);        //查询串口发送中断标志位TI,判断是否发送STX完成
	TI=0;                //清发送中断标志位

	send_len=SER_BUFFER[LENGTH]+5;  //因为除数据之外还有SeqNr,Command,Len,BCC以及ETX需要发送   
	                                //所以发送长度要在数据长度LENGTH的基础上加5
	SER_BUFFER[send_len-2]=0;       //倒数第2的发送缓冲区为BCC，清0
	SER_BUFFER[send_len-1]=ETX;     //发送缓冲区的最末为结束符ETX

    //Delay_50us(2);
	for(i=0; i < send_len; i++)//循环发送缓冲区的数据到TX模块
	{
		SBUF=SER_BUFFER[i];
		if(i < (send_len-2))   //如果不到BCC校验，将之前所发送数据进行异或
			SER_BUFFER[send_len-2]^=SER_BUFFER[i];
		if(i==send_len-3)    //到BCC校验，将之前所有异或的数据再取反得到BCC校验数据
			SER_BUFFER[send_len-2]=~SER_BUFFER[send_len-2];
		while(TI==0);               //查询是否发送一个字节完毕
		TI=0;
	}
//	CON_485=0;
	REN=1;                         //串口允许接收
	ES=1;                          //打开串行I／O中断
  P15 = 0;	
	return	COMM_OK;               //返回COMM_OK
}

//========================================================================
// 函数: SetTimer2Baudraye(u16 dat)
// 描述: 设置Timer2做波特率发生器。
// 参数: dat: Timer2的重装值.
// 返回: none.
// 版本: VER1.0
// 日期: 2014-11-28
// 备注: 
//========================================================================
//void SetTimer2Baudraye(u16 dat)  // 使用Timer2做波特率.
//{
//    AUXR &= ~(1<<4);    //Timer stop
//    AUXR &= ~(1<<3);    //Timer2 set As Timer
//    AUXR |=  (1<<2);    //Timer2 set as 1T mode
//    T2H = dat / 256;
//    T2L = dat % 256;
//    IE2  &= ~(1<<2);    //禁止中断
//    AUXR |=  (1<<4);    //Timer run enable
//}


//========================================================================
// 函数: void   UART1_config(u8 brt)
// 描述: UART1初始化函数。
// 参数: brt: 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
// 返回: none.
// 版本: VER1.0
// 日期: 2014-11-28
// 备注: 
//========================================================================
void UART1_timer1_config(uint32_t Baudrate1,uint8 cport)    // 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
{
	/*********** 波特率使用定时器1 *****************/
		TR1 = 0;
		AUXR &= ~0x01;		//S1 BRT Use Timer1;
		AUXR |=  (1<<6);	//Timer1 set as 1T mode
		TMOD &= ~(1<<6);	//Timer1 set As Timer
		TMOD &= ~0x30;		//Timer1_16bitAutoReload;
		TH1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) / 256);
		TL1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) % 256);
		ET1 = 0;	//禁止中断
		INT_CLKO &= ~0x02;	//不输出时钟
		TR1  = 1;
	/*************************************************/

    SCON = (SCON & 0x3f) | 0x40;    //UART1模式, 0x00: 同步移位输出, 0x40: 8位数据,可变波特率, 0x80: 9位数据,固定波特率, 0xc0: 9位数据,可变波特率
    ES  = 1;    //允许中断
    REN = 1;    //允许接收
    P_SW1 &= 0x3f;
    P_SW1 |= cport;      //UART1 switch to, 0x00: P3.0 P3.1, 0x40: P3.6 P3.7, 0x80: P1.6 P1.7, 0xC0: P4.3 P4.4
    EA=1;
}


/*********************************************************************************************************
** 函数名称:        void Serial_Int(void)
** 功能描述: 串口中断函数.串口接收到数据(STX)后，先发送ACK信号，然后再继续接收全部数据.以后启动定时器2为
             10毫秒溢出，若10毫秒内，未接收到下一个字节，则本次接收数据失败。
**
** 输　入: 
**         
**
** 输　出: 无
**
** 函数返回: 无
**           
** 全局变量: 无
********************************************************************************************************/

void SerialPort1_ISR(void) interrupt 4 //using 1 
{
	unsigned long delay = 0;
	uint8 i;
	uint8 index;
	uint8 BCC_sum=0;
	uint8 rcv_len=5;  //因为除数据之外还有SeqNr,Command,Len,BCC以及ETX   
	                  //所以接收长度要在数据长度LENGTH的基础上加5 				  	
  P25 = 0;
	ES=0;             //关闭串行I／O中断
	RI=0;      //清接收中断标志位
	if (SBUF != STX) 
	{	
		ES=1;
		return;			// 判断是否为帧头，
	}		
	
  for(index=0;index<rcv_len;index++)//循环接收TX模块发送的数据块
	{
		while((RI == 0)&&(delay < 200000))delay++;//判断接收到数据或超时
	  if(delay>199995)  	
		{
		  delay= 0;
			RI=0;
			for(i=0;i<64;i++) SER_BUFFER[i]=0;
			ES=1;
			return;			
		}

		if(RI == 1)            //如果是接收到数据而退出while循环
		{	
			RI=0;       //清接收中断标志
			delay= 0;
			SER_BUFFER[index]=SBUF;//将接收到的数据依次保存到数据缓冲区
			if(index==LENGTH)
			{	
			     rcv_len+=SER_BUFFER[index];//将接收的TX模块发送过来的数据长度值加上5
			}
			if(index<rcv_len-2)	
			{
			     BCC_sum^=SER_BUFFER[index];//计算数据块的BCC校验
			}	
		}
	} 
	
	if(index==rcv_len)       //判断接收到的数据长度是否正确
	{
		if(BCC_sum!=~SER_BUFFER[rcv_len-2]) //如果接收数据正确，则判断BCC校验是否正确
		{
		
			for(i=0;i<64;i++) SER_BUFFER[i]=0;
			ES=1;
			return;   //校验BCC
		}
		if(SER_BUFFER[rcv_len-1]!=ETX)	//如果BCC校验正确，最后判断是否有结束符ETX
		{
			
			for(i=0;i<64;i++) SER_BUFFER[i]=0;
			ES=1;
			return;
		}
	}
	else	
	{
		for(i=0;i<64;i++) SER_BUFFER[i]=0;
		ES=1;
		return;
	}
	ES=1;
}



