using System.Reflection.Metadata;
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
var connection = await connectionFactory.CreateConnectionAsync();
var model = await connection.CreateChannelAsync();
await model.QueueDeclareAsync(queueName, true, false, false, null);
await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic, true);

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
};

await model.BasicConsumeAsync(queueName, true, consumer);

Console.Read();