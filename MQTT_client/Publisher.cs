﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NetMQ;
using NetMQ.Sockets;



namespace Madam.Common.Box.MQTT
{
    public class Publisher : IDisposable
    {
        private PublisherSocket PubSocket { get; }
        
        public Publisher()
        {
            PubSocket = new PublisherSocket(">tcp://localhost:5678");

        }
        
        public void SendSingleFrame(string topic, string msg)
        {
            

            var frame = topic + "msg :" + msg;
            PubSocket.SendFrame(topic, true);
            PubSocket.SendFrame(frame);

        }

        public NetMQMessage CreateMqMessage(string topic, string method, object obj)
        {

            NetMQMessage message = new NetMQMessage();
            byte[] data = ToBinary(obj);
            message.Append(topic);
            message.Append(method);
            message.Append(data);
            return message;
        }

        public NetMQMessage PrepareSingleObject(string topic, object obj)
        {
            //on va mettre en premiere frame le topic, le reste sera les infos necessaires
            NetMQMessage message = new NetMQMessage();
            byte[] data = ToBinary(obj);
            message.Append(topic);
            message.Append(data);
            return message;

        }

        public NetMQMessage AddStringToMessage(NetMQMessage msg, string str)
        {

            msg.Append(str);
            return msg;
        }


        public NetMQMessage AddObjectToMessage(NetMQMessage msg, Object obj)
        {
            byte[] data = ToBinary(obj);
            msg.Append(data);
            return msg;
        }

        public void SendMessage(NetMQMessage msg)
        {
            PubSocket.SendMultipartMessage(msg);
         
        }


        public void Dispose()
        {
            PubSocket.Dispose();
        }
        

    }
}