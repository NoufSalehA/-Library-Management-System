using System.Reflection.Metadata;
public interface INotificationService
{
    void SendNotificationOnSuccess(string successMessage);
    void SendNotificationOnFailure(string failureMessage);

}
internal class Program
{
    private static void Main()
    {
        var emailService = new EmailNotificationService();//1 pass them to library
        var smsService = new SMSNotificationService();//2
        var libraryWithEmail = new Library(emailService);
        var libraryWithSms = new Library(smsService);
        var book1 = new Book("When Marnie Was There", new DateTime(1967, 11, 11));
        var book2 = new Book("And Then There Were None", new DateTime(1939, 3, 1));
        var book3 = new Book("Dumb Witness");
        var book7 = new Book("Jane Eyre", new DateTime(2023, 7, 1));
        var book4 = new Book("The Catcher in the Rye", new DateTime(2023, 4, 1));
        var book5 = new Book("Pride and Prejudice", new DateTime(2023, 5, 1));
        var book6 = new Book("Wuthering Heights", new DateTime(2023, 6, 1));
        var user1 = new User("Alice", new DateTime(2023, 1, 1));
        var user2 = new User("Bob", new DateTime(2023, 2, 1));
        var user3 = new User("Charlie", new DateTime(2023, 3, 1));
        var user4 = new User("David");
        Console.WriteLine($"-----------------------------------------------");
        Console.WriteLine($"=== Books Notification ===");
        libraryWithEmail.AddBook(book1);
        libraryWithEmail.AddBook(book2);
        libraryWithEmail.AddBook(book3);
        libraryWithEmail.AddBook(book4);
        Console.WriteLine($"-----------------------------------------------");
        var books = libraryWithEmail.GetAllBooks(1, 4);
        Console.WriteLine($"Books list :");
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Name} ,{b.CreatedDate}.{b.Id}");
        }
        Console.WriteLine($"---------------------------------------------------");
        libraryWithEmail.DeleteBook(book4.Id);
        Console.WriteLine($"Update Book List  :");
        books = libraryWithEmail.GetAllBooks(1, 4);
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Name} ,{b.CreatedDate}.{b.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"=== Users Notification ===");
        libraryWithSms.AddUser(user1);
        libraryWithSms.AddUser(user2);
        libraryWithSms.AddUser(user3);
        libraryWithSms.AddUser(user4);
        Console.WriteLine($"-------------------------------------------------------");
        var users = libraryWithSms.GetAllUsers(1, 4);
        Console.WriteLine($"Users list :");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Name} ,{u.CreatedDate}.{u.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");

        Console.WriteLine($"=== Notification ===");

        libraryWithSms.DeleteUser(user2.Id);
        users = libraryWithSms.GetAllUsers(1, 4);
        Console.WriteLine($"------------");
        Console.WriteLine($"Updated Users List");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Name} ,{u.CreatedDate}.{u.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Search A book that include the word *Marnie* :");
        var book = libraryWithEmail.FindBookByItsTitle("Marnie");
        foreach (var title in book)
        {
            Console.WriteLine($"====== {title.Name} ==========");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Search A user with *Ali* in her name");
        var user = libraryWithSms.FindUserByName("Alice");
        foreach (var name in user)
        {
            Console.WriteLine($"===== {name.Name} ====");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"*******Sorted by created date Books List********");
        var sortedBooks = libraryWithEmail._books.OrderBy(book => book.CreatedDate).ToList();
        foreach (var sorted in sortedBooks)
        {
            Console.WriteLine($"{sorted.Name},{sorted.CreatedDate} ,{sorted.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"*******Sorted by created date Users List********");
        var sortedUsers = libraryWithSms._users.OrderBy(user => user.CreatedDate).ToList();
        foreach (var sort in sortedUsers)
        {
            Console.WriteLine($"{sort.Name}, {sort.CreatedDate} , {sort.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"--- The End ---");
    }
}
