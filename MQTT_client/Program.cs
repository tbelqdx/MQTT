using MQTT;
using System;
using System.Threading.Tasks;

namespace MQTT_client
{
    class Program
    {
        static  void Main(string[] args)
        {
            bool result = StartMQTT().Result;
            


        }

        public static async Task<bool> StartMQTT()
        {
            Publisher publisher = new Publisher();
            int i = 0;

            while (i < 10000)
            {

                publisher.SendSingleFrame("TopicA", "MQTT test:  " + i);
                Console.WriteLine("TopicA"+ "MQTT test:  " + i);
                await Task.Delay(1);

                i++;
            }
            return (true);
        }
    }


}
