using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Services
{
    public class RabbitMqService
    {
        private ConnectionFactory factory;
        private IConnection connection;

        public RabbitMqService()
        {
            factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://icpodzmj:k6tphAR4wqIo8ma0kbTXZ15tozBfrzvc@eagle.rmq.cloudamqp.com/icpodzmj")
            };

            connection = factory.CreateConnection();
        }

        public void SendMessage(string message)
        {
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "athena_task_queue", durable: true,
                    exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "", routingKey: "athena_task_queue", basicProperties: properties,
                    body: body);
            }
        }

        ~RabbitMqService()
        {
            connection.Close();
        }
    }
}
