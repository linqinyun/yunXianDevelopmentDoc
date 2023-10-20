#include "uart.h"

uint8 idata SER_BUFFER[64]; 
uint8 idata SER_BUFFER1[10]; 


/*********************************************************************************************************
** ��������:        uchar Send_Data(void)
** ��������: �����ݻ����������ݷ��͸�TXģ��
             ��ʱ��2��Ϊ���пڿ��Ź���ʱ��������STX��������ʱ��2Ϊ10���������
             ����ʱ��2���(10msû�н��յ�TXģ���ACK���򱾴�����������ʧ�ܡ�
**
** �䡡��: 
**         
**
** �䡡��: ��
**
** ��������:��������
**           
** ȫ�ֱ���: ��
********************************************************************************************************/


uint8 Send_Data(void)
{
	uint8 send_len;              //�������ݳ���
	uint8 i;
  P15 = 1;
	ES=0;                        //�رմ���I��O�ж�
	REN=0;               //���ڲ��������
  TI = 0;
	SBUF=STX;            //����ʼ��STX�ŵ��������ݻ�����
	while(TI==0);        //��ѯ���ڷ����жϱ�־λTI,�ж��Ƿ���STX���
	TI=0;                //�巢���жϱ�־λ

	send_len=SER_BUFFER[LENGTH]+5;  //��Ϊ������֮�⻹��SeqNr,Command,Len,BCC�Լ�ETX��Ҫ����   
	                                //���Է��ͳ���Ҫ�����ݳ���LENGTH�Ļ����ϼ�5
	SER_BUFFER[send_len-2]=0;       //������2�ķ��ͻ�����ΪBCC����0
	SER_BUFFER[send_len-1]=ETX;     //���ͻ���������ĩΪ������ETX

    //Delay_50us(2);
	for(i=0; i < send_len; i++)//ѭ�����ͻ����������ݵ�TXģ��
	{
		SBUF=SER_BUFFER[i];
		if(i < (send_len-2))   //�������BCCУ�飬��֮ǰ���������ݽ������
			SER_BUFFER[send_len-2]^=SER_BUFFER[i];
		if(i==send_len-3)    //��BCCУ�飬��֮ǰ��������������ȡ���õ�BCCУ������
			SER_BUFFER[send_len-2]=~SER_BUFFER[send_len-2];
		while(TI==0);               //��ѯ�Ƿ���һ���ֽ����
		TI=0;
	}
//	CON_485=0;
	REN=1;                         //�����������
	ES=1;                          //�򿪴���I��O�ж�
  P15 = 0;	
	return	COMM_OK;               //����COMM_OK
}

//========================================================================
// ����: SetTimer2Baudraye(u16 dat)
// ����: ����Timer2�������ʷ�������
// ����: dat: Timer2����װֵ.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
//void SetTimer2Baudraye(u16 dat)  // ʹ��Timer2��������.
//{
//    AUXR &= ~(1<<4);    //Timer stop
//    AUXR &= ~(1<<3);    //Timer2 set As Timer
//    AUXR |=  (1<<2);    //Timer2 set as 1T mode
//    T2H = dat / 256;
//    T2L = dat % 256;
//    IE2  &= ~(1<<2);    //��ֹ�ж�
//    AUXR |=  (1<<4);    //Timer run enable
//}


//========================================================================
// ����: void   UART1_config(u8 brt)
// ����: UART1��ʼ��������
// ����: brt: ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
void UART1_timer1_config(uint32_t Baudrate1,uint8 cport)    // ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
{
	/*********** ������ʹ�ö�ʱ��1 *****************/
		TR1 = 0;
		AUXR &= ~0x01;		//S1 BRT Use Timer1;
		AUXR |=  (1<<6);	//Timer1 set as 1T mode
		TMOD &= ~(1<<6);	//Timer1 set As Timer
		TMOD &= ~0x30;		//Timer1_16bitAutoReload;
		TH1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) / 256);
		TL1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) % 256);
		ET1 = 0;	//��ֹ�ж�
		INT_CLKO &= ~0x02;	//�����ʱ��
		TR1  = 1;
	/*************************************************/

    SCON = (SCON & 0x3f) | 0x40;    //UART1ģʽ, 0x00: ͬ����λ���, 0x40: 8λ����,�ɱ䲨����, 0x80: 9λ����,�̶�������, 0xc0: 9λ����,�ɱ䲨����
    ES  = 1;    //�����ж�
    REN = 1;    //�������
    P_SW1 &= 0x3f;
    P_SW1 |= cport;      //UART1 switch to, 0x00: P3.0 P3.1, 0x40: P3.6 P3.7, 0x80: P1.6 P1.7, 0xC0: P4.3 P4.4
    EA=1;
}


/*********************************************************************************************************
** ��������:        void Serial_Int(void)
** ��������: �����жϺ���.���ڽ��յ�����(STX)���ȷ���ACK�źţ�Ȼ���ټ�������ȫ������.�Ժ�������ʱ��2Ϊ
             10�����������10�����ڣ�δ���յ���һ���ֽڣ��򱾴ν�������ʧ�ܡ�
**
** �䡡��: 
**         
**
** �䡡��: ��
**
** ��������: ��
**           
** ȫ�ֱ���: ��
********************************************************************************************************/

void SerialPort1_ISR(void) interrupt 4 //using 1 
{
	unsigned long delay = 0;
	uint8 i;
	uint8 index;
	uint8 BCC_sum=0;
	uint8 rcv_len=5;  //��Ϊ������֮�⻹��SeqNr,Command,Len,BCC�Լ�ETX   
	                  //���Խ��ճ���Ҫ�����ݳ���LENGTH�Ļ����ϼ�5 				  	
  P25 = 0;
	ES=0;             //�رմ���I��O�ж�
	RI=0;      //������жϱ�־λ
	if (SBUF != STX) 
	{	
		ES=1;
		return;			// �ж��Ƿ�Ϊ֡ͷ��
	}		
	
  for(index=0;index<rcv_len;index++)//ѭ������TXģ�鷢�͵����ݿ�
	{
		while((RI == 0)&&(delay < 200000))delay++;//�жϽ��յ����ݻ�ʱ
	  if(delay>199995)  	
		{
		  delay= 0;
			RI=0;
			for(i=0;i<64;i++) SER_BUFFER[i]=0;
			ES=1;
			return;			
		}

		if(RI == 1)            //����ǽ��յ����ݶ��˳�whileѭ��
		{	
			RI=0;       //������жϱ�־
			delay= 0;
			SER_BUFFER[index]=SBUF;//�����յ����������α��浽���ݻ�����
			if(index==LENGTH)
			{	
			     rcv_len+=SER_BUFFER[index];//�����յ�TXģ�鷢�͹��������ݳ���ֵ����5
			}
			if(index<rcv_len-2)	
			{
			     BCC_sum^=SER_BUFFER[index];//�������ݿ��BCCУ��
			}	
		}
	} 
	
	if(index==rcv_len)       //�жϽ��յ������ݳ����Ƿ���ȷ
	{
		if(BCC_sum!=~SER_BUFFER[rcv_len-2]) //�������������ȷ�����ж�BCCУ���Ƿ���ȷ
		{
		
			for(i=0;i<64;i++) SER_BUFFER[i]=0;
			ES=1;
			return;   //У��BCC
		}
		if(SER_BUFFER[rcv_len-1]!=ETX)	//���BCCУ����ȷ������ж��Ƿ��н�����ETX
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



