using MQTT;
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

                i++;
            }
            return (true);
        }
    }


}
