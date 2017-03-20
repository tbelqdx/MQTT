using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT
{
    class Program
    {
        static void Main(string[] args)
        {


            using (NetMQContext context = NetMQContext.Create())
            {
                using (var publisherSocket = context.CreateXPublisherSocket())
                {
                    publisherSocket.SetWelcomeMessage("WM");
                    publisherSocket.Bind("tcp://*:6669");

                    // we just drop subscriptions                     
                    publisherSocket.ReceiveReady += (sender, eventArgs) =>
                      publisherSocket.SkipMultipartMessage();

                    NetMQPoller poller = new NetMQPoller();
                    poller.Add(publisherSocket);

                    // send a message every second
                    
                    sendMessageTimer.Elapsed += (sender, eventArgs) =>
                      publisherSocket.
                        SendMoreFrame("A").
                        SendFrame(new Random().Next().ToString());

                    // send heartbeat every two seconds
                    ;
                    heartbeatTimer.Elapsed +=
                      (sender, eventArgs) => publisherSocket.SendFrame("HB");

                    poller.PollTillCancelled();
                }
            }
        }
    }
}
