using System;
using NetMQ;
using NetMQ.Sockets;

namespace Madam.App.Cloud.MQTT
{
    public class Intermediary
    {
        public void StartIntermediary()
        {
            using (var xpubSocket = new XPublisherSocket("@tcp://localhost:1234"))
            using (var xsubSocket = new XSubscriberSocket("@tcp://localhost:5678"))
            {
                Console.WriteLine("Intermediary started, and waiting for messages");

                // proxy messages between frontend / backend
                var proxy = new Proxy(xsubSocket, xpubSocket);

               
                // blocks indefinitely
                proxy.Start();
            }
        }
    }
}