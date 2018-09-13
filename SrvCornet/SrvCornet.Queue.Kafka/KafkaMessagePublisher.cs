using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using NLog;
using SrvCornet.Queue.Kafka;
using SrvCornet.Utils;

namespace Ikel.Common.Kafka
{
    public class KafkaMessagePublisher
    {
        private Producer<string,string> producer;
        private Logger logger = LogManager.GetLogger("KafkaMessagePublisher");

        public KafkaMessagePublisher(IkenKafkaProducer producer)
        {
            this.producer = producer;
        }
        public async Task<DeliveryReport<string, string>> Publish(string topic, object obj)
        {
            string payload = null;
            if (obj.GetType().Equals(typeof(string)))
                payload = (string)obj;
            else
                payload = SnakeJsonConverter.Serialize(obj);
            try
            {
                //var message = await producer.ProduceAsync(topic, DateTime.Now.ToLongTimeString(), payload);
                var message = new Message<string, string>();
                message.Key = DateTime.Now.ToLongTimeString();
                message.Value = payload;
                var deliveryReport = await producer.ProduceAsync(topic, message);
                return deliveryReport;
            }
            catch(Exception e)
            {
                logger.Error(e);
                throw;
            }
        }
    }
}
