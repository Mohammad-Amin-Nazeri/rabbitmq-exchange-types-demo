//

using System.Text;
using RabbitMQ.Client;

var queueName = "register-User";
Console.Write("Enter Your PhoneNumber :");
var phoneNumber = Console.ReadLine();

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
var connection = await connectionFactory.CreateConnectionAsync();
var model = await connection.CreateChannelAsync();
await model.QueueDeclareAsync(queueName, true, false, false, null);


if (phoneNumber != null)
{
    var body = Encoding.ASCII.GetBytes(phoneNumber.ToString());
    await model.BasicPublishAsync("", queueName, body);
}

Console.WriteLine(phoneNumber);
Console.ReadKey(); 