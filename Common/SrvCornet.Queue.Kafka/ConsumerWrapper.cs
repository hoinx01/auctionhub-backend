using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using NLog;

namespace SrvCornet.Queue.Kafka
{
    public class ConsumerWrapper
    {
        private KafkaConsumer consumer;
        private string[] topics;
        private BaseKafkaMessageProcessor processor;
        private Logger logger = LogManager.GetLogger("KafkaConsumerWrapper");

        public ConsumerWrapper(KafkaConsumer consumer, string[] topics, BaseKafkaMessageProcessor processor)
        {
            this.consumer = consumer;
            this.processor = processor;
            this.topics = topics;
        }
        public async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                consumer.Subscribe(topics);
                consumer.OnError += (obj, error) =>
                {
                    Console.WriteLine(error);
                };
                var offsets = new List<long>();
                consumer.OnRecord += async (obj, msg) =>
                {
                    lock (offsets)
                    {
                        offsets.Add(msg.Offset);
                    }

                    try
                    {
                        logger.Info(string.Format("topic: {0}, offset: {1}", msg.Topic, msg.Offset));
                        await processor.ProcessMessageAsync(msg);
                        consumer.StoreOffsets(new List<TopicPartitionOffset>() { new TopicPartitionOffset(msg.TopicPartition, msg.Offset) });
                        var commitedOffset = consumer.Commit(msg);

                        logger.Info(string.Format("topic: {0}, offset: {1} success", msg.Topic, msg.Offset));
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, string.Format("topic: {0}, offset: {1} failed", msg.Topic, msg.Offset));
                        throw;
                    }
                    lock (offsets)
                    {
                        offsets.Remove(msg.Offset);
                    }
                };

                while (true)
                {
                    if (offsets.Count <= 1)
                        consumer.Poll(100);
                }
            });
        }
    }
}
