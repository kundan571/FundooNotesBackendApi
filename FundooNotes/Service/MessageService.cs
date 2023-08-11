using Azure.Messaging.ServiceBus;

namespace FundooNotes.Service;

public class MessageService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly IConfiguration _configuration;

    public MessageService(IConfiguration configuration)
    {
        _configuration = configuration;
        string connectionString = _configuration["Azure-Bus:ConnectionString"];
        string queueName = _configuration["Azure-Bus:Queue-Name"];

        _serviceBusClient = new ServiceBusClient(connectionString);
        _serviceBusSender = _serviceBusClient.CreateSender(queueName);
    }

    public async Task SendMessageToQueue(string email, string token)
    {
        string messageBody = $"To reset your fundoo notes password,copy and paste the following code : \n\n\n\t {token}";
        ServiceBusMessage message = new ServiceBusMessage();
        message.Subject = "Fundoo notes Reset Token";
        message.To = email;
        message.Body = BinaryData.FromString(messageBody);

        await _serviceBusSender.SendMessageAsync(message);
    }
}
