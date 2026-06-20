using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Utils;

var queueName = "SendSmsForUser";
var exchangeName = "User";

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

await using var connection = await connectionFactory.CreateConnectionAsync();
await using var model = await connection.CreateChannelAsync();

await model.QueueDeclareAsync(queueName, true, false, false, null);
await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic, true);

// "*" matches exactly one word, so this matches keys like "User.registered"
await model.QueueBindAsync(queueName, exchangeName, "*.registered");

var consumer = new AsyncEventingBasicConsumer(model);

consumer.ReceivedAsync += async (sender, args) =>
{
    var result = Encoding.UTF8.GetString(args.Body.ToArray());
    var user = JsonConvert.DeserializeObject<User>(result);

    if (user != null)
    {
        Console.WriteLine("Send Sms For " + user.PhoneNumber);
    }

    await Task.CompletedTask;
};

await model.BasicConsumeAsync(queueName, true, consumer);

Console.WriteLine("Waiting for messages. Press Enter to exit.");
Console.ReadLine();