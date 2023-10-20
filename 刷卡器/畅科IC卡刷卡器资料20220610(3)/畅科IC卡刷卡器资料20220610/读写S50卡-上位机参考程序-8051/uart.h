/****************************************Copyright (c)**************************************************
**                                      TXRFID
**                                    
**                                 http://www.txrfid.com
**
**--------------文件信息-------------------------------------------------------------------------
**文   件   名: TX_B.H    
**创   建   人: TXRFID
**创 建 日  期: 2008年1月5日
**描        述: TX500B的C文件。
**
**---------------------------------------------------------------------------------------
**-----------------修改记录--------------------------------------------------------------
** 修改内容:    1.

** 当前版本:    v1.0 
** 修 改 人:    TXRFID
** 修改日期:    2009年02月05日
** 注    意: 
**---------------------------------------------------------------------------------------
**
**-----------------------------------------------------------------------------------------
****************************************************************************************/
#ifndef __TX_B_H__                    // 防止头文件被重复包含                                                          
#define __TX_B_H__

#ifdef TX_B_GLOBALS                   // 注意在TX_B.c 文件中定义TX_GLOBALS
	#define TX_EXT
#else
	#define TX_EXT extern
#endif


#include "sys.h"

//Communication Error
#define COMM_OK                 0x00      //命令调用成功
#define COMM_ERR                0xff      //串行通信错误

//定义通信帧常量
#define STX                     0x20       //开始符
#define ACK                     0x06       //应答
#define NAK                     0x15       //NAK
#define ETX                     0x03       //终止符

//定义数据块格式的位置
#define SEQNR                   0          //数据交换包的序号
#define COMMAND                 1          //命令字符  
#define STATUS                  1          //状态字符
#define LENGTH                  2          //数据的长度        
#define DATA                    3          //数据字节

extern uint8 idata SER_BUFFER[64]; 
extern uint8 idata SER_BUFFER1[10]; 
void UART1_timer1_config(uint32_t Baudrate1,uint8 cport);
uint8 Send_Data(void);
uint8 Send_Data1(void);
void UART2_timer2_config(uint32_t Baudrate1);


#endif              // __TX_B_H__
