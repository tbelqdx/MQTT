using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace MQTT
{
    public class RouterSocket
    {


        private NetMQ.Sockets.RouterSocket server;

        private NetMQPoller poller;

        public RouterSocket()
        {

            server = new NetMQ.Sockets.RouterSocket("@tcp://localhost:1235");



        }

        public async Task ReceiveMessage()
        {
            while (true)
            {
                var clientMessage = server.ReceiveMultipartMessage();
                Console.WriteLine("======================================");
                Console.WriteLine(" INCOMING CLIENT MESSAGE FROM CLIENT ");
                Console.WriteLine("======================================");
                PrintFrames("Server receiving", clientMessage);
                if (clientMessage.FrameCount == 3)
                {
                    var clientAddress = clientMessage[0];
                    var clientOriginalMessage = clientMessage[2].ConvertToString();
                    string response = string.Format("{0} back from server {1}",
                        clientOriginalMessage, DateTime.Now.ToLongTimeString());
                    var messageToClient = new NetMQMessage();
                    messageToClient.Append(clientAddress);
                    messageToClient.AppendEmptyFrame();
                    messageToClient.Append(response);
                    server.SendMultipartMessage(messageToClient);
                }
            }
        }

        private void PrintFrames(string operationType, NetMQMessage message)
        {
            for (int i = 0; i < message.FrameCount; i++)
            {
                Console.WriteLine("{0} Socket : Frame[{1}] = {2}", operationType, i,
                    message[i].ConvertToString());
            }
        }


        public void Dispose()
        {
            server.Dispose();
            poller.Dispose();
        }


    }
}