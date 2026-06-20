namespace Utils;

// Shared DTO sent as JSON payload between publishers and consumers
public class User
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}