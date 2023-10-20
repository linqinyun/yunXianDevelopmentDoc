/****************************************Copyright (c)**************************************************
**                                      TXRFID
**                                    
**                                 http://www.txrfid.com
**
**--------------�ļ���Ϣ-------------------------------------------------------------------------
**��   ��   ��: TX_B.H    
**��   ��   ��: TXRFID
**�� �� ��  ��: 2008��1��5��
**��        ��: TX500B��C�ļ���
**
**---------------------------------------------------------------------------------------
**-----------------�޸ļ�¼--------------------------------------------------------------
** �޸�����:    1.

** ��ǰ�汾:    v1.0 
** �� �� ��:    TXRFID
** �޸�����:    2009��02��05��
** ע    ��: 
**---------------------------------------------------------------------------------------
**
**-----------------------------------------------------------------------------------------
****************************************************************************************/
#ifndef __TX_B_H__                    // ��ֹͷ�ļ����ظ�����                                                          
#define __TX_B_H__

#ifdef TX_B_GLOBALS                   // ע����TX_B.c �ļ��ж���TX_GLOBALS
	#define TX_EXT
#else
	#define TX_EXT extern
#endif


#include "sys.h"

//Communication Error
#define COMM_OK                 0x00      //������óɹ�
#define COMM_ERR                0xff      //����ͨ�Ŵ���

//����ͨ��֡����
#define STX                     0x20       //��ʼ��
#define ACK                     0x06       //Ӧ��
#define NAK                     0x15       //NAK
#define ETX                     0x03       //��ֹ��

//�������ݿ��ʽ��λ��
#define SEQNR                   0          //���ݽ����������
#define COMMAND                 1          //�����ַ�  
#define STATUS                  1          //״̬�ַ�
#define LENGTH                  2          //���ݵĳ���        
#define DATA                    3          //�����ֽ�

extern uint8 idata SER_BUFFER[64]; 
extern uint8 idata SER_BUFFER1[10]; 
void UART1_timer1_config(uint32_t Baudrate1,uint8 cport);
uint8 Send_Data(void);
uint8 Send_Data1(void);
void UART2_timer2_config(uint32_t Baudrate1);


#endif              // __TX_B_H__
