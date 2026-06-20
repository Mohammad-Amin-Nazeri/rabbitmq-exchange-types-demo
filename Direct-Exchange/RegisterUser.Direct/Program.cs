using System.Text;
using RabbitMQ.Client;

// Exchange and queue setup for the "Direct" demo
var exchangeName = "User-Exchange";
var queueName = "register-User";
var routingKey = "user.sms"; // must match exactly with the consumer's binding key

Console.Write("Enter Your PhoneNumber: ");
var phoneNumber = Console.ReadLine();

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

await using var connection = await connectionFactory.CreateConnectionAsync();
await using var model = await connection.CreateChannelAsync();

// Direct exchange: routes messages to queues bound with the exact routing key
await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);
await model.QueueDeclareAsync(queueName, true, false, false, null);
await model.QueueBindAsync(queueName, exchangeName, routingKey);

if (phoneNumber != null)
{
    var body = Encoding.UTF8.GetBytes(phoneNumber);

    // Publish to the exchange (NOT to the default exchange) with the routing key
    await model.BasicPublishAsync(exchangeName, routingKey, body);
}

Console.WriteLine(phoneNumber);
Console.ReadKey();