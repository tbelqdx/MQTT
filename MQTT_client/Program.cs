using MQTT;
using System;
using System.Threading.Tasks;

namespace MQTT_client
{
    class Program
    {
        static  void Main(string[] args)
        {
            Task.Delay(15);
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
                

                i++;
            }
            return (true);
        }
    }


}
