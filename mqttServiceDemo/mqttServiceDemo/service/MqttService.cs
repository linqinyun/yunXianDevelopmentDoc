﻿using MQTTnet.Protocol;
using MQTTnet.Server;
using MQTTnet;
using System.Text;

namespace mqttServiceDemo.service
{
    public class MqttService
    {
        public static MqttServer _mqttServer { get; set; }

        public static void PublishData(string data)
        {
            var message = new MqttApplicationMessage
            {
                Topic = "topic1",
                Payload = Encoding.Default.GetBytes(data),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = false  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
            };

            _mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(message) // 发送消息给有订阅 topic_01的客户端
            {
                SenderClientId = "Server_01"
            }).GetAwaiter().GetResult();
        }
    }
}
