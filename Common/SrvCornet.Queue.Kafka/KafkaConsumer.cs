using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace SrvCornet.Queue.Kafka
{
    public class KafkaConsumer : Consumer<string, string>
    {
        public KafkaConsumer(KafkaConfig config) : base(config.GetConsumerConfig(), new StringDeserializer(Encoding.UTF8), new StringDeserializer(Encoding.UTF8))
        {

        }
    }
}
