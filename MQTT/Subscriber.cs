using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;


namespace MQTT
{
    class Subscriber
    {

        private SubscriberSocket SubSocket { get; }
        

        public Subscriber()
        {
            SubSocket = new SubscriberSocket(">tcp://localhost:1234");
           

        }
        IEnumerable<Type> GetSubclasses(Type type)
        {
            return type.Assembly.GetTypes().Where(t => t.IsSubclassOf(type));
        }

        public Task SubscribeTopic(string topic)
        {

            SubSocket.Options.ReceiveHighWatermark = 1000;
            SubSocket.Subscribe(topic);
            Console.WriteLine("Subscriber socket connecting...");
           
            while (true)
            {
                NetMQMessage messagereceived = SubSocket.ReceiveMultipartMessage();

                Console.WriteLine("MQTT", "TOPIC:" + messagereceived);
            }

        }

        public  Task Listen()
        {
            while (true)
            {
                string messageTopicReceived = SubSocket.ReceiveFrameString();
                string messageReceived = SubSocket.ReceiveFrameString();
                Console.WriteLine(messageReceived);
            }

        }

        public void Dispose()
        {
            SubSocket.Dispose();
        }
        
    }
}
