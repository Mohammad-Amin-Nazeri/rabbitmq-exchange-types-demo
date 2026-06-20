//

using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Utils;

var queueName = "register-User";
var exchangeName = "User";

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
var connection = await connectionFactory.CreateConnectionAsync();
var model = await connection.CreateChannelAsync();
await model.QueueDeclareAsync(queueName, true, false, false, null);

await model.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic, true);

if (phoneNumber != null)
{
    var user = new User()
    {
        Email = email ?? "",
        PhoneNumber = phoneNumber,
    };

    var userConverted = JsonConvert.SerializeObject(user);
    var body = Encoding.UTF8.GetBytes(userConverted);
    await model.BasicPublishAsync(exchangeName, "User.registered", body);
}

Console.WriteLine(phoneNumber);
Console.ReadKey();