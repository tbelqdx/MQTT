using System;
using System.Threading.Tasks;
using MQTT;

namespace Program
{
    class Program
    {
        static async Task MainAsync(string[] args)
        {
            await StartListeningMQTT();


        }

        private static async Task StartListeningMQTT()
        {
            Subscriber subscriber = new Subscriber();
            Intermediary intermediary = new Intermediary();
            RouterSocket router = new RouterSocket();
            router.ReceiveMessage();

            Task.Run(() =>
            {
                intermediary.StartIntermediary();
            });
            await Task.Delay(TimeSpan.FromSeconds(1));
            Task.Run(() =>
            {
                //subscriber.SubscribeTopic("TOPICA");
                subscriber.SubscribeTopic("Box");
            });
        }
    }
}
