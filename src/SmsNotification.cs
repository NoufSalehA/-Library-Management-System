public class SMSNotificationService : INotificationService
{
    public void SendNotificationOnSuccess(string successMessage)
    {
        Console.WriteLine($"SMS Message notification is Successful:{successMessage}");
    }
    public void SendNotificationOnFailure(string failureMessage)
    {
        Console.WriteLine($"SMS Message notification is Failure:{failureMessage}");
    }
}