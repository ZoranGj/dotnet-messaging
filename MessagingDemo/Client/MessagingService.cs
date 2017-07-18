using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace Client
{
    public class MessagingService
    {
        const string CONFIG_HOSTNAME = "localhost";
        const string CONFIG_USERNAME = "guest";
        const string CONFIG_PASSWORD = "guest";
        const string RABBITMQ_EXCHANGE = "messagingDemoExchange";
        const string RABBITMQ_QUEUE = "messagingDemoQueue";
        const string RABBITMQ_ROUTINGKEY = "route_key";

        public static void Main()
        {
            AddMessageToQueue();
        }

        public static void AddMessageToQueue()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = CONFIG_HOSTNAME;
            connectionFactory.UserName = CONFIG_USERNAME;
            connectionFactory.Password = CONFIG_PASSWORD;
            IConnection connection = connectionFactory.CreateConnection();

            IModel model = connection.CreateModel();

            model.QueueDeclare(RABBITMQ_QUEUE, true, false, false, null);
            model.ExchangeDeclare(RABBITMQ_EXCHANGE, ExchangeType.Topic);
            model.QueueBind(RABBITMQ_QUEUE, RABBITMQ_EXCHANGE, RABBITMQ_ROUTINGKEY);
            model.BasicPublish(RABBITMQ_EXCHANGE, RABBITMQ_ROUTINGKEY, true, null, RandomMessage());
        }

        public static byte[] RandomMessage()
        {
            int number = new Random().Next(20, 50);
            string message = Enumerable.Range(10, number).Select(i => i.ToString()).ToString();
            return Encoding.ASCII.GetBytes(message);
        }
    }
}
 