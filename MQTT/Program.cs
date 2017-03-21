using System;
using System.Threading.Tasks;
using MQTT;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

           StartListeningMQTT().Wait();
        }

        private static async Task StartListeningMQTT()
        {
            Subscriber subscriber = new Subscriber();
            Intermediary intermediary = new Intermediary();
            Task.Run(() =>
            {
                intermediary.StartIntermediary();
            });
            await Task.Delay(TimeSpan.FromSeconds(1));
            Task.Run(() =>
            {
                subscriber.SubscribeTopic("TopicA");
            });
            Task.Run(() =>
            {
                subscriber.Listen();
            });
        }
    }
}
