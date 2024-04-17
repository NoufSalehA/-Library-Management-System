using System.Reflection.Metadata;

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
    private List<User> _users;
    public Library()//constructor -create objs for users,books
    {
        books = new List<Book>();

        _users = new List<User>();
    }
    public void AddBook(Book book)//method for adding books
    {
        books.Add(book);
    }
    public void DeleteBook(Guid id)//method for deleting books by its id
    {
        Book? bookToDelete = books.Find(book => book.Id == id);//find book by id
        if (bookToDelete != null)
        {
            books.Remove(bookToDelete);
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
    }
    public void DeleteUser(Guid id)
    {
        User? userToDelete = _users.Find(user => user.Id == id);//find user by id
        if (userToDelete != null)
        {
            _users.Remove(userToDelete);
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


        Library library = new Library();//book -user
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);

        library.AddUser(user1);
        library.AddUser(user2);
        library.AddUser(user3);
        library.AddUser(user4);
        var books = library.GetAllBooks(1, 4);
        Console.WriteLine($"Orginal Books list");

        foreach (var b in books)
        {
            Console.WriteLine($"{b.Names} ,{b.TheCreateDate}.{b.Id}");

        }
        library.DeleteBook(book4.Id);
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"after deleting book 4:The Catcher in the Rye \n");
        books = library.GetAllBooks(1, 4);

        foreach (var b in books)
        {
            Console.WriteLine($"{b.Names} ,{b.TheCreateDate}.{b.Id}");

        }
        Console.WriteLine($"-------------------------------------------------------");
        var users = library.GetAllUsers(1, 4);
        Console.WriteLine($"Orginal Users list");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Names} ,{u.TheCreateDate}.{u.Id}");
        }
        library.DeleteUser(user2.Id);
        users = library.GetAllUsers(1, 4);
        Console.WriteLine($"-------------------------------------------------------");

        Console.WriteLine($"After deleting user2:Bob");


        foreach (var u in users)
        {
            Console.WriteLine($"{u.Names} ,{u.TheCreateDate}.{u.Id}");


        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Find A book include the word *Marnie*");
        var book = library.FindBookByItsTitle("Marnie");
        foreach (var title in book)
        {
            Console.WriteLine($"====== {title.Names} ==========");

        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"Find A user with *Ali* in her name");
        var user = library.FindUserByName("Alice");
        foreach (var name in user)
        {
            Console.WriteLine($"===== {name.Names} ====");

        }
        Console.WriteLine($"-------------------------------------------------------");
        Console.WriteLine($"*******Sorted by created date Books List********");









    }
}
//