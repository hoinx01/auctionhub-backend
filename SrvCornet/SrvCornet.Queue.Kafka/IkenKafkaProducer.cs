using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace SrvCornet.Queue.Kafka
{
    public class IkenKafkaProducer : Producer<string, string>
    {
        public IkenKafkaProducer(KafkaConfig kafkaConfig) : base(kafkaConfig.GetProducerConfig(), new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8))
        {

        }
    }
}
