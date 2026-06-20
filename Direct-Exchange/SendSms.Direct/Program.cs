using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var exchangeName = "User-Exchange";
var queueName = "register-User";
var routingKey = "user.sms"; // must match exactly with the publisher's routing key

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

await using var connection = await connectionFactory.CreateConnectionAsync();
await using var model = await connection.CreateChannelAsync();

await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);
await model.QueueDeclareAsync(queueName, true, false, false, null);
await model.QueueBindAsync(queueName, exchangeName, routingKey);

var consumer = new AsyncEventingBasicConsumer(model);

consumer.ReceivedAsync += async (sender, args) =>
{
    var result = Encoding.UTF8.GetString(args.Body.ToArray());
    Console.WriteLine("Send Sms For " + result);
    await Task.CompletedTask;
};

await model.BasicConsumeAsync(queueName, true, consumer);

Console.WriteLine("Waiting for messages. Press Enter to exit.");
Console.ReadLine();