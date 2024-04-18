using System.Reflection.Metadata;
public interface INotificationService
{
    void SendNotificationOnSuccess(string successMessage);
    void SendNotificationOnFailure(string failureMessage);

}
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
public class LibraryItems
{//only name is required
    public Guid Id
    {
        get;
        set;
    }
    public string Names
    {
        get;
        set;
    }
    public DateTime TheCreateDate
    {
        get;
        set;
    }
    public LibraryItems(string names, DateTime? thecreatedate = null)//constructor//trying null instead of default
    {
        Id = Guid.NewGuid();
        this.Names = names;
        this.TheCreateDate = thecreatedate ?? DateTime.Now;//if it is null then it will use the date of today- without this. is also correct
    }
}
public class Book : LibraryItems//inheritance
{
    public Book(string title, DateTime? theDate = null) : base(title, theDate)
    {//inherit /access the parent constructor using ':base'
    }
}
public class User : LibraryItems
{
    public User(string name, DateTime? theDate = null) : base(name, theDate)
    {

    }
}
public class Library
{//all the work
    public List<Book> books;
    public List<User> _users;
    private INotificationService notificationService;//receive what inside constructor down
    public Library(INotificationService notificationService)//constructor -create objs for users,books //define notification inside constructor. dependency injection done

    {
        books = new List<Book>();
        _users = new List<User>();
        this.notificationService = notificationService;
    }
    public void AddBook(Book book)//method for adding books
    {
        books.Add(book);// the message here will added/pass to the original success message
        notificationService.SendNotificationOnSuccess($"*{book.Names}* Has been Added");
    }
    public void DeleteBook(Guid id)//method for deleting books by its id
    {
        Book? bookToDelete = books.Find(book => book.Id == id);//find book by id
        if (bookToDelete != null)
        {
            books.Remove(bookToDelete);
            notificationService.SendNotificationOnSuccess($"*{bookToDelete.Names} *--- Deleted ---");
        }
        else
        {
            Console.WriteLine($"--!--Incorrect ID--!--");
        }
    }
    public List<Book> FindBookByItsTitle(string title)
    {
        return books.FindAll(book => book.Names.Contains(title));
    }
    public List<Book> GetAllBooks(int page, int pageSize)
    {
        //pagination: number of page-limit(page size)
        return books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
    public void AddUser(User user)//method to add user
    {
        _users.Add(user);
        notificationService.SendNotificationOnSuccess($"*{user.Names}* Has been Added");
    }
    public void DeleteUser(Guid id)
    {
        User? userToDelete = _users.Find(user => user.Id == id);//find user by id
        if (userToDelete != null)
        {
            _users.Remove(userToDelete);
            notificationService.SendNotificationOnFailure($"User{userToDelete.Names} is removed from list");
        }
        else
        {
            Console.WriteLine($"--!--Incorrect ID--!--");
        }
    }
    public List<User> FindUserByName(string name)
    {
        return _users.FindAll(user => user.Names.Contains(name));
    }
    public List<User> GetAllUsers(int page, int pageSize)
    {
        //pagination: number of page-limit(page size)
        return _users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
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
            Console.WriteLine($"{b.Names} ,{b.TheCreateDate}.{b.Id}");
        }
        Console.WriteLine($"---------------------------------------------------");
        libraryWithEmail.DeleteBook(book4.Id);
        Console.WriteLine($"Update Book List  :");
        books = libraryWithEmail.GetAllBooks(1, 4);
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Names} ,{b.TheCreateDate}.{b.Id}");
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
            Console.WriteLine($"{u.Names} ,{u.TheCreateDate}.{u.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");

        Console.WriteLine($"=== Notification ===");

        libraryWithSms.DeleteUser(user2.Id);
        users = libraryWithSms.GetAllUsers(1, 4);
        Console.WriteLine($"------------");
        Console.WriteLine($"Updated Users List");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Names} ,{u.TheCreateDate}.{u.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Search A book that include the word *Marnie* :");
        var book = libraryWithEmail.FindBookByItsTitle("Marnie");
        foreach (var title in book)
        {
            Console.WriteLine($"====== {title.Names} ==========");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Search A user with *Ali* in her name");
        var user = libraryWithSms.FindUserByName("Alice");
        foreach (var name in user)
        {
            Console.WriteLine($"===== {name.Names} ====");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"*******Sorted by created date Books List********");
        var sortedBooks = libraryWithEmail.books.OrderBy(book => book.TheCreateDate).ToList();
        foreach (var sorted in sortedBooks)
        {
            Console.WriteLine($"{sorted.Names},{sorted.TheCreateDate} ,{sorted.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"*******Sorted by created date Users List********");
        var sortedUsers = libraryWithSms._users.OrderBy(user => user.TheCreateDate).ToList();
        foreach (var sort in sortedUsers)
        {
            Console.WriteLine($"{sort.Names}, {sort.TheCreateDate} , {sort.Id}");
        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"--- The End ---");
    }
}
