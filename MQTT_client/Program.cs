using MQTT;
using System.Threading.Tasks;

namespace MQTT_client
{
    class Program
    {
        static async Task MainAsync(string[] args)
        {
            await StartMQTT();


        }

        private static async Task StartMQTT()
        {
            Publisher publisher = new Publisher();
            int i = 0;

            while (i < 10000)
            {

                publisher.SendSingleFrame("TopicA", "MQTT test:  " + i);

                i++;
            }
        }
    }


}
