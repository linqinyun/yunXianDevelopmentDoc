/*---------------------------------------------------------------------*/
/* --- STC MCU Limited ------------------------------------------------*/
/* --- STC15F4K60S4 系列 定时器2用作串口1的波特率发生器举例------------*/
/* --- Mobile: (86)13922805190 ----------------------------------------*/
/* --- Fax: 86-0513-55012956,55012947,55012969 ------------------------*/
/* --- Tel: 86-0513-55012928,55012929,55012966-------------------------*/
/* --- Web: www.STCMCU.com --------------------------------------------*/
/* --- Web: www.GXWMCU.com --------------------------------------------*/
/* 如果要在程序中使用此代码,请在程序中注明使用了STC的资料及程序        */
/* 如果要在文章中应用此代码,请在文章中注明使用了STC的资料及程序        */
/*---------------------------------------------------------------------*/ 

//本示例在Keil开发环境下请选择Intel的8058芯片型号进行编译
//若无特别说明,工作频率一般为11.0592MHz
//                           18.432000MHZ

#include "intrins.h"
#include "uart.h"

void init(void)
{
	
    P0M0 = 0x00;
    P0M1 = 0x00;
    P1M0 = 0x00;
    P1M1 = 0x00;
    P2M0 = 0x00;
    P2M1 = 0x00;
    P3M0 = 0x00;
    P3M1 = 0x00;
    P4M0 = 0x00;
    P4M1 = 0x00;
    P5M0 = 0x00;
    P5M1 = 0x00;
    P6M0 = 0x00;
    P6M1 = 0x00;
    P7M0 = 0x00;
    P7M1 = 0x00;
}

////============================================================
////写卡-以块为单位，每个块十六个字节（或加密）
////============================================================

uint8 card_write_block(void)
{
	uint8 status = 0,i;
	uint32 delay = 0;
	SER_BUFFER[SEQNR] = 0x00;    //地址默认为00
	SER_BUFFER[COMMAND] = 0x23; //表示写块指令
	SER_BUFFER[LENGTH] = 0x18;  // 写块指令中携带的有效数据24个字节
	SER_BUFFER[DATA] = 0X00;    //表示采用密码A校验卡片密码
	for(i=0;i<6;i++) SER_BUFFER[DATA+1+i] = 0XFF;   //表示采用默认密码6个FF校验卡片密码
	SER_BUFFER[DATA+7] = 0X02;    //表示将后面的十六个字节写入到第2块
	for(i=0;i<16;i++) SER_BUFFER[DATA+8+i] = i; //表示将0x00~0x0F十六个数据写入到第2块
	Send_Data(); //发送写块的数据
	SER_BUFFER[COMMAND] = 0x00;
	while((SER_BUFFER[COMMAND] == 0x00)&&(delay<200000))delay++;   //delay为了防止死机
	if(SER_BUFFER[COMMAND] == 0x23) status = SER_BUFFER[DATA]; //将写卡的反馈状态进行返回
	return status;
}

////============================================================
////写卡-以块为单位，每个块十六个字节（或加密）
////============================================================

uint8 card_read_block(void)
{
	uint8 status = 0,i;
	uint32 delay = 0;
	SER_BUFFER[SEQNR] = 0x00;    //地址默认为00
	SER_BUFFER[COMMAND] = 0x22; //表示写块指令
	SER_BUFFER[LENGTH] = 0x08;  // 写块指令中携带的有效数据24个字节
	SER_BUFFER[DATA] = 0X00;    //表示采用密码A校验卡片密码
	for(i=0;i<6;i++) SER_BUFFER[DATA+1+i] = 0XFF;   //表示采用默认密码6个FF校验卡片密码
	SER_BUFFER[DATA+7] = 0X02;    //表示将后面的十六个字节写入到第2块
	Send_Data(); //发送写块的数据
	SER_BUFFER[COMMAND] = 0x00;
	while((SER_BUFFER[COMMAND] == 0x00)&&(delay<200000))delay++;   //delay为了防止死机
	if(SER_BUFFER[COMMAND] == 0x22) status = SER_BUFFER[DATA]; //将写卡的反馈状态进行返回
	return status;
}

void main(void)
{
	init();  				 //初始化IO口
  UART1_timer1_config(9600,0x40);	 //默认初始化成和读卡器对接的IO口
	while(1)
	{
		
		if(SER_BUFFER[LENGTH] == 0x08)   //表示有卡片
		{
		  P11 = 0;
			UART1_timer1_config(9600,0x00);	   //切换成对外的串口IO口
			card_write_block();
			SER_BUFFER[LENGTH] = 0;
			UART1_timer1_config(9600,0x40);
		}		
	}
}
