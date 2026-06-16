using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var queueName = "register-User";

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
var connection = await connectionFactory.CreateConnectionAsync();
var model = await connection.CreateChannelAsync();
await model.QueueDeclareAsync(queueName, true, false, false, null);

var consumer = new AsyncEventingBasicConsumer(model);

consumer.ReceivedAsync += async (sender, args) =>
{
    var result = Encoding.ASCII.GetString(args.Body.ToArray());
    Console.WriteLine("Send Sms For " + result);
};

await model.BasicConsumeAsync(queueName,true,consumer);

Console.Read();