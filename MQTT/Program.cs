using System;
using System.Threading.Tasks;
using MQTT;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
           bool result=StartListeningMQTT().Result;


        }

        private static async Task<bool> StartListeningMQTT()
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
                subscriber.SubscribeTopic("TOPICA");
                
            });
            return true;
        }
    }
}
