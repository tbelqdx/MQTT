using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace MQTT
{
    class DealerSockets
    {
        DealerSocket client;
        private const int delay = 3000;

        public void DealerSocketInit()
        {
            //Task.Factory.StartNew(state =>
            //{
            var ClientSocketPerThread = new ThreadLocal<DealerSocket>();
            client = new DealerSocket();
            client.Options.Identity = Encoding.Unicode.GetBytes("test");
            client.Connect("tcp://localhost:1235");
            client.ReceiveReady += Client_ReceiveReady;
            ClientSocketPerThread.Value = client;
            if (!ClientSocketPerThread.IsValueCreated)
            {

                //poller.Add(client);
            }
            else
            {
                client = ClientSocketPerThread.Value;
            }



            //}, string.Format("client {0}", 1), TaskCreationOptions.LongRunning);
        }

        public void Sendframe()
        {
            int i;
            for (i = 0; i < 50; i++)
            {
                var messageToServer = new NetMQMessage();
                messageToServer.AppendEmptyFrame();
                messageToServer.Append("MESSAGE DE TEST");

                Console.WriteLine("======================================");
                Console.WriteLine(" OUTGOING MESSAGE TO SERVER ");
                Console.WriteLine("======================================");
                PrintFrames("Client Sending", messageToServer);
                client.SendMultipartMessage(messageToServer);
                Thread.Sleep(delay);
            }

        }

        void PrintFrames(string operationType, NetMQMessage message)
        {
            for (int i = 0; i < message.FrameCount; i++)
            {
                Console.WriteLine("{0} Socket : Frame[{1}] = {2}", operationType, i,
                    message[i].ConvertToString());
            }
        }

        private void Client_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
