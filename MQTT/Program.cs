using System;
using System.Threading.Tasks;
using MQTT;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NetMQ Server";

            StartListeningMQTT().Wait();
        }

        private static async Task StartListeningMQTT()
        {
            Subscriber subscriber = new Subscriber();
            Intermediary intermediary = new Intermediary();

            Parallel.Invoke(
            () =>
          
            {
                intermediary.StartIntermediary();
                
            },

            () =>             
            {
                subscriber.SubscribeTopic("TopicA");
                subscriber.Listen();
            });
           
        }
    }
}
