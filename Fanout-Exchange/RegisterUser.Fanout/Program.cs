using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Utils;

var queueName = "register-User";
var exchangeName = "User.Registered";

Console.Write("Enter Your PhoneNumber :");
var phoneNumber = Console.ReadLine();

Console.Write("Enter Your Email :");
var email = Console.ReadLine();

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

await using var connection = await connectionFactory.CreateConnectionAsync();
await using var model = await connection.CreateChannelAsync();

await model.QueueDeclareAsync(queueName, true, false, false, null);

// Fanout exchange: routing key is ignored, message is broadcast to every bound queue
await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, true);

if (phoneNumber != null)
{
    var user = new User
    {
        Email = email ?? "",
        PhoneNumber = phoneNumber,
    };

    var userConverted = JsonConvert.SerializeObject(user);
    var body = Encoding.UTF8.GetBytes(userConverted);

    // Routing key "" is irrelevant for fanout exchanges
    await model.BasicPublishAsync(exchangeName, "", body);
}

Console.WriteLine(phoneNumber);
Console.ReadKey();