public class EmailNotificationService : INotificationService
{
    public void SendNotificationOnSuccess(string successMessage)
    {
        Console.WriteLine($"Email message notification is successful:{successMessage}");

    }
    public void SendNotificationOnFailure(string failureMessage)
    {
        Console.WriteLine($"Email Message notification is Failure:{failureMessage}");

    }
}