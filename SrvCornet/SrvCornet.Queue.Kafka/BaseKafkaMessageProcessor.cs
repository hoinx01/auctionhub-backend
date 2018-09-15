using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace SrvCornet.Queue.Kafka
{
    public abstract class BaseKafkaMessageProcessor
    {
        public abstract void ProcessError(Error error);
        public abstract Task ProcessMessageAsync(ConsumerRecord<string, string> message);
    }
}
